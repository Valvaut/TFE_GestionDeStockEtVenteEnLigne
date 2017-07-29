using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class Valeur
    {
        public int ID { get; set; }
        public int ProduitID { get; set; }
        public int AttributID { get; set; }

        public String Valeurs { get; set; }
        public Produit Produit { get; set; }
        public Attribut Attribut { get; set; }
    }
}
