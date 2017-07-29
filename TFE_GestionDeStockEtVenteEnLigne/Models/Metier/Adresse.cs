using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class Adresse
    {
        public int ID { get; set; }
        public String Localite { get; set; }
        public String Rue { get; set; }
        public int Numero { get; set; }
        public String NumeroBoite { get; set; }
        public String Pays { get; set; }
        public int CodePostal { get; set; }
        public string Comune { get; set; }

        public ICollection<RepHabite> RepHabite { get; set; }
        public ICollection<Domicile> DomicileClient { get; set; }
        public ICollection<Implantation> Implanter { get; set; }
        public ICollection<AdresseFacturation> AdresseFacturation { get; set; }
    }
}
