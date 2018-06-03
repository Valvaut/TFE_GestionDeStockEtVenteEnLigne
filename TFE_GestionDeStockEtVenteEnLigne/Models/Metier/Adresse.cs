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
        [Display(Name = "Code Postal")]
        public int CodePostal { get; set; }
        [Display(Name = "Commune")]
        public string Comune { get; set; }

        public ICollection<RepHabite> RepHabite { get; set; }
        public ICollection<Domicile> DomicileClient { get; set; }
        public ICollection<Implantation> Implanter { get; set; }
        public ICollection<Commande> ListeCommande { get; set; }

        public Boolean Equals(Adresse adr)
        {
            return Localite == adr.Localite && Rue == adr.Rue && Numero == adr.Numero && NumeroBoite == adr.NumeroBoite && Pays == adr.Pays  && CodePostal == adr.CodePostal && Comune == adr.Comune;
        }
        
        public override string ToString()
        {
            return Localite + "," + Rue + "," + Numero + "," + NumeroBoite + "," + Pays + "," + CodePostal + "," + Comune;
        }
    }
}
