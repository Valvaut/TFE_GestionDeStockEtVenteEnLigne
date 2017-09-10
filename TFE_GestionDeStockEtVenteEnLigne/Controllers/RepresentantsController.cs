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
using TFE_GestionDeStockEtVenteEnLigne.Models.Metier;
using Microsoft.AspNetCore.Authorization;

namespace TFE_GestionDeStockEtVenteEnLigne.Controllers
{
    [Authorize(Roles = "gestionnaire")]
    public class RepresentantsController : Controller
    {
        private readonly TFEContext _context;

        public RepresentantsController(TFEContext context)
        {
            _context = context;    
        }

        // GET: Representants
        public async Task<IActionResult> Index()
        {
            return View(await _context.Repesentants.ToListAsync());
        }

        // GET: Representants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var representant = await _context.Repesentants
                .SingleOrDefaultAsync(m => m.ID == id);
            if (representant == null)
            {
                return NotFound();
            }

            return View(representant);
        }

        // GET: Representants/Create
        public async Task<IActionResult> Create()
        {
            RepresentantAdaptateur r = new RepresentantAdaptateur();
            r.ListAdresse = await _context.Adresses.ToListAsync();
            r.ListFournisseur= await _context.Fournisseurs.ToListAsync();
            return View(r);
        }

        // POST: Representants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Prenom,Nom,Mail,GSM")] Representant representant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(representant);
                var tableauFournisseur = Request.Form["Fournisseur"];
                List<int> ListeFournisseur = new List<int>();
                foreach (var mot in tableauFournisseur)
                {
                    ListeFournisseur.Add(int.Parse(mot));
                }
                foreach (int Fournisseur in ListeFournisseur)
                {
                    Contact contact = new Contact();
                    contact.RepresentantID = representant.ID;
                    contact.FournisseurID = Fournisseur;
                    _context.Add(contact);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(representant);
        }

        // GET: Representants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var representant = await _context.Repesentants.SingleOrDefaultAsync(m => m.ID == id);
            if (representant == null)
            {
                return NotFound();
            }
            return View(representant);
        }

        // POST: Representants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Prenom,Nom,Mail,GSM")] Representant representant)
        {
            if (id != representant.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(representant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepresentantExists(representant.ID))
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
            return View(representant);
        }

        // GET: Representants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var representant = await _context.Repesentants
                .SingleOrDefaultAsync(m => m.ID == id);
            if (representant == null)
            {
                return NotFound();
            }

            return View(representant);
        }

        // POST: Representants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var representant = await _context.Repesentants.SingleOrDefaultAsync(m => m.ID == id);
            _context.Repesentants.Remove(representant);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool RepresentantExists(int id)
        {
            return _context.Repesentants.Any(e => e.ID == id);
        }
    }
}
