using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class Categorie
    {
        public int ID { get; set; }
        public int? CategorieParentID { get; set; }
        public String Nom { get; set; }

        public ICollection<Produit> Produits { get; set; }
        [Display(Name = "Catégories Enfants")]
        public ICollection<Categorie> CategorieEnfant { get; set; }
        [Display(Name = "Catégorie Parents")]
        public virtual Categorie CategorieParent { get; set; }


    }
}
