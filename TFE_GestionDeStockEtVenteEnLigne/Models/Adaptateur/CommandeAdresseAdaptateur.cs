using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models.Adaptateur
{
    public class CommandeAdresseAdaptateur
    {
        public Commande Commande { get; set; }
        public Adresse Adresse { get; set; }
    }
}
