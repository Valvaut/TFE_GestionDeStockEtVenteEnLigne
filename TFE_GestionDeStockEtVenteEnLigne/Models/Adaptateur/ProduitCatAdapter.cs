using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    [Bind("Produit")]
    public class ProduitCatAdapter
    {
        public List<Categorie> ListCat { get; set; }
        public Produit Produit { get; set; }
        public List<MotClef> ListMotClef { get; set; }
        public int MotClef { get; set; }
        public List<Attribut> TousLesAttributs { get; set; }
        public List<Fournisseur> ListFournisseur { get; set; }
        public int FournisseurID { get; set; }
        public List<IFormFile> Images{get; set;}

        public ProduitCatAdapter()
        {
            Produit = new Produit();
            ListMotClef = new List<MotClef>();
            ListFournisseur = new List<Fournisseur>();
        }

    }
}
