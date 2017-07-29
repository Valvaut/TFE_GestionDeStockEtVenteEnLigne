using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class Fournisseur
    {
        public int ID { get; set; }
        public String Nom { get; set; }
        public String Reference { get; set; }
        public String Mail { get; set; }
        public String Telephone { get; set; }
        public String Fax { get; set; }
        public String NumCompte { get; set; }
        public String NumTva { get; set; }

        public ICollection<Implantation> Implanter { get; set; }
        public ICollection<Representant> Representant { get; set; }
        public ICollection<Provient> Provients { get; set; }
    }
}

