using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TFE_GestionDeStockEtVenteEnLigne.Data;
using TFE_GestionDeStockEtVenteEnLigne.Models;

namespace TFE_GestionDeStockEtVenteEnLigne.Controllers
{
    public class HorrairesController : Controller
    {
        private readonly TFEContext _context;

        public HorrairesController(TFEContext context)
        {
            _context = context;    
        }

        // GET: Horraires
        public IActionResult Index()
        {
            HorraireEventAdapter Adapter = new HorraireEventAdapter();

            Adapter.ListEvent=  _context.Evenement.ToList();
            Adapter.Horraire = _context.Horraire.ToList();
            return View( Adapter);
        }

        // GET: Horraires/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horraire = await _context.Horraire
                .SingleOrDefaultAsync(m => m.ID == id);
            if (horraire == null)
            {
                return NotFound();
            }

            return View(horraire);
        }

        // GET: Horraires/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Horraires/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Lundi,Mardi,Mercredi,Jeudi,Vendredi,Samedi,Dimanche")] Horraire horraire)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horraire);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(horraire);
        }

        // GET: Horraires/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horraire = await _context.Horraire.SingleOrDefaultAsync(m => m.ID == id);
            if (horraire == null)
            {
                return NotFound();
            }
            return View(horraire);
        }

        // POST: Horraires/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Lundi,Mardi,Mercredi,Jeudi,Vendredi,Samedi,Dimanche")] Horraire horraire)
        {
            if (id != horraire.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horraire);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorraireExists(horraire.ID))
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
            return View(horraire);
        }

        // GET: Horraires/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horraire = await _context.Horraire
                .SingleOrDefaultAsync(m => m.ID == id);
            if (horraire == null)
            {
                return NotFound();
            }

            return View(horraire);
        }

        // POST: Horraires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horraire = await _context.Horraire.SingleOrDefaultAsync(m => m.ID == id);
            _context.Horraire.Remove(horraire);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool HorraireExists(int id)
        {
            return _context.Horraire.Any(e => e.ID == id);
        }
    }
}
