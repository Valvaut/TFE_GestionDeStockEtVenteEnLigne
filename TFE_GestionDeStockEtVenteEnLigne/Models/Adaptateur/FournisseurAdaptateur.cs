using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models.Adaptateur
{
   
    public class FournisseurAdaptateur
    {
        public Fournisseur Fournisseur { get; set; }
        public List<Adresse> ListAdresse { get; set; }
        public List<Representant> ListeRepresentant { get; set; }

        public FournisseurAdaptateur()
        {
            Fournisseur = new Fournisseur();
            ListAdresse = new List<Adresse>();
        }
    }
}
