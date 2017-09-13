using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TFE_GestionDeStockEtVenteEnLigne.Data;
using TFE_GestionDeStockEtVenteEnLigne.Models;
using TFE_GestionDeStockEtVenteEnLigne.Models.Adaptateur;
using Microsoft.AspNetCore.Authorization;

namespace TFE_GestionDeStockEtVenteEnLigne.Controllers
{
    [Authorize(Roles = "gestionnaire")]
    public class ClientsController : Controller
    {
        private readonly TFEContext _context;

        public ClientsController(TFEContext context)
        {
            _context = context;    
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c=>c.Domicile)
                .Include(c=> c.Commande)
                .ThenInclude(com=>com.Possede)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (client == null)
            {
                return NotFound();
            }
            //Adresse adresse;
            foreach (var c in client.Domicile)
            {
                c.Adresse = await _context.Adresses.SingleOrDefaultAsync(a => a.ID == c.AdresseID);
            }

            foreach (var c in client.Commande)
            {
                foreach (var i in c.Possede)
                {
                    i.Produit = await _context.Produits
                               .Include(p => p.Categorie)
                                   .ThenInclude(d => d.CategorieParent)
                               .Include(p => p.MotClef)
                                   .ThenInclude(mp => mp.MotClef)
                               .AsNoTracking()
                               .SingleOrDefaultAsync(m => m.ID == i.ProduitID);
                }
            }
            return View(client);
        }

        // GET: Clients/Create
        public async Task<IActionResult> Create()
        {
            ClientAdaptateur c = new ClientAdaptateur();
            c.ListeAdresse = await _context.Adresses.ToListAsync();
            return View(c);
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nom,Prenom,Mail,Adresse,NumTva,Tel,NumeroClient")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.SingleOrDefaultAsync(m => m.ID == id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nom,Prenom,Mail,Adresse,NumTva,Tel,NumeroClient")] Client client)
        {
            if (id != client.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ID))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .SingleOrDefaultAsync(m => m.ID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.SingleOrDefaultAsync(m => m.ID == id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ID == id);
        }
    }
}
