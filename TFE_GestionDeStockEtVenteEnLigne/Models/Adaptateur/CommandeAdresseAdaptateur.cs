﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models.Adaptateur
{
    public class CommandeAdresseAdaptateur
    {
        public Commande Commande { get; set; }
        public Adresse Adresse { get; set; }

        public List<Adresse> ListeAdresse { get; set; }

        //public CommandeAdresseAdaptateur()
        //{
        //    Commande = new Commande();
        //    ListeAdresse = new List<Adresse>();
        //    Adresse = new Adresse();
        //}
    }
}
