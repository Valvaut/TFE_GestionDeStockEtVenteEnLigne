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
        public async Task<IActionResult> Index(string sortOrder, string searchStr, string cat)
        {
            ViewData["RefSort"] = String.IsNullOrEmpty(sortOrder) ? "ref_desc" : "";
            ViewData["DenoSort"] = sortOrder == "Denomination" ? "deno_desc" : "Denomination";
            ViewData["Filter"] = searchStr;

            var produits = from f in _context.Produits select f;
            var pro = _context.Produits;

            if (!String.IsNullOrEmpty(cat))
            {
                produits = produits.Where(f => f.Categorie.Nom.Contains(cat));
                ViewData["cat"]= cat;
            }
            else if (!String.IsNullOrEmpty(searchStr))
            {
                produits = produits.Where(f => f.Ref.Contains(searchStr) || f.Denomination.Contains(searchStr));
                ViewData["recherche"] = searchStr;
            }
            switch (sortOrder)
            {
                case "ref_desc":
                    produits = produits.OrderByDescending(f => f.Ref); break;
                case "Denomination":
                    produits = produits.OrderBy(f => f.Denomination); break;
                case "deno_desc":
                    produits = produits.OrderByDescending(f => f.Denomination); break;
                default:
                    produits = produits.OrderBy(f => f.Ref); break;
            }
            return View(await produits.Where(p=>p.Visible == true).ToListAsync());
        }


        // GET: Produits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produits.Include(p => p.Categorie)
                    .ThenInclude(c => c.CategorieParent)
                .Include(p => p.MotClef)
                    .ThenInclude(mp => mp.MotClef)
                .Include(p=>p.Valeur)
                    .ThenInclude(v=>v.Attribut)
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
            pc.ListCat = await _context.Categories.ToListAsync();
            pc.ListMotClef = await _context.MotClefs.ToListAsync();
            pc.TousLesAttributs = await _context.Attributs.ToListAsync();
            pc.ListFournisseur= await _context.Fournisseurs.ToListAsync();
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
                    produitcatAdapter.Produit.Visible = true;
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
                        TauxTVA = int.Parse(Request.Form["tauxTVA"]),
                        QuantiteMinCommande = int.Parse(Request.Form["quantite"]),
                        ProduitID = produitcatAdapter.Produit.ID,
                        FournisseurID = fournisseurID
                    };
                    _context.Add(provient);
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
                                        .SingleOrDefaultAsync(m => m.ID == id);
            adaptateur.ListCat = await _context.Categories
                                       .ToListAsync();
            adaptateur.ListMotClef = await _context.MotClefs
                                       .Include(mc=>mc.Produit)
                                       .ToListAsync();
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
        public async Task<IActionResult> Edit(int id, [Bind("ID,Ref,Denomination,Prix,QuantiteEmballage,NBPieceEmballage,TVA,CompteCompta,Description,Marque,QuantiteStock,Image,CategorieID")] Produit produit)
        {
            if (id != produit.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //var produitverif = await _context.Produits.Include(p => p.Possede).SingleOrDefaultAsync(m => m.ID == id);
                    //if (produitverif.Possede.Count >= 0)

                    produit.Visible = false;
                    var images = Request.Form.Files["Image"];
                    if (images.FileName == "")
                    {
                        var produitBD = await _context.Produits
                                .AsNoTracking()
                                .SingleOrDefaultAsync(m => m.ID == id);
                        produit.Image = produitBD.Image;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream();

                        images.OpenReadStream().CopyTo(ms);
                        produit.Image = ms.ToArray();
                    }
                    _context.Update(produit);
                    Produit nouveau = new Produit
                    {
                        Visible = true,
                        CategorieID = produit.CategorieID,
                        Ref = produit.Ref,
                        Denomination = produit.Denomination,
                        Prix = produit.Prix,
                        QuantiteEmballage = produit.QuantiteEmballage,
                        NBPieceEmballage = produit.NBPieceEmballage,
                        TVA = produit.TVA,
                        CompteCompta = produit.CompteCompta,
                        Description = produit.Description,
                        Marque = produit.Marque,
                        QuantiteStock = produit.QuantiteStock,
                        Image = produit.Image,
                    };
                    _context.Add(nouveau);
                    //images = Request.Form.Files["Image"];
                    //if (images.FileName == "")
                    //{
                    //    var produitBD = await _context.Produits
                    //            .AsNoTracking()
                    //            .SingleOrDefaultAsync(m => m.ID == id);
                    //    nouveau.Image = produitBD.Image;
                    //}
                    //else
                    //{
                    //    MemoryStream ms = new MemoryStream();
            
                    //    images.OpenReadStream().CopyTo(ms);
                    //    nouveau.Image = ms.ToArray();
                    //}

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
                            ProduitID = nouveau.ID
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
                        v.ProduitID = nouveau.ID;
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
                        TauxTVA = int.Parse(Request.Form["tauxTVA"]),
                        QuantiteMinCommande = int.Parse(Request.Form["quantite"]),
                        ProduitID = nouveau.ID,
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

    }
}
