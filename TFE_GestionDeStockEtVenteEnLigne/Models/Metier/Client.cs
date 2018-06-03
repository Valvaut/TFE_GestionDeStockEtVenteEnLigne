using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Prénom")]
        public String Prenom { get; set; }
        [Display(Name = "Numéro de TVA")]
        public String NumTva { get; set; }
        [Display(Name = "Téléphone")]
        public String Tel { get; set; }
        public bool Newsletter { get; set; }

        public ICollection<Domicile> Domicile { get; set; }
        public ICollection<Commande> Commande { get; set; }
        public ICollection<Panier> Panier { get; set; }

    }
}
