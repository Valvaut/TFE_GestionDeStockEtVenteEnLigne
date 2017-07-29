using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class ProduitMotClef
    {
        public int ID { get; set; }
        public int ProduitID{ get; set; }
        public int MotClefId { get; set; }

        public Produit Produit { get; set; }
        public MotClef MotClef { get; set; }

    }
}
