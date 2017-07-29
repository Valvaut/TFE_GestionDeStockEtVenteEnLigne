using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class Travail
    {
        public int ID { get; set; }
        public int RepresentantID { get; set; }
        public int FournisseurID { get; set; }

        public Fournisseur Fournisseur { get; set; }
        public Representant Representant { get; set; }
    }
}
