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
                    e.Produit.QuantiteStock = e.Produit.QuantiteStock -e.Quantite;
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

            var commande = await _context.Commandes.SingleOrDefaultAsync(m => m.ID == id);
            if (commande == null)
            {
                return NotFound();
            }
            return View(commande);
        }

        // POST: Commandes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,dateCommade,EnCours,Envoie")] Commande commande)
        {
            if (id != commande.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commande);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommandeExists(commande.ID))
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
            return View(commande);
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
