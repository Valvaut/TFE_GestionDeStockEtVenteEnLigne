using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class RepHabite
    {
        public int ID { get; set;}
        public int AdresseID { get; set; }
        public int RepresentantID { get; set; }

        public Representant Representant { get; set; }
        public Adresse Adresse { get; set; }
    }
}
