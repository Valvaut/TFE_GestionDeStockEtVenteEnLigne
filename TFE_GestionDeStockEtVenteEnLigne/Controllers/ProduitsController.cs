using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TFE_GestionDeStockEtVenteEnLigne.Data;
using TFE_GestionDeStockEtVenteEnLigne.Models;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System;
using TFE_GestionDeStockEtVenteEnLigne.Models.Metier;

namespace TFE_GestionDeStockEtVenteEnLigne.Controllers
{
    public class ProduitsController : Controller
    {
        private readonly TFEContext _context;

        public ProduitsController(TFEContext context)
        {
            _context = context;    
        }

        // GET: Produits
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Produits.ToListAsync());
        //}
        public async Task<IActionResult> Index(string sortOrder, string searchStr, string cat,string qte)
        {
            ViewData["RefSort"] = String.IsNullOrEmpty(sortOrder) ? "ref_desc" : "";
            ViewData["DenoSort"] = sortOrder == "Denomination" ? "deno_desc" : "Denomination";
            ViewData["Filter"] = searchStr;
            ViewData["passtock"] = TempData["passtock"];

            var produits = from f in _context.Produits select f;
            //var produits = _context.Produits.ToArray();
            var pro = _context.Produits;

            if (!String.IsNullOrEmpty(cat))
            {
                //produits = produits.Where(f => f.Categorie.Nom.Contains(cat));
                var categorie = _context.Categories
                    .Include(c => c.CategorieEnfant)
                        .ThenInclude(p => p.Produits)
                    .Include(c => c.Produits)
                    .SingleOrDefault(c => c.Nom.Equals(cat));
                var prods = GetProdRecu(categorie);
                //List<Produit> prods = cate.Produits.ToList();
                //while (cate.CategorieParent != null)
                //{
                //    cate = _context.Categories
                //        .Include(c=>c.Produits)
                //        .SingleOrDefault(cp=>cp.ID == cate.CategorieParentID);
                //    prods.AddRange(cate.Produits);
                //}
                int i = 0;
                while (i < prods.Count)
                {
                    if (prods[i].Visible)
                    {
                        ++i;
                    }
                    else
                    {
                        prods.RemoveAt(i);
                        prods.RemoveAll(Produit.PredicatDelNotVisible);
                    }
                }
                ViewData["cat"] = cat;
                return View(prods);
            }
            else if (!String.IsNullOrEmpty(searchStr))
            {
                produits = produits.Where(f => f.Ref.Contains(searchStr) || f.Denomination.Contains(searchStr));
                ViewData["recherche"] = searchStr;
            }
            else if (!String.IsNullOrEmpty(qte))
            {
                produits = produits.Where(qte0 => qte0.QuantiteEmballage == 0);
            }
            return View(await produits.Where(p=>p.Visible == true).ToListAsync());
        }


        // GET: Produits/Details/5
        public async Task<IActionResult> Details(int? id, String source )
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["source"] = source;
            var produit = await _context.Produits.Include(p => p.Categorie)
                    .ThenInclude(c => c.CategorieParent)
                .Include(p => p.MotClef)
                    .ThenInclude(mp => mp.MotClef)
                .Include(p=>p.Valeur)
                    .ThenInclude(v=>v.Attribut)
                .Include(p=>p.Provients)
                    .ThenInclude(f=>f.Fournisseur)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            //produit.MotClef = from produitMotClef in ProduitMotClef where produit.ID== ProduitMotClef.id select produitMotClef;
            if (produit == null)
            {
                return NotFound();
            }

