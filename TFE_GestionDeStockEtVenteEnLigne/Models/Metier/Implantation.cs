using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class Implantation
    {
        public int ID { get; set; }
        public int AdresseID { get; set; }
        public int FournisseurID { get; set; }

        public Fournisseur Fournisseur { get; set; }
        public Adresse Adresse { get; set; }
    }
}
