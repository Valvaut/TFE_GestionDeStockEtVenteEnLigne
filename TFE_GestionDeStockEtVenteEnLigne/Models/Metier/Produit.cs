using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TFE_GestionDeStockEtVenteEnLigne.Models.Metier;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    [Bind("ID,CategorieID,MotClef,Ref,Denomination,Prix,QuantiteEmballage,NBPieceEmballage,TVA,CompteCompta,Description,Marque,QuantiteStock,Image")]
    public class Produit
    {
        public int ID { get; set; }

        public int CategorieID { get; set; }
        [Display(Name = "Référence")]
        public String Ref { get; set; }
        [Display(Name = "Dénomination")]
        public String Denomination { get; set; }

        [Range(0, double.MaxValue)]
        public double Prix { get; set; }

        [Range(0, int.MaxValue)]
        [Display(Name = "Quantité  en stock")]
        public int QuantiteEmballage { get; set; }

        [Range(0, int.MaxValue)]
        [Display(Name = "Quantité par emballage")]
        public int NBPieceEmballage { get; set; }

        [Range(0, int.MaxValue)]
        public int TVA { get; set; }
        [Display(Name = "Compte comptable")]
        public String CompteCompta{ get; set; }

        public String Description { get; set; }

        public String Marque { get; set; }

        [Range(0, int.MaxValue)]
        [Display(Name = "Quantité en stock")]
        public int QuantiteStock { get; set; }

        public Byte[] Image { get; set; }

        public bool Visible { get; set; }
        [Display(Name = "Date de création")]
        public DateTime Date { get; set; }

        public ICollection<Possede> Possede { get; set; }
        public ICollection <ProduitMotClef>MotClef { get; set; }
        public ICollection<Panier> Panier { get; set; }
        public Categorie Categorie { get; set; }
        public ICollection<Valeur> Valeur { get; set; }
        public ICollection<Provient> Provients { get; set; }
    }
}
