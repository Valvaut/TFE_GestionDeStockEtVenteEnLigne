using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class Adresse
    {
        public int ID { get; set; }
        public String Localite { get; set; }
        public String Rue { get; set; }

        [Range(0,int.MaxValue)]
        public int Numero { get; set; }
        public String NumeroBoite { get; set; }
        public String Pays { get; set; }

        [Range(0, int.MaxValue)]
        public int CodePostal { get; set; }
        public string Comune { get; set; }

        public ICollection<RepHabite> RepHabite { get; set; }
        public ICollection<Domicile> DomicileClient { get; set; }
        public ICollection<Implantation> Implanter { get; set; }
        public ICollection<Commande> ListeCommande { get; set; }
    }
}
