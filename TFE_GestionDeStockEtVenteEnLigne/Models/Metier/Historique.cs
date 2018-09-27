using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models.Metier
{
    public class Historique
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Numéro de produit")]
        public int ProduitID { get; set; }
        [Display(Name = "Quantité en stock")]
        public int QteStock  { get; set; }
        [Display(Name = "Mouvement")]
        public string Action { get; set; }
        [Display(Name = "Quantité déplacée")]
        public int QteMouv { get; set; }

        public Produit Produit { get; set; }

    }
}
