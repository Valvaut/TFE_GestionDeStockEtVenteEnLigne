using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class Provient
    {
        public int ID { get; set; }
        public int ProduitID { get; set; }
        public int FournisseurID { get; set; }

        [Range(0, double.MaxValue)]
        public Double Prix { get; set; }

        [Range(0, int.MaxValue)]
        public int TauxTVA { get; set; }

        [Range(0, int.MaxValue)]
        public int QuantiteMinCommande { get; set; }

        public Produit Produit { get; set; }
        public Fournisseur Fournisseur{ get; set; }
    }
}
