using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class Facture
    {
        public int ID { get; set; }
        public int CommandeID { get; set; }

        [Range(0, int.MaxValue)]
        public int Numero { get; set; }
        [Display(Name = "Date de paiement")]
        public DateTime DatePaiement{ get; set; }

        public Commande Commande { get; set; }

    }
}
