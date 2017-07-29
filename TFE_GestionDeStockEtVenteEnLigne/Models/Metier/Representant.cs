using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class Representant
    {
        public int ID { get; set; }
        public String Prenom { get; set; }
        public String Nom { get; set; }
        public String Mail { get; set; }
        public String GSM { get; set; }

        public ICollection<RepHabite> RepHabite { get; set; }
    }
}
