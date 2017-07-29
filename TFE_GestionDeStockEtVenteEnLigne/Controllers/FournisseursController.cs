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

namespace TFE_GestionDeStockEtVenteEnLigne.Controllers
{
    public class FournisseursController : Controller
    {
        private readonly TFEContext _context;

        public FournisseursController(TFEContext context)
        {
            _context = context;    
        }

        // GET: Fournisseurs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fournisseurs.ToListAsync());
        }

        // GET: Fournisseurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fournisseur = await _context.Fournisseurs
                .SingleOrDefaultAsync(m => m.ID == id);
            if (fournisseur == null)
            {
                return NotFound();
            }

            return View(fournisseur);
        }

        // GET: Fournisseurs/Create
        public async Task<IActionResult> Create()
        {
            FournisseurAdaptateur f = new FournisseurAdaptateur();
            f.ListAdresse = await _context.Adresses.ToListAsync();
            f.ListeRepresentant= await _context.Repesentants.ToListAsync();
            return View(f);
        }

        // POST: Fournisseurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nom,Reference,Mail,Telephone,Fax,NumCompte,NumTva")] Fournisseur fournisseur)
        {
            
            if (ModelState.IsValid)
            {
                
                _context.Add(fournisseur);
                await _context.SaveChangesAsync();

                var tableauAdresse = Request.Form["addresse"];
                if (tableauAdresse.Count > 0)
                {
                    List<int> listeAddresse = new List<int>();
                    foreach (var mot in tableauAdresse)
                    {
                        listeAddresse.Add(int.Parse(mot));
                    }
                    foreach (int adresse in listeAddresse)
                    {
                        Implantation imp = new Implantation();
                        imp.AdresseID = adresse;
                        imp.FournisseurID = fournisseur.ID;
                        _context.Add(imp);
                        await _context.SaveChangesAsync();
                    }
                }
                
                var tableauRepresentant = Request.Form["Representant"];
                if(tableauRepresentant.Count>0)
                {
                    List<int> ListeRepresentant = new List<int>();
                    foreach (var mot in tableauRepresentant)
                    {
                        ListeRepresentant.Add(int.Parse(mot));
                    }
                    foreach (int Representant in ListeRepresentant)
                    {
                        Contact contact = new Contact();
                        contact.RepresentantID = Representant;
                        contact.FournisseurID = fournisseur.ID;
                        _context.Add(contact);
                        await _context.SaveChangesAsync();
                    }
                }
                
                return RedirectToAction("Index");
            }
            return View(fournisseur);
        }

        // GET: Fournisseurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fournisseur = await _context.Fournisseurs.SingleOrDefaultAsync(m => m.ID == id);
            if (fournisseur == null)
            {
                return NotFound();
            }
            return View(fournisseur);
        }

        // POST: Fournisseurs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nom,Reference,Mail,Telephone,Fax,NumCompte,NumTva")] Fournisseur fournisseur)
        {
            if (id != fournisseur.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fournisseur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FournisseurExists(fournisseur.ID))
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
            return View(fournisseur);
        }

        // GET: Fournisseurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fournisseur = await _context.Fournisseurs
                .SingleOrDefaultAsync(m => m.ID == id);
            if (fournisseur == null)
            {
                return NotFound();
            }

            return View(fournisseur);
        }

        // POST: Fournisseurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fournisseur = await _context.Fournisseurs.SingleOrDefaultAsync(m => m.ID == id);
            _context.Fournisseurs.Remove(fournisseur);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FournisseurExists(int id)
        {
            return _context.Fournisseurs.Any(e => e.ID == id);
        }
    }
}
