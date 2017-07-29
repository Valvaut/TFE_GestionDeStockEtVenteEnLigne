using System;
using System.Collections.Generic;
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
        public ICollection<Categorie> CategorieEnfant { get; set; }
        public virtual Categorie CategorieParent { get; set; }


    }
}
