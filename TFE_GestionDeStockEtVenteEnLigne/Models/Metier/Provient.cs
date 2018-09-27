using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TFE_GestionDeStockEtVenteEnLigne.Models.Metier;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class Provient
    {
        public int ID { get; set; }
        public int ProduitID { get; set; }
        public int FournisseurID { get; set; }
        public int TauxTVAID { get; set; }

        [Range(0, double.MaxValue)]
        [Display(Name = "Prix achat")]
        public Double Prix { get; set; }

        [Range(0, int.MaxValue)]
        [Display(Name = "Taux de TVA")]
        public int TauxTVA { get; set; }
        [Display(Name = "Taux TVA ?")]
        public TauxTVA TauxTVAObjet {get;set;}

        [Range(0, int.MaxValue)]
        [Display(Name = "Quantité à commander au mminimum")]
        public int QuantiteMinCommande { get; set; }

        public Produit Produit { get; set; }
        public Fournisseur Fournisseur{ get; set; }
    }
}
