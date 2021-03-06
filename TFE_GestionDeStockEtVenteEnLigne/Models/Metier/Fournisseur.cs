﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class Fournisseur
    {
        public int ID { get; set; }
        public String Nom { get; set; }
        [Display(Name = "Référence")]
        public String Reference { get; set; }
        public String Mail { get; set; }
        [Display(Name = "Téléphone")]
        public String Telephone { get; set; }
        public String Fax { get; set; }
        [Display(Name = "Numéro de compte")]
        public String NumCompte { get; set; }
        [Display(Name = "Numéro de TVA")]
        public String NumTva { get; set; }
        [Display(Name = "Site internet")]
        public String SiteNet { get; set; }

        public ICollection<Implantation> Implanter { get; set; }
        public ICollection<Representant> Representant { get; set; }
        public ICollection<Provient> Provients { get; set; }
    }
}

