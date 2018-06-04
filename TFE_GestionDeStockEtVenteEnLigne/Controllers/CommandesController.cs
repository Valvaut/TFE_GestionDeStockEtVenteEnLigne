using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TFE_GestionDeStockEtVenteEnLigne.Data;
using TFE_GestionDeStockEtVenteEnLigne.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TFE_GestionDeStockEtVenteEnLigne.Models.Adaptateur;

namespace TFE_GestionDeStockEtVenteEnLigne.Controllers
{
    [Authorize(Roles = "gestionnaire,client")]
    public class CommandesController : Controller
    {
        private readonly TFEContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommandesController(UserManager<ApplicationUser> userManager, TFEContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Commandes
        public  IActionResult Index()
        {
           
            if (User.IsInRole("gestionnaire"))
            {
                var listeCommande1 = _context.Commandes.Include(a => a.AdresseFacturation).ToList();
                return View(listeCommande1);
            }

            var IdUser = _userManager.GetUserId(User);
            //var IdClient = _context.Clients.Where(c => c.RegisterViewModelID == IdUser).ToArray();
            var listeCommande =  _context.Commandes.Include(a=>a.AdresseFacturation).Where(c=>c.RegisterViewModelID== IdUser).ToList();

            return View(listeCommande);
        }

        // GET: Commandes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var commande = await _context.Commandes.Include(p=>p.Possede).ThenInclude(pr=>pr.produits)
            //    .SingleOrDefaultAsync(m => m.ID == id);
            Commande commande = await _context.Commandes
                                        .Include(c => c.Possede)
                                            .ThenInclude(p=>p.Produit)
                                        .Include(c => c.AdresseFacturation)
                                        .SingleOrDefaultAsync(m => m.ID == id);
            if (User.IsInRole("gestionnaire") || _userManager.GetUserId(User)==commande.RegisterViewModelID)
            {


                Produit produit;
                if (commande == null)
                {
                    return NotFound();
                }
                foreach (var i in commande.Possede)
                {
                    produit = await _context.Produits
                       .Include(p => p.Categorie)
                           .ThenInclude(c => c.CategorieParent)
                       .Include(p => p.MotClef)
                           .ThenInclude(mp => mp.MotClef)
                       .AsNoTracking()
                       .SingleOrDefaultAsync(m => m.ID == i.ProduitID);
                    i.Produit = produit;
                }


                return View(commande);
            }
            return NotFound();
        }

        // GET: Commandes/Create
        public  IActionResult Create()
        {
            var IdUser = _userManager.GetUserId(User);
            var panier = _context.Panier.Where(p=>p.RegisterViewModelID == IdUser).ToArray();
            TempData["prodNotDispo"] = "";
            foreach (var prod in panier)
            {
                var produit = _context.Produits.SingleOrDefault(p=>p.ID == prod.ProduitID);
                if (produit.QuantiteEmballage < prod.Quantite)
                {
                    TempData["prodNotDispo"] += "Produit : " + produit.Denomination + " plus suffisament en stock,";
                }
            }
            if (!(TempData["prodNotDispo"].Equals("")))
            {
                return RedirectToAction("Index","Paniers");
            }
            CommandeAdresseAdaptateur addadp = new CommandeAdresseAdaptateur();
            addadp.ListeAdresse =  _context.Adresses.Include(a => a.DomicileClient).Where(a=>a.DomicileClient.Any(p=>p.RegisterViewModelID == IdUser)).ToList();
            if (addadp.ListeAdresse.Count <= 0 )
            {
                addadp.ListeAdresse.Add(new Adresse());
            }
            return View(addadp);
        }

