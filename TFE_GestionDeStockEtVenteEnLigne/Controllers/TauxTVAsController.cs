using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TFE_GestionDeStockEtVenteEnLigne.Data;
using TFE_GestionDeStockEtVenteEnLigne.Models.Metier;

namespace TFE_GestionDeStockEtVenteEnLigne.Controllers
{
    public class TauxTVAsController : Controller
    {
        private readonly TFEContext _context;

        public TauxTVAsController(TFEContext context)
        {
            _context = context;    
        }

        // GET: TauxTVAs
        public async Task<IActionResult> Index()
        {
            return View(await _context.TVA.ToListAsync());
        }

        // GET: TauxTVAs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tauxTVA = await _context.TVA
                .SingleOrDefaultAsync(m => m.ID == id);
            if (tauxTVA == null)
            {
                return NotFound();
            }

            return View(tauxTVA);
        }

        // GET: TauxTVAs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TauxTVAs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Valeur")] TauxTVA tauxTVA)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tauxTVA);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tauxTVA);
        }

        // GET: TauxTVAs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tauxTVA = await _context.TVA.SingleOrDefaultAsync(m => m.ID == id);
            if (tauxTVA == null)
            {
                return NotFound();
            }
            return View(tauxTVA);
        }

        // POST: TauxTVAs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Valeur")] TauxTVA tauxTVA)
        {
            if (id != tauxTVA.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tauxTVA);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TauxTVAExists(tauxTVA.ID))
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
            return View(tauxTVA);
        }

        // GET: TauxTVAs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tauxTVA = await _context.TVA
                .SingleOrDefaultAsync(m => m.ID == id);
            if (tauxTVA == null)
            {
                return NotFound();
            }

            return View(tauxTVA);
        }

        // POST: TauxTVAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tauxTVA = await _context.TVA.SingleOrDefaultAsync(m => m.ID == id);
            _context.TVA.Remove(tauxTVA);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TauxTVAExists(int id)
        {
            return _context.TVA.Any(e => e.ID == id);
        }
    }
}
