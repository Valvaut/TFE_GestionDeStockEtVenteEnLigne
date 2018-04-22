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
    public class CategoriesController : Controller
    {
        private readonly TFEContext _context;

        public CategoriesController(TFEContext context)
        {
            _context = context;    
        }
        // GET: Categories
        public async Task<IActionResult> Recherche()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorie = await _context.Categories
                .SingleOrDefaultAsync(m => m.ID == id);
            if (categorie == null)
            {
                return NotFound();
            }

            return View(categorie);
        }

        // GET: Categories/Create
        public async Task<IActionResult> Create()
        {
            Categorie c = new Categorie();
            c.CategorieEnfant = await _context.Categories.ToListAsync();
            return View(c);
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nom")] Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categorie);
                String cat= Request.Form["CategorieParentID"];
                if(cat!="Aucune")
                {
                    int CatID = int.Parse(cat);
                    categorie.CategorieParentID = CatID;
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(categorie);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorie = await _context.Categories.SingleOrDefaultAsync(m => m.ID == id);
            if (categorie == null)
            {
                return NotFound();
            }
            return View(categorie);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nom")] Categorie categorie)
        {
            if (id != categorie.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categorie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategorieExists(categorie.ID))
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
            return View(categorie);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorie = await _context.Categories
                .SingleOrDefaultAsync(m => m.ID == id);
            if (categorie == null)
            {
                return NotFound();
            }

            return View(categorie);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categorie = await _context.Categories.SingleOrDefaultAsync(m => m.ID == id);
            _context.Categories.Remove(categorie);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CategorieExists(int id)
        {
            return _context.Categories.Any(e => e.ID == id);
        }


    }
}
