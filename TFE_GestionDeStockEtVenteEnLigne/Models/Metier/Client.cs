using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFE_GestionDeStockEtVenteEnLigne.Models.AccountViewModels;
using TFE_GestionDeStockEtVenteEnLigne.Models.Metier;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class Client
    {
        public int ID { get; set; }
        public String RegisterViewModelID { get; set; }
        public String Nom { get; set; }
        public String Prenom { get; set; }

        public String NumTva { get; set; }
        public String Tel { get; set; }

        public ICollection<Domicile> Domicile { get; set; }
        public ICollection<Commande> Commande { get; set; }
        public ICollection<Panier> Panier { get; set; }

    }
}
