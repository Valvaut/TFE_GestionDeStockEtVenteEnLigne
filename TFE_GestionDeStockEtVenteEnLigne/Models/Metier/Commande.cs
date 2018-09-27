using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class Commande
    {
        public int ID { get; set; }
        public int? ClientID { get; set; }
        public String RegisterViewModelID { get; set; }
        public int? AdresseID { get; set; }
        [Display(Name = "Date de commande")]
        public DateTime DateCommade{ get; set; }
        [Display(Name = "Commande en cours")]
        public bool EnCours { get; set; }
        [Display(Name = "Commande envoyée")]
        public DateTime Envoie { get; set; }

        public virtual Facture Facture { get; set; }
        [Display(Name = "Adresse de facturation")]
        public Adresse AdresseFacturation { get; set; }
        public Client Client{ get; set; }
        public ICollection<Possede> Possede { get; set; }

        public Commande()
        {
            Possede = new List<Possede>();
        }
    }
}
