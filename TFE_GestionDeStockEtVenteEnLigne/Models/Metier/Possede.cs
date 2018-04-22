using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class Possede
    {
        public int ID { get; set; }
        public int CommandeID { get; set; }

        public int ProduitID { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantite { get; set; }

        public Commande Commande { get; set; }
        public Produit Produit { get; set; }
    }
}
