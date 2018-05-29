using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TFE_GestionDeStockEtVenteEnLigne.Data;
using TFE_GestionDeStockEtVenteEnLigne.Models.Metier;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TFE_GestionDeStockEtVenteEnLigne.Models;

namespace TFE_GestionDeStockEtVenteEnLigne.Controllers
{
    [Authorize(Roles = "gestionnaire,client")]
    public class PaniersController : Controller
    {
        private readonly TFEContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaniersController(UserManager<ApplicationUser> userManager,TFEContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Paniers
        public async Task<IActionResult> Index()
        {
            var IdUser = _userManager.GetUserId(User);
            var tFEContext = _context.Panier.Include(p => p.Produit).Where(p=>p.RegisterViewModelID == IdUser);
            return View(await tFEContext.ToListAsync());
        }

        // GET: Paniers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panier = await _context.Panier
                .Include(p => p.Produit)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (panier == null)
            {
                return NotFound();
            }

            return View(panier);
        }

        // GET: Paniers/Create
        public IActionResult Create()
        {
            ViewData["ProduitID"] = new SelectList(_context.Produits, "ID", "ID");
            return View();
        }

        // POST: Paniers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,RegisterViewModelID,ProduitID")] Panier panier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(panier);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ProduitID"] = new SelectList(_context.Produits, "ID", "ID", panier.ProduitID);
            return View(panier);
        }

        // GET: Paniers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panier = await _context.Panier.SingleOrDefaultAsync(m => m.ID == id);
            if (panier == null)
            {
                return NotFound();
            }
            ViewData["ProduitID"] = new SelectList(_context.Produits, "ID", "ID", panier.ProduitID);
            return View(panier);
        }

        // POST: Paniers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,RegisterViewModelID,ProduitID")] Panier panier)
        {
            if (id != panier.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(panier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PanierExists(panier.ID))
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
            ViewData["ProduitID"] = new SelectList(_context.Produits, "ID", "ID", panier.ProduitID);
            return View(panier);
        }

        // GET: Paniers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panier = await _context.Panier
                .Include(p => p.Produit)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (panier == null)
            {
                return NotFound();
            }

            return View(panier);
        }
        public async Task<IActionResult> DeletePanier()
        {
            var IdUser = _userManager.GetUserId(User);
            var panier =  _context.Panier.ToArray();
            _context.Panier.RemoveRange(panier);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: Paniers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var panier = await _context.Panier.SingleOrDefaultAsync(m => m.ID == id);
            _context.Panier.Remove(panier);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PanierExists(int id)
        {
            return _context.Panier.Any(e => e.ID == id);
        }
    }
}
