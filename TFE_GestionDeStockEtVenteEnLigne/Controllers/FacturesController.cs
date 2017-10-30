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
using Microsoft.AspNetCore.NodeServices;
using System.Net.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace TFE_GestionDeStockEtVenteEnLigne.Controllers
{
    
    public class FacturesController : Controller
    {
        private readonly TFEContext _context;

        public FacturesController(TFEContext context)
        {
            _context = context;    
        }

        // GET: Factures
        public async Task<IActionResult> Index()
        {
            var tFEContext = _context.Factures.Include(f => f.Commande);
            return View(await tFEContext.ToListAsync());
        }

        // GET: Factures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facture = await _context.Factures
                .Include(f => f.Commande)
                    .ThenInclude(c=>c.Possede)
                        .ThenInclude(p=>p.Produit)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (facture == null)
            {
                return NotFound();
            }

            return View(facture);
        }

        // GET: Factures/Create
        public IActionResult Create()
        {
            ViewData["ID"] = new SelectList(_context.Set<Commande>(), "ID", "ID");
            return View();
        }

        // POST: Factures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Numero,DatePaiement")] Facture facture)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facture);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ID"] = new SelectList(_context.Set<Commande>(), "ID", "ID", facture.ID);
            return View(facture);
        }

        // GET: Factures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facture = await _context.Factures.SingleOrDefaultAsync(m => m.ID == id);
            if (facture == null)
            {
                return NotFound();
            }
            ViewData["ID"] = new SelectList(_context.Set<Commande>(), "ID", "ID", facture.ID);
            return View(facture);
        }

        // POST: Factures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Numero,DatePaiement")] Facture facture)
        {
            if (id != facture.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FactureExists(facture.ID))
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
            ViewData["ID"] = new SelectList(_context.Set<Commande>(), "ID", "ID", facture.ID);
            return View(facture);
        }

        // GET: Factures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facture = await _context.Factures
                .Include(f => f.Commande)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (facture == null)
            {
                return NotFound();
            }

            return View(facture);
        }

        // POST: Factures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facture = await _context.Factures.SingleOrDefaultAsync(m => m.ID == id);
            _context.Factures.Remove(facture);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FactureExists(int id)
        {
            return _context.Factures.Any(e => e.ID == id);
        }
        [HttpGet]
        public async Task<IActionResult> Facture([FromServices] INodeServices nodeServices)
        {
            HttpClient hc = new HttpClient();
            var host = Request.Host.ToString()+ Request.Path.ToString()+ Request.QueryString.ToString();
            var b = Request.QueryString;

            var htmlContent = await hc.GetStringAsync($"http://{Request.Host}/Factures/Details/2");

            var result = await nodeServices.InvokeAsync<byte[]>("./pdf", htmlContent);

            HttpContext.Response.ContentType = "application/pdf";

            HttpContext.Response.Headers.Add("x-filename", "report.pdf");
            HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "x-filename");
            HttpContext.Response.Body.Write(result, 0, result.Length);
            return new ContentResult();
        }
    }
}
