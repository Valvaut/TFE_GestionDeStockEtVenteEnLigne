using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TFE_GestionDeStockEtVenteEnLigne.Models;
using TFE_GestionDeStockEtVenteEnLigne.Models.Metier;

namespace TFE_GestionDeStockEtVenteEnLigne.Data
{
    public class TFEContext : DbContext
    {
        public TFEContext(DbContextOptions<TFEContext> options) : base(options)
        {

        }
        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<RepHabite> RepHabites { get; set; }
        public DbSet<Representant> Repesentants { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<Fournisseur> Fournisseurs { get; set; }
        public DbSet<Attribut> Attributs { get; set; }
        public DbSet<Domicile> Domiciles { get; set; }
        public DbSet<Implantation> Implantations { get; set; }
        public DbSet<MotClef> MotClefs { get; set; }
        public DbSet<Possede> Possedes { get; set; }
        public DbSet<ProduitMotClef> ProduitsMotClefs { get; set; }
        public DbSet<Provient> Provients { get; set; }
        public DbSet<Travail> Travails { get; set; }
        public DbSet<Horraire> Horraire { get; set; }
        public DbSet<Evenement> Evenement { get; set; }
        public DbSet<Panier> Panier { get; set; }
        public DbSet<Historique> Historique { get; set; }
        public DbSet<TauxTVA> TVA { get; set; }
        public object HttpContext { get; internal set; }
    }
}
