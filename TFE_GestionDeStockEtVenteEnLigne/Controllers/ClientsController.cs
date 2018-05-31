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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TFE_GestionDeStockEtVenteEnLigne.Controllers
{
    [Authorize(Roles = "gestionnaire")]
    public class ClientsController : Controller
    {
        private readonly TFEContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ClientsController(TFEContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            var user = this.User;
            if (user.IsInRole("gestionnaire"))
            {
                return View(await _userManager.Users.ToListAsync());
            }
            return View(await _userManager.Users.ToListAsync());

        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _userManager.Users
                .Include(c=>c.Domicile)
                .Include(c=> c.Commande)
                .ThenInclude(com=>com.Possede)
                .SingleOrDefaultAsync(m => m.Id == id);
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
        public  IActionResult Create()
        {
      
            return View();
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
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _userManager.Users.SingleOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Edit(string id, [Bind("Id,Nom,Prenom,Tel,Newsletter,Email,EmailConfirmed,PhoneNumber,PhoneNumberConfirmed,LockoutEnd,LockoutEnabled")] ApplicationUser client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _userManager.UpdateAsync(client);
                    var user = _userManager.Users.SingleOrDefault(u=>u.Id == id);
                    user.Nom = client.Nom;
                    user.Prenom = client.Prenom;
                    user.Tel = client.Tel;
                    user.Newsletter = client.Newsletter;
                    user.Email = client.Email;
                    user.EmailConfirmed = client.EmailConfirmed;
                    user.NormalizedEmail = client.Email;
                    user.UserName = client.Email;
                    user.PhoneNumber = client.PhoneNumber;
                    user.PhoneNumberConfirmed = client.PhoneNumberConfirmed;
                    user.LockoutEnd = client.LockoutEnd;
                    user.LockoutEnabled = client.LockoutEnabled;


                    await _userManager.UpdateAsync(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception e)
                {
                }
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _userManager.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var client = await _userManager.Users.SingleOrDefaultAsync(m => m.Id == id);
            await _userManager.DeleteAsync(client);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ID == id);
        }
        private bool ClientExists(string id)
        {
            return _userManager.Users.Any(e => e.Id == id);
        }
    }
}
