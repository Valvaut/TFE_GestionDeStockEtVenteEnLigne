using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class Domicile
    {
        public int ID { get; set; }
        public int AdresseID { get; set; }
        public int ClientID { get; set; }

        public Client Client { get; set; }
        public Adresse Adresse { get; set; }
    }
}
