using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models.Adaptateur
{
    public class RepresentantAdaptateur
    {
        public Representant Representant { get; set; }
        public List<Adresse> ListAdresse { get; set; }
        public List<Fournisseur> ListFournisseur { get; set; }
        public RepresentantAdaptateur()
        {
            Representant = new Representant();
            ListAdresse = new List<Adresse>();
        }
    }
}
