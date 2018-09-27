using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class Representant
    {
        public int ID { get; set; }
        [Display(Name = "Prénom")]
        public String Prenom { get; set; }
        public String Nom { get; set; }
        public String Mail { get; set; }
        public String GSM { get; set; }

        [Display(Name = "Domicile")]
        public ICollection<RepHabite> RepHabite { get; set; }
    }
}
