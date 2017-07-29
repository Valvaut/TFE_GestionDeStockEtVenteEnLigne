using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class AdresseFacturation
    {
        public int ID { get; set; }
        public int AdresseID { get; set; }
        public int FactureID { get; set; }

        public Facture Facture { get; set; }
        public Adresse Adresse { get; set; }
    }
}
