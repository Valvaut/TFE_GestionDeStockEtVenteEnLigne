using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models.Metier
{
    public class Historique
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int ProduitID { get; set; }
        public int QteStock  { get; set; }
        public string Action { get; set; }
        public int QteMouv { get; set; }

        public Produit Produit { get; set; }

    }
}
