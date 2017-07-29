using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class Provient
    {
        public int ID { get; set; }
        public int ProduitID { get; set; }
        public int FournisseurID { get; set; }
        public int Prix { get; set; }
        public int TauxTVA { get; set; }
        public int QuantiteMinCommande { get; set; }

        public Produit Produit { get; set; }
        public Fournisseur Fournisseur{ get; set; }
    }
}
