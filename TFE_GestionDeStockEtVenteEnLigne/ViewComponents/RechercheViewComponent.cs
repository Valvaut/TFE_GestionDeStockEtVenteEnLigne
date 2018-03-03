using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFE_GestionDeStockEtVenteEnLigne.Data;
using TFE_GestionDeStockEtVenteEnLigne.Models;
using Microsoft.EntityFrameworkCore;

namespace TFE_GestionDeStockEtVenteEnLigne.ViewComponents
{
    public class RechercheViewComponent: ViewComponent
    {
        private readonly TFEContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RechercheViewComponent(UserManager<ApplicationUser> userManager, TFEContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categorie = await _context.Categories.Include(c => c.CategorieEnfant).ToListAsync();
            return View(categorie);
        }
    }
}