        // POST: Commandes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CommandeAdresseAdaptateur Adaptateur)
        {
            if (ModelState.IsValid)
            {
                //ajout de l'adresse dans la bd
                var listAdd = _context.Adresses.Include(a => a.DomicileClient).ToList();
                Boolean existe = false;
                int i = 0;
                while (!existe && i < listAdd.Count)
                {
                    if (listAdd[i].Equals(Adaptateur.Adresse))
                        existe = true;
                    else
                        i++;
                }
                if (!existe)
                {
                    _context.Add(Adaptateur.Adresse);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    Adaptateur.Adresse.ID = listAdd[i].ID;
                }
                //récup des infoi utilisateur et du panier
                var IdUser = _userManager.GetUserId(User);
                //var IdClient = _context.Clients.Where(c => c.RegisterViewModelID == IdUser);
                var tFEContext = _context.Panier.Include(p => p.Produit).Where(p => p.RegisterViewModelID == IdUser).ToArray();
                //var Client = IdClient.ToArray();
                Domicile d = new Domicile();
                d.AdresseID = Adaptateur.Adresse.ID;
                //d.ClientID = Client[0].ID;
                d.RegisterViewModelID = IdUser;
                _context.Add(d);
                await _context.SaveChangesAsync();
                //création de la comande
                Commande commande = new Commande();
                commande.AdresseID = Adaptateur.Adresse.ID;
                //commande.ClientID = Client[0].ID;
                commande.RegisterViewModelID = IdUser;
                commande.DateCommade = DateTime.Now;
                if (User.IsInRole("gestionnaire"))
                    commande.EnCours = false;
                else
                    commande.EnCours = true;
                _context.Add(commande);
                await _context.SaveChangesAsync();
                //ajout des produit a la commande
                int idCommande = commande.ID;
                foreach (var e in tFEContext)
                {
                    Possede p = new Possede();
                    p.CommandeID = idCommande;
                    p.ProduitID = e.ProduitID;
                    e.Produit.QuantiteEmballage -= e.Quantite;
                    e.Produit.QuantiteStockTotal = e.Produit.QuantiteEmballage * e.Produit.NBPieceEmballage + e.Produit.QuantiteStock;
                    p.Quantite = e.Quantite;
                    _context.Add(p);
                }
                await _context.SaveChangesAsync();
                //creation de la facture
                Facture f = new Facture();
                f.CommandeID = idCommande;
                _context.Add(f);
                await _context.SaveChangesAsync();
                //vidage du panier
                var panier = _context.Panier.Where(p => p.RegisterViewModelID == IdUser);
                _context.Panier.RemoveRange(panier);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index","Commandes");
        }

        // GET: Commandes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CommandeAdresseAdaptateur data = new CommandeAdresseAdaptateur();
            data.Commande = await _context.Commandes
                .Include(c=>c.AdresseFacturation)
                .Include(c1=>c1.Possede)
                    .ThenInclude(p=>p.Produit)
                .Include(c=>c.Client)
                .SingleOrDefaultAsync(m => m.ID == id);
            ViewData["client"] = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == data.Commande.RegisterViewModelID);

            data.ListeAdresse = await _context.Adresses
                                            .Where(a => a.DomicileClient.Any(d => d.RegisterViewModelID == data.Commande.RegisterViewModelID))
                                            .ToListAsync();
            if (data.Commande == null)
            {
                return NotFound();
            }
            return View(data);
        }

        // POST: Commandes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CommandeAdresseAdaptateur Adaptateur)
        {
            if (id != Adaptateur.Commande.ID)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    var possedeId = Request.Form["possedeId"];
                    
                    List<int> listPossedeId = new List<int>();
                    foreach (var idposs in possedeId)
                    {
                        listPossedeId.Add(int.Parse(idposs));
                    }
                    var quantite = Request.Form["quantite"];
                    List<int> listQuant = new List<int>();
                    foreach (var quant in quantite)
                    {
                        listQuant.Add(int.Parse(quant));
                    }
                    var Produitid = Request.Form["produitId"];
                    List<int> ListproduitId = new List<int>();
                    foreach (var prodid in Produitid)
                    {
                        ListproduitId.Add(int.Parse(prodid));
                    }
                    var commandeid = Request.Form["CommandeId"];
                    List<int> listcomid = new List<int>();
                    foreach (var com in commandeid)
                    {
                        listcomid.Add(int.Parse(com));
                    }
                    List<Possede> listPoss = new List<Possede>();
                    for (int j = 0; j < listPossedeId.Count; j++)
                    {
                        listPoss.Add(new Possede()
                        {
                            ProduitID = ListproduitId[j],
                            CommandeID = listcomid[j],
                            Quantite = listQuant[j],
                        });
                    }

                    Adaptateur.Commande.Possede = listPoss;


                    var aRemove= _context.Possedes.Where(p=>p.CommandeID == Adaptateur.Commande.ID).ToList();
                    _context.Possedes.RemoveRange(aRemove);


                    var listAdd = _context.Adresses.Include(a => a.DomicileClient).ToList();
                    Boolean existe = false;
                    int i = 0;
                    while (!existe && i < listAdd.Count)
                    {
                        if (listAdd[i].Equals(Adaptateur.Commande.AdresseFacturation))
                            existe = true;
                        else
                            i++;
                    }
                    if (!existe)
                    {
                        _context.Add(Adaptateur.Commande.AdresseFacturation);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        Adaptateur.Commande.AdresseID = listAdd[i].ID;
                    }

                    _context.Update(Adaptateur.Commande);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommandeExists(Adaptateur.Commande.ID))
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
            return View(Adaptateur.Commande);
        }
        public async Task<IActionResult> DeleteArticleDemande(int? id, int? idcomm)
        {
            if (id == null)
            {
                return NotFound();
            }
            var p = await _context.Possedes.SingleOrDefaultAsync(c=>c.ID == id);
            _context.Possedes.Remove(p);
            await _context.SaveChangesAsync();

            return RedirectToAction("Edit", new { id = idcomm });
        }
        // GET: Commandes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commande = await _context.Commandes
                .SingleOrDefaultAsync(m => m.ID == id);
            if (commande == null)
            {
                return NotFound();
            }

            return View(commande);
        }

        // POST: Commandes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commande = await _context.Commandes.SingleOrDefaultAsync(m => m.ID == id);
            _context.Commandes.Remove(commande);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CommandeExists(int id)
        {
            return _context.Commandes.Any(e => e.ID == id);
        }

    }
}
