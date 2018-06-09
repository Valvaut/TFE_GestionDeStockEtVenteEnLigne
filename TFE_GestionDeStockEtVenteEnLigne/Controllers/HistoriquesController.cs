using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TFE_GestionDeStockEtVenteEnLigne.Data;
using TFE_GestionDeStockEtVenteEnLigne.Models.Metier;

namespace TFE_GestionDeStockEtVenteEnLigne.Controllers
{
    [Authorize(Roles = "gestionnaire")]
    public class HistoriquesController : Controller
    {
        private readonly TFEContext _context;

        public HistoriquesController(TFEContext context)
        {
            _context = context;    
        }

        // GET: Historiques
        public async Task<IActionResult> Index()
        {
            var tFEContext = _context.Historique.Include(h => h.Produit);
            return View(await tFEContext.ToListAsync());
        }

        // GET: Historiques/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historique = await _context.Historique
                .Include(h => h.Produit)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (historique == null)
            {
                return NotFound();
            }

            return View(historique);
        }

        // GET: Historiques/Create
        public IActionResult Create()
        {
            ViewData["ProduitID"] = new SelectList(_context.Produits, "ID", "ID");
            return View();
        }

        // POST: Historiques/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Date,ProduitID,QteStock,Action,QteMouv")] Historique historique)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historique);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ProduitID"] = new SelectList(_context.Produits, "ID", "ID", historique.ProduitID);
            return View(historique);
        }

        // GET: Historiques/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historique = await _context.Historique.SingleOrDefaultAsync(m => m.ID == id);
            if (historique == null)
            {
                return NotFound();
            }
            ViewData["ProduitID"] = new SelectList(_context.Produits, "ID", "ID", historique.ProduitID);
            return View(historique);
        }

        // POST: Historiques/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Date,ProduitID,QteStock,Action,QteMouv")] Historique historique)
        {
            if (id != historique.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historique);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoriqueExists(historique.ID))
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
            ViewData["ProduitID"] = new SelectList(_context.Produits, "ID", "ID", historique.ProduitID);
            return View(historique);
        }

        // GET: Historiques/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historique = await _context.Historique
                .Include(h => h.Produit)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (historique == null)
            {
                return NotFound();
            }

            return View(historique);
        }

        // POST: Historiques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historique = await _context.Historique.SingleOrDefaultAsync(m => m.ID == id);
            _context.Historique.Remove(historique);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool HistoriqueExists(int id)
        {
            return _context.Historique.Any(e => e.ID == id);
        }
    }
}
