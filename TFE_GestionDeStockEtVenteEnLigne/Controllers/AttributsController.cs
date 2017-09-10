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

namespace TFE_GestionDeStockEtVenteEnLigne.Controllers
{
    //[Authorize(Roles = "gestionnaire")]
    public class AttributsController : Controller
    {
        private readonly TFEContext _context;

        public AttributsController(TFEContext context)
        {
            _context = context;    
        }

        // GET: Attributs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Attributs.ToListAsync());
        }

        // GET: Attributs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attribut = await _context.Attributs
                .SingleOrDefaultAsync(m => m.ID == id);
            if (attribut == null)
            {
                return NotFound();
            }

            return View(attribut);
        }

        // GET: Attributs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Attributs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nom,Mesure")] Attribut attribut)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attribut);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(attribut);
        }

        // GET: Attributs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attribut = await _context.Attributs.SingleOrDefaultAsync(m => m.ID == id);
            if (attribut == null)
            {
                return NotFound();
            }
            return View(attribut);
        }

        // POST: Attributs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nom,Mesure")] Attribut attribut)
        {
            if (id != attribut.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attribut);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttributExists(attribut.ID))
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
            return View(attribut);
        }

        // GET: Attributs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attribut = await _context.Attributs
                .SingleOrDefaultAsync(m => m.ID == id);
            if (attribut == null)
            {
                return NotFound();
            }

            return View(attribut);
        }

        // POST: Attributs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attribut = await _context.Attributs.SingleOrDefaultAsync(m => m.ID == id);
            _context.Attributs.Remove(attribut);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AttributExists(int id)
        {
            return _context.Attributs.Any(e => e.ID == id);
        }
    }
}
