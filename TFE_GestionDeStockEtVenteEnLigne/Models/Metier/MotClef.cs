using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class MotClef
    {
        public int ID { get; set; }
        public String Valeur { get; set; }
        
        public ICollection<ProduitMotClef> Produit { get; set; }

    }
}
