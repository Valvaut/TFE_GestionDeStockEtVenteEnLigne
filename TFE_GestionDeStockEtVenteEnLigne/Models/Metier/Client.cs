using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFE_GestionDeStockEtVenteEnLigne.Models.AccountViewModels;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class Client
    {
        public int ID { get; set; }
        public String RegisterViewModelID { get; set; }
        public String Nom { get; set; }
        public String Prenom { get; set; }
        public String Mail { get; set; }
        public String NumTva { get; set; }
        public String Tel { get; set; }
        public String NumeroClient{ get; set; }


        public ICollection<Domicile> Domicile { get; set; }
        public ICollection<Commande> Commande { get; set; }

    }
}
