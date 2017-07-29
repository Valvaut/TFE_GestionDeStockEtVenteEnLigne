using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class Commande
    {
        public int ID { get; set; }
        public DateTime DateCommade{ get; set; }
        public bool EnCours { get; set; }
        public DateTime Envoie { get; set; }

        public virtual Facture Facture { get; set; }
        public AdresseFacturation AdresseFacturation { get; set; }
        public Client Client{ get; set; }
        public ICollection<Possede> Possede { get; set; }
    }
}
