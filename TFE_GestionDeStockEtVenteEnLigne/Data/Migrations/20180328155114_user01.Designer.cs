using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TFE_GestionDeStockEtVenteEnLigne.Data;

namespace TFE_GestionDeStockEtVenteEnLigne.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180328155114_user01")]
    partial class user01
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

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

                    b.ToTable("Adresse");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<bool>("Newsletter");

                    b.Property<string>("Nom");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("NumTva");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Prenom");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Tel");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Attribut", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Mesure");

                    b.Property<string>("Nom");

                    b.HasKey("ID");

                    b.ToTable("Attribut");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Categorie", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategorieParentID");

                    b.Property<string>("Nom");

                    b.HasKey("ID");

                    b.HasIndex("CategorieParentID");

                    b.ToTable("Categorie");
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

                    b.ToTable("Client");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Commande", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AdresseID");

                    b.Property<string>("ApplicationUserId");

                    b.Property<int>("ClientID");

                    b.Property<DateTime>("DateCommade");

                    b.Property<bool>("EnCours");

                    b.Property<DateTime>("Envoie");

                    b.HasKey("ID");

                    b.HasIndex("AdresseID");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("ClientID");

                    b.ToTable("Commande");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Domicile", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdresseID");

                    b.Property<string>("ApplicationUserId");

                    b.Property<int>("ClientID");

                    b.HasKey("ID");

                    b.HasIndex("AdresseID");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("ClientID");

                    b.ToTable("Domicile");
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

                    b.ToTable("Facture");
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

                    b.ToTable("Fournisseur");
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

                    b.ToTable("Implantation");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Metier.Panier", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<int?>("ClientID");

                    b.Property<int>("ProduitID");

                    b.Property<int>("Quantite");

                    b.Property<string>("RegisterViewModelID");

                    b.HasKey("ID");

                    b.HasIndex("ApplicationUserId");

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

                    b.ToTable("MotClef");
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

                    b.ToTable("Possede");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Produit", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategorieID");

                    b.Property<string>("CompteCompta");

                    b.Property<string>("Denomination");

                    b.Property<string>("Description");

                    b.Property<byte[]>("Image");

                    b.Property<string>("Marque");

                    b.Property<int>("NBPieceEmballage");

                    b.Property<float>("Prix");

                    b.Property<int>("QuantiteEmballage");

                    b.Property<int>("QuantiteStock");

                    b.Property<string>("Ref");

                    b.Property<int>("TVA");

                    b.HasKey("ID");

                    b.HasIndex("CategorieID");

                    b.ToTable("Produit");
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

                    b.ToTable("ProduitMotClef");
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Provient", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FournisseurID");

                    b.Property<int>("Prix");

                    b.Property<int>("ProduitID");

                    b.Property<int>("QuantiteMinCommande");

                    b.Property<int>("TauxTVA");

                    b.HasKey("ID");

                    b.HasIndex("FournisseurID");

                    b.HasIndex("ProduitID");

                    b.ToTable("Provient");
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

                    b.ToTable("RepHabite");
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

                    b.ToTable("Representant");
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
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

                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.ApplicationUser")
                        .WithMany("Commande")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Client", "Client")
                        .WithMany("Commande")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Domicile", b =>
                {
                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Adresse", "Adresse")
                        .WithMany("DomicileClient")
                        .HasForeignKey("AdresseID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.ApplicationUser")
                        .WithMany("Domicile")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.Client", "Client")
                        .WithMany("Domicile")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade);
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

            modelBuilder.Entity("TFE_GestionDeStockEtVenteEnLigne.Models.Metier.Panier", b =>
                {
                    b.HasOne("TFE_GestionDeStockEtVenteEnLigne.Models.ApplicationUser")
                        .WithMany("Panier")
                        .HasForeignKey("ApplicationUserId");

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
