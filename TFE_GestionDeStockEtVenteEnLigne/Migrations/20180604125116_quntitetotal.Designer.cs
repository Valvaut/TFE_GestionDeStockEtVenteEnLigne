using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TFE_GestionDeStockEtVenteEnLigne.Data;

namespace TFE_GestionDeStockEtVenteEnLigne.Migrations
{
    [DbContext(typeof(TFEContext))]
    [Migration("20180604125116_quntitetotal")]
    partial class quntitetotal
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Adresse", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CodePostal");

                    b.Property<string>("Comune");

                    b.Property<string>("Localite");

                    b.Property<int>("Numero");

                    b.Property<string>("NumeroBoite");

                    b.Property<string>("Pays");

                    b.Property<string>("Rue");

                    b.HasKey("ID");

                    b.ToTable("Adresses");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Attribut", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Mesure");

                    b.Property<string>("Nom");

                    b.HasKey("ID");

                    b.ToTable("Attributs");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Categorie", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategorieParentID");

                    b.Property<string>("Nom");

                    b.HasKey("ID");

                    b.HasIndex("CategorieParentID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Client", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Newsletter");

                    b.Property<string>("Nom");

                    b.Property<string>("NumTva");

                    b.Property<string>("Prenom");

                    b.Property<string>("RegisterViewModelID");

                    b.Property<string>("Tel");

                    b.HasKey("ID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Commande", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AdresseID");

                    b.Property<int?>("ClientID");

                    b.Property<DateTime>("DateCommade");

                    b.Property<bool>("EnCours");

                    b.Property<DateTime>("Envoie");

                    b.Property<string>("RegisterViewModelID");

                    b.HasKey("ID");

                    b.HasIndex("AdresseID");

                    b.HasIndex("ClientID");

                    b.ToTable("Commandes");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Domicile", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdresseID");

                    b.Property<int?>("ClientID");

                    b.Property<string>("RegisterViewModelID");

                    b.HasKey("ID");

                    b.HasIndex("AdresseID");

                    b.HasIndex("ClientID");

                    b.ToTable("Domiciles");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Evenement", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.HasKey("ID");

                    b.ToTable("Evenement");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Facture", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CommandeID");

                    b.Property<DateTime>("DatePaiement");

                    b.Property<int>("Numero");

                    b.HasKey("ID");

                    b.HasIndex("CommandeID")
                        .IsUnique();

                    b.ToTable("Factures");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Fournisseur", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Fax");

                    b.Property<string>("Mail");

                    b.Property<string>("Nom");

                    b.Property<string>("NumCompte");

                    b.Property<string>("NumTva");

                    b.Property<string>("Reference");

                    b.Property<string>("SiteNet");

                    b.Property<string>("Telephone");

                    b.HasKey("ID");

                    b.ToTable("Fournisseurs");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Horraire", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Dimanche");

                    b.Property<string>("Jeudi");

                    b.Property<string>("Lundi");

                    b.Property<string>("Mardi");

                    b.Property<string>("Mercredi");

                    b.Property<string>("Samedi");

                    b.Property<string>("Vendredi");

                    b.HasKey("ID");

                    b.ToTable("Horraire");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Implantation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdresseID");

                    b.Property<int>("FournisseurID");

                    b.HasKey("ID");

                    b.HasIndex("AdresseID");

                    b.HasIndex("FournisseurID");

                    b.ToTable("Implantations");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Metier.Contact", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FournisseurID");

                    b.Property<int>("RepresentantID");

                    b.HasKey("ID");

                    b.HasIndex("FournisseurID");

                    b.HasIndex("RepresentantID");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Metier.Panier", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClientID");

                    b.Property<int>("ProduitID");

                    b.Property<int>("Quantite");

                    b.Property<string>("RegisterViewModelID");

                    b.HasKey("ID");

                    b.HasIndex("ClientID");

                    b.HasIndex("ProduitID");

                    b.ToTable("Panier");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.MotClef", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Valeur");

                    b.HasKey("ID");

                    b.ToTable("MotClefs");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Possede", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CommandeID");

                    b.Property<int>("ProduitID");

                    b.Property<int>("Quantite");

                    b.HasKey("ID");

                    b.HasIndex("CommandeID");

                    b.HasIndex("ProduitID");

                    b.ToTable("Possedes");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Produit", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategorieID");

                    b.Property<string>("CompteCompta");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Denomination");

                    b.Property<string>("Description");

                    b.Property<byte[]>("Image");

                    b.Property<string>("Marque");

                    b.Property<int>("NBPieceEmballage");

                    b.Property<double>("Prix");

                    b.Property<int>("QuantiteEmballage");

                    b.Property<int>("QuantiteStock");

                    b.Property<int>("QuantiteStockTotal");

                    b.Property<string>("Ref");

                    b.Property<int>("TVA");

                    b.Property<bool>("Visible");

                    b.HasKey("ID");

                    b.HasIndex("CategorieID");

                    b.ToTable("Produits");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.ProduitMotClef", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MotClefId");

                    b.Property<int>("ProduitID");

                    b.HasKey("ID");

                    b.HasIndex("MotClefId");

                    b.HasIndex("ProduitID");

                    b.ToTable("ProduitsMotClefs");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Provient", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FournisseurID");

                    b.Property<double>("Prix");

                    b.Property<int>("ProduitID");

                    b.Property<int>("QuantiteMinCommande");

                    b.Property<int>("TauxTVA");

                    b.HasKey("ID");

                    b.HasIndex("FournisseurID");

                    b.HasIndex("ProduitID");

                    b.ToTable("Provients");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.RepHabite", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdresseID");

                    b.Property<int>("RepresentantID");

                    b.HasKey("ID");

                    b.HasIndex("AdresseID");

                    b.HasIndex("RepresentantID");

                    b.ToTable("RepHabites");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Representant", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("FournisseurID");

                    b.Property<string>("GSM");

                    b.Property<string>("Mail");

                    b.Property<string>("Nom");

                    b.Property<string>("Prenom");

                    b.HasKey("ID");

                    b.HasIndex("FournisseurID");

                    b.ToTable("Repesentants");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Travail", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FournisseurID");

                    b.Property<int>("RepresentantID");

                    b.HasKey("ID");

                    b.HasIndex("FournisseurID");

                    b.HasIndex("RepresentantID");

                    b.ToTable("Travails");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Valeur", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AttributID");

                    b.Property<int>("ProduitID");

                    b.Property<string>("Valeurs");

                    b.HasKey("ID");

                    b.HasIndex("AttributID");

                    b.HasIndex("ProduitID");

                    b.ToTable("Valeur");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Categorie", b =>
                {
                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Categorie", "CategorieParent")
                        .WithMany("CategorieEnfant")
                        .HasForeignKey("CategorieParentID");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Commande", b =>
                {
                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Adresse", "AdresseFacturation")
                        .WithMany("ListeCommande")
                        .HasForeignKey("AdresseID");

                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Client", "Client")
                        .WithMany("Commande")
                        .HasForeignKey("ClientID");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Domicile", b =>
                {
                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Adresse", "Adresse")
                        .WithMany("DomicileClient")
                        .HasForeignKey("AdresseID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Client", "Client")
                        .WithMany("Domicile")
                        .HasForeignKey("ClientID");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Facture", b =>
                {
                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Commande", "Commande")
                        .WithOne("Facture")
                        .HasForeignKey("TFE_GestionDeStockEtVenteEnLigne.Models.Facture", "CommandeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Implantation", b =>
                {
                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Adresse", "Adresse")
                        .WithMany("Implanter")
                        .HasForeignKey("AdresseID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Fournisseur", "Fournisseur")
                        .WithMany("Implanter")
                        .HasForeignKey("FournisseurID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Metier.Contact", b =>
                {
                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Fournisseur", "Fournisseur")
                        .WithMany()
                        .HasForeignKey("FournisseurID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Representant", "Representant")
                        .WithMany()
                        .HasForeignKey("RepresentantID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Metier.Panier", b =>
                {
                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Client", "Client")
                        .WithMany("Panier")
                        .HasForeignKey("ClientID");

                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Produit", "Produit")
                        .WithMany("Panier")
                        .HasForeignKey("ProduitID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Possede", b =>
                {
                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Commande", "Commande")
                        .WithMany("Possede")
                        .HasForeignKey("CommandeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Produit", "Produit")
                        .WithMany("Possede")
                        .HasForeignKey("ProduitID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Produit", b =>
                {
                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Categorie", "Categorie")
                        .WithMany("Produits")
                        .HasForeignKey("CategorieID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.ProduitMotClef", b =>
                {
                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.MotClef", "MotClef")
                        .WithMany("Produit")
                        .HasForeignKey("MotClefId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Produit", "Produit")
                        .WithMany("MotClef")
                        .HasForeignKey("ProduitID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Provient", b =>
                {
                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Fournisseur", "Fournisseur")
                        .WithMany("Provients")
                        .HasForeignKey("FournisseurID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Produit", "Produit")
                        .WithMany("Provients")
                        .HasForeignKey("ProduitID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.RepHabite", b =>
                {
                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Adresse", "Adresse")
                        .WithMany("RepHabite")
                        .HasForeignKey("AdresseID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Representant", "Representant")
                        .WithMany("RepHabite")
                        .HasForeignKey("RepresentantID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Representant", b =>
                {
                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Fournisseur")
                        .WithMany("Representant")
                        .HasForeignKey("FournisseurID");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Travail", b =>
                {
                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Fournisseur", "Fournisseur")
                        .WithMany()
                        .HasForeignKey("FournisseurID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Representant", "Representant")
                        .WithMany()
                        .HasForeignKey("RepresentantID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Valeur", b =>
                {
                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Attribut", "Attribut")
                        .WithMany("Valeur")
                        .HasForeignKey("AttributID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Produit", "Produit")
                        .WithMany("Valeur")
                        .HasForeignKey("ProduitID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
