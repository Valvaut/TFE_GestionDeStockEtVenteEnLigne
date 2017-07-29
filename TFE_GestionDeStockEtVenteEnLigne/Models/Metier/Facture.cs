using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class Facture
    {
        public int ID { get; set; }
        public int CommandeID { get; set; }
        public int Numero { get; set; }
        public DateTime DatePaiement{ get; set; }

        public Commande Commande { get; set; }

    }
}