            return View(produit);
        }

        // GET: Produits/Create
        //[Authorize(Roles = "gestionnaire")]
        [Authorize(Roles = "gestionnaire")]
        public async Task<IActionResult> Create()
        {
            ProduitCatAdapter pc = new ProduitCatAdapter();
            pc.ListCat = await _context.Categories.Where(c=>!(c.CategorieEnfant.Any())).ToListAsync();
            pc.ListMotClef = await _context.MotClefs.ToListAsync();
            pc.TousLesAttributs = await _context.Attributs.ToListAsync();
            pc.ListFournisseur= await _context.Fournisseurs.ToListAsync();
            pc.TauxTVA = await _context.TVA.OrderByDescending(t=>t.Valeur).ToListAsync();
            return View(pc);
        }

        // POST: Produits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "gestionnaire")]
        [Authorize(Roles = "gestionnaire")]
        public async Task<IActionResult> Create( ProduitCatAdapter produitcatAdapter)
        {
            var prixvente = Request.Form["Prixvente"];
            
            var prixachat = Request.Form["Prix"];
            produitcatAdapter.Produit.Prix = double.Parse(prixvente.ToString().Replace('.', ','));
            if (ModelState.IsValid)
            {
                try
                {

                    //gestion de l'image et du produit
                    MemoryStream ms = new MemoryStream();
                    var images = Request.Form.Files["Image"];
                    images.OpenReadStream().CopyTo(ms);
                    produitcatAdapter.Produit.Image = ms.ToArray();
                    produitcatAdapter.Produit.Date = DateTime.Now;
                    produitcatAdapter.Produit.Visible = true;
                    produitcatAdapter.Produit.QuantiteStockTotal = produitcatAdapter.Produit.QuantiteEmballage * produitcatAdapter.Produit.NBPieceEmballage + produitcatAdapter.Produit.QuantiteStock;
                    produitcatAdapter.Produit.TauxTVAID = int.Parse(Request.Form["Produit.TauxTVAID"]); 
                    _context.Add(produitcatAdapter.Produit);//insert le produit


                    var tableauIDAttributs = Request.Form["nameAttribut"];
                    var tableauValeurs = Request.Form["ValueAttribut"];

                    List<int> listIDAttribut = new List<int>();
                    foreach (var NameAttribut in tableauIDAttributs)
                    {
                        listIDAttribut.Add(int.Parse(NameAttribut));
                    }

                    List<String> listValeur = new List<String>();
                    foreach (var Value in tableauValeurs)
                    {
                        listValeur.Add(Value);
                    }
                    List<Valeur> ListValeurConstruit = new List<Valeur>();
                    int i = 0;
                    while (i < listIDAttribut.Count && i < listValeur.Count)
                    {
                        Valeur v = new Valeur
                        {
                            Valeurs = listValeur[i]
                        };
                        v.AttributID = listIDAttribut[i];
                        ++i;
                        v.ProduitID = produitcatAdapter.Produit.ID;
                        _context.Add(v);

                    }

                    //r�cup�re les id mots clef
                    var tableauMotClef = Request.Form["MotClef"];
                    List<int> ListMotClef = new List<int>();
                    foreach (var mot in tableauMotClef)
                    {
                        ListMotClef.Add(int.Parse(mot));
                    }
                    foreach (int motClef in ListMotClef)
                    {
                        ProduitMotClef pm = new ProduitMotClef
                        {
                            MotClefId = motClef,
                            ProduitID = produitcatAdapter.Produit.ID
                        };
                        _context.Add(pm);

                    }

                    float Prix2 = 0;
                    if (!(prixachat.ToString().Equals("")))
                    {
                        Prix2 = float.Parse(prixachat.ToString().Replace('.', ','));
                    }
                    //gere le fournisseur
                    int fournisseurID = int.Parse((Request.Form["select"]).ToString());
                    Provient provient = new Provient
                    {

                        Prix = Prix2,
                        //TauxTVA = int.Parse(Request.Form["tauxTVA"]),
                        TauxTVAID = int.Parse(Request.Form["tauxTVA"]),
                        QuantiteMinCommande = int.Parse(Request.Form["quantite"]),
                        ProduitID = produitcatAdapter.Produit.ID,
                        FournisseurID = fournisseurID
                    };
                    _context.Add(provient);
                    Historique h = new Historique
                    {
                        Date = DateTime.Now,
                        ProduitID = produitcatAdapter.Produit.ID,
                        Action = "Creation",
                        QteStock = produitcatAdapter.Produit.QuantiteStockTotal,
                        QteMouv = produitcatAdapter.Produit.QuantiteStockTotal,
                    };
                    _context.Add(h);
                    await _context.SaveChangesAsync();
                  
                    return RedirectToAction("");
                }
                catch (Exception e)
                {

                }
            }
            produitcatAdapter.ListCat = _context.Categories.ToList();
            return View(produitcatAdapter);
        }

        // GET: Produits/Edit/5
        [Authorize(Roles = "gestionnaire")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProduitCatAdapter adaptateur = new ProduitCatAdapter();
            adaptateur.Produit = await _context.Produits
                                        .Include(p => p.Categorie)
                                        .Include(p=>p.Valeur)
                                            .ThenInclude(a=>a.Attribut)
                                        .Include(p=>p.MotClef)
                                        .Include(p => p.Provients)
                                            .ThenInclude(tva => tva.TauxTVAObjet)
                                        .Include(p=>p.TauxTVA)
                                        .SingleOrDefaultAsync(m => m.ID == id);
            adaptateur.ListCat = await _context.Categories.Where(c => !(c.CategorieEnfant.Any()))
                                       .ToListAsync();
            adaptateur.ListMotClef = await _context.MotClefs
                                       .Include(mc=>mc.Produit)
                                       .ToListAsync();
            adaptateur.TauxTVA = await _context.TVA.ToListAsync();
            foreach (var item in adaptateur.Produit.MotClef)
            {
                int i = 0;
                while( i < adaptateur.ListMotClef.Count)
                {
                    var item2 = adaptateur.ListMotClef[i];
                    if (item2.Produit.Contains(item))
                    {
                        adaptateur.ListMotClef.Remove(item2);
                    }
                    else
                    {
                        ++i;
                    }
                }
            }
            adaptateur.ListFournisseur = await _context.Fournisseurs
                                       .ToListAsync();
            adaptateur.TousLesAttributs = await _context.Attributs
                                       .ToListAsync();
            if (adaptateur == null)
            {
                return NotFound();
            }
            return View(adaptateur);
        }

        // POST: Produits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "gestionnaire")]
        public async Task<IActionResult> Edit(int id, [Bind("Ref,Denomination,Prix,QuantiteEmballage,NBPieceEmballage,TVA,CompteCompta,Description,Marque,QuantiteStock,Image,CategorieID,TauxTVA")] Produit produit)
        {
            //if (id != produit.ID)
            //{
            //    return NotFound();
            //}
            var a = Request.Form["prixVente"].ToString().Replace(".",",");
            if (produit.Prix == 0)//si nombre a virgule
            {
                produit.Prix = double.Parse(a);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    produit.TauxTVAID = int.Parse(Request.Form["Produit.TauxTVAID"]);
                    var produitBD = await _context.Produits
                               .AsNoTracking()
                               .SingleOrDefaultAsync(m => m.ID == id);
                    //var produitverif = await _context.Produits.Include(p => p.Possede).SingleOrDefaultAsync(m => m.ID == id);
                    //if (produitverif.Possede.Count >= 0)

                    produitBD.Visible = false;
                    var images = Request.Form.Files["Image"];
                    if (images.FileName == "")//si pas chois d'image, prend l'ancienne
                    {
                        produit.Image = produitBD.Image;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream();

                        images.OpenReadStream().CopyTo(ms);
                        produit.Image = ms.ToArray();
                    }
                    _context.Update(produitBD);
                    int qteMouv = produit.QuantiteStockTotal;
                    var qterecus = Request.Form["qteRecu"];
                    String action = "Edit";
                    if (qterecus !="")
                    {
                        var qteRecu = int.Parse(qterecus);
                        produit.QuantiteEmballage += qteRecu;
                        action = "Reasortir";
                        qteMouv = qteRecu;
                    }

                    produit.QuantiteStockTotal = produit.QuantiteEmballage * produit.NBPieceEmballage + produit.QuantiteStock;
                    produit.Visible = true;
                    produit.Date =DateTime.Now;
                   
                    _context.Add(produit);
                    if(qterecus != "")
                    {
                        Historique h = new Historique
                        {
                            Date = DateTime.Now,
                            ProduitID = produit.ID,
                            Action = action,
                            QteMouv = qteMouv,
                            QteStock = produitBD.QuantiteStockTotal,
                        };
                        _context.Add(h);
                    }

                    var tableauMotClef = Request.Form["MotClef"];
                    List<int> ListMotClef = new List<int>();
                    foreach (var mot in tableauMotClef)
                    {
                        ListMotClef.Add(int.Parse(mot));
                    }
                    foreach (int motClef in ListMotClef)
                    {
                        ProduitMotClef pm = new ProduitMotClef
                        {
                            MotClefId = motClef,
                            ProduitID = produit.ID
                        };
                        _context.Add(pm);
                    }
                    var tableauIDAttributs = Request.Form["nameAttribut"];
                    var tableauValeurs = Request.Form["ValueAttribut"];

                    List<int> listIDAttribut = new List<int>();
                    foreach (var NameAttribut in tableauIDAttributs)
                    {
                        listIDAttribut.Add(int.Parse(NameAttribut));
                    }

                    List<String> listValeur = new List<String>();
                    foreach (var Value in tableauValeurs)
                    {
                        listValeur.Add(Value);
                    }
                    List<Valeur> ListValeurConstruit = new List<Valeur>();
                    int i = 0;
                    while (i < listIDAttribut.Count && i < listValeur.Count)
                    {
                        Valeur v = new Valeur
                        {
                            Valeurs = listValeur[i]
                        };
                        v.AttributID = listIDAttribut[i];
                        ++i;
                        v.ProduitID = produit.ID;
                        _context.Add(v);

                    }
                    var prixachat = Request.Form["Prix"];
                    float Prix2 = 0;
                    if (!(prixachat.ToString().Equals("")))
                    {
                        Prix2 = float.Parse(prixachat.ToString().Replace('.', ','));
                    }
                    int fournisseurID = int.Parse((Request.Form["select"]).ToString());
                    Provient provient = new Provient
                    {

                        Prix = Prix2,
                        //TauxTVA = int.Parse(Request.Form["tauxTVA"]),
                        TauxTVAID = int.Parse(Request.Form["tauxTVA"]),
                        QuantiteMinCommande = int.Parse(Request.Form["quantite"]),
                        ProduitID = produit.ID,
                        FournisseurID = fournisseurID
                    };
                    _context.Add(provient);
                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduitExists(produit.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(produit);
        }

        // GET: Produits/Delete/5
        [Authorize(Roles = "gestionnaire")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produits.Include(p=>p.Categorie)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (produit == null)
            {
                return NotFound();
            }

            return View(produit);
        }

        // POST: Produits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "gestionnaire")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produit = await _context.Produits.SingleOrDefaultAsync(m => m.ID == id);
            _context.Produits.Remove(produit);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProduitExists(int id)
        {
            return _context.Produits.Any(e => e.ID == id);
        }

        private List<Produit> GetProdRecu(Categorie c)
        {
             c.CategorieEnfant = _context.Categories
                    .Include(ca => ca.CategorieEnfant)
                    .Include(ca => ca.Produits)
                    .Where(ca=>ca.CategorieParentID == c.ID)
                    .ToList();
            List<Produit> res = new List<Produit>();
            if(c.CategorieEnfant != null)
            { 
                foreach (var cat in c.CategorieEnfant)
                {
                    res.AddRange(GetProdRecu(cat));
              
                }
            }
            if (c.Produits != null)
                res.AddRange(c.Produits);
            return res;
        }

    }
}
