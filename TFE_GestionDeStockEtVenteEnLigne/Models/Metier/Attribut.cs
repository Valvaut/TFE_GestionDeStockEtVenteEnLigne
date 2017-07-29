using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class Attribut
    {
        public int ID { get; set; }
        public String Nom { get; set; } 

        public ICollection<Valeur> Valeur { get; set; }
    }
}
