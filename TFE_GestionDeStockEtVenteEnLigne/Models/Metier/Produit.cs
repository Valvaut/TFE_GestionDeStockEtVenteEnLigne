using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    [Bind("ID,CategorieID,MotClef,Ref,Denomination,Prix,QuantiteEmballage,NBPieceEmballage,TVA,CompteCompta,Description,Marque,QuantiteStock,Image")]
    public class Produit
    {
        public int ID { get; set; }
        public int CategorieID { get; set; }
        public String Ref { get; set; }
        public String Denomination { get; set; }
        public int Prix { get; set; }
        public int QuantiteEmballage { get; set; }
        public int NBPieceEmballage { get; set; }
        public int TVA { get; set; }
        public String CompteCompta{ get; set; }
        public String Description { get; set; }
        public String Marque { get; set; }
        public int QuantiteStock { get; set; }
        public Byte[] Image { get; set; }

        public ICollection<Possede> Possede { get; set; }
        public ICollection <ProduitMotClef>MotClef { get; set; }
        public Categorie Categorie { get; set; }
        public ICollection<Valeur> Valeur { get; set; }
        public ICollection<Provient> Provients { get; set; }
    }
}
