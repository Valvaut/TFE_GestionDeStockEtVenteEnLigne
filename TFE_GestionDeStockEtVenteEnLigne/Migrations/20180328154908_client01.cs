using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TFE_GestionDeStockEtVenteEnLigne.Migrations
{
    public partial class client01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CodePostal = table.Column<int>(nullable: false),
                    Comune = table.Column<string>(nullable: true),
                    Localite = table.Column<string>(nullable: true),
                    Numero = table.Column<int>(nullable: false),
                    NumeroBoite = table.Column<string>(nullable: true),
                    Pays = table.Column<string>(nullable: true),
                    Rue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Attributs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Mesure = table.Column<string>(nullable: true),
                    Nom = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategorieParentID = table.Column<int>(nullable: true),
                    Nom = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_CategorieParentID",
                        column: x => x.CategorieParentID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Newsletter = table.Column<bool>(nullable: false),
                    Nom = table.Column<string>(nullable: true),
                    NumTva = table.Column<string>(nullable: true),
                    Prenom = table.Column<string>(nullable: true),
                    RegisterViewModelID = table.Column<string>(nullable: true),
                    Tel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Evenement",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evenement", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Fournisseurs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fax = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    Nom = table.Column<string>(nullable: true),
                    NumCompte = table.Column<string>(nullable: true),
                    NumTva = table.Column<string>(nullable: true),
                    Reference = table.Column<string>(nullable: true),
                    SiteNet = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fournisseurs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Horraire",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Dimanche = table.Column<string>(nullable: true),
                    Jeudi = table.Column<string>(nullable: true),
                    Lundi = table.Column<string>(nullable: true),
                    Mardi = table.Column<string>(nullable: true),
                    Mercredi = table.Column<string>(nullable: true),
                    Samedi = table.Column<string>(nullable: true),
                    Vendredi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horraire", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MotClefs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Valeur = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotClefs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Produits",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategorieID = table.Column<int>(nullable: false),
                    CompteCompta = table.Column<string>(nullable: true),
                    Denomination = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    Marque = table.Column<string>(nullable: true),
                    NBPieceEmballage = table.Column<int>(nullable: false),
                    Prix = table.Column<float>(nullable: false),
                    QuantiteEmballage = table.Column<int>(nullable: false),
                    QuantiteStock = table.Column<int>(nullable: false),
                    Ref = table.Column<string>(nullable: true),
                    TVA = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produits", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Produits_Categories_CategorieID",
                        column: x => x.CategorieID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commandes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdresseID = table.Column<int>(nullable: true),
                    ClientID = table.Column<int>(nullable: false),
                    DateCommade = table.Column<DateTime>(nullable: false),
                    EnCours = table.Column<bool>(nullable: false),
                    Envoie = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commandes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Commandes_Adresses_AdresseID",
                        column: x => x.AdresseID,
                        principalTable: "Adresses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Commandes_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Domiciles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdresseID = table.Column<int>(nullable: false),
                    ClientID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domiciles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Domiciles_Adresses_AdresseID",
                        column: x => x.AdresseID,
                        principalTable: "Adresses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Domiciles_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Implantations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdresseID = table.Column<int>(nullable: false),
                    FournisseurID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Implantations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Implantations_Adresses_AdresseID",
                        column: x => x.AdresseID,
                        principalTable: "Adresses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Implantations_Fournisseurs_FournisseurID",
                        column: x => x.FournisseurID,
                        principalTable: "Fournisseurs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Repesentants",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FournisseurID = table.Column<int>(nullable: true),
                    GSM = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    Nom = table.Column<string>(nullable: true),
                    Prenom = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repesentants", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Repesentants_Fournisseurs_FournisseurID",
                        column: x => x.FournisseurID,
                        principalTable: "Fournisseurs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Panier",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientID = table.Column<int>(nullable: true),
                    ProduitID = table.Column<int>(nullable: false),
                    Quantite = table.Column<int>(nullable: false),
                    RegisterViewModelID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Panier", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Panier_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Panier_Produits_ProduitID",
                        column: x => x.ProduitID,
                        principalTable: "Produits",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProduitsMotClefs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MotClefId = table.Column<int>(nullable: false),
                    ProduitID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduitsMotClefs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProduitsMotClefs_MotClefs_MotClefId",
                        column: x => x.MotClefId,
                        principalTable: "MotClefs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProduitsMotClefs_Produits_ProduitID",
                        column: x => x.ProduitID,
                        principalTable: "Produits",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Provients",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FournisseurID = table.Column<int>(nullable: false),
                    Prix = table.Column<int>(nullable: false),
                    ProduitID = table.Column<int>(nullable: false),
                    QuantiteMinCommande = table.Column<int>(nullable: false),
                    TauxTVA = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provients", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Provients_Fournisseurs_FournisseurID",
                        column: x => x.FournisseurID,
                        principalTable: "Fournisseurs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Provients_Produits_ProduitID",
                        column: x => x.ProduitID,
                        principalTable: "Produits",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Valeur",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttributID = table.Column<int>(nullable: false),
                    ProduitID = table.Column<int>(nullable: false),
                    Valeurs = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Valeur", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Valeur_Attributs_AttributID",
                        column: x => x.AttributID,
                        principalTable: "Attributs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Valeur_Produits_ProduitID",
                        column: x => x.ProduitID,
                        principalTable: "Produits",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Factures",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommandeID = table.Column<int>(nullable: false),
                    DatePaiement = table.Column<DateTime>(nullable: false),
                    Numero = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factures", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Factures_Commandes_CommandeID",
                        column: x => x.CommandeID,
                        principalTable: "Commandes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Possedes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommandeID = table.Column<int>(nullable: false),
                    ProduitID = table.Column<int>(nullable: false),
                    Quantite = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Possedes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Possedes_Commandes_CommandeID",
                        column: x => x.CommandeID,
                        principalTable: "Commandes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Possedes_Produits_ProduitID",
                        column: x => x.ProduitID,
                        principalTable: "Produits",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FournisseurID = table.Column<int>(nullable: false),
                    RepresentantID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Contacts_Fournisseurs_FournisseurID",
                        column: x => x.FournisseurID,
                        principalTable: "Fournisseurs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contacts_Repesentants_RepresentantID",
                        column: x => x.RepresentantID,
                        principalTable: "Repesentants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepHabites",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdresseID = table.Column<int>(nullable: false),
                    RepresentantID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepHabites", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RepHabites_Adresses_AdresseID",
                        column: x => x.AdresseID,
                        principalTable: "Adresses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepHabites_Repesentants_RepresentantID",
                        column: x => x.RepresentantID,
                        principalTable: "Repesentants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Travails",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FournisseurID = table.Column<int>(nullable: false),
                    RepresentantID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Travails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Travails_Fournisseurs_FournisseurID",
                        column: x => x.FournisseurID,
                        principalTable: "Fournisseurs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Travails_Repesentants_RepresentantID",
                        column: x => x.RepresentantID,
                        principalTable: "Repesentants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategorieParentID",
                table: "Categories",
                column: "CategorieParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Commandes_AdresseID",
                table: "Commandes",
                column: "AdresseID");

            migrationBuilder.CreateIndex(
                name: "IX_Commandes_ClientID",
                table: "Commandes",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Domiciles_AdresseID",
                table: "Domiciles",
                column: "AdresseID");

            migrationBuilder.CreateIndex(
                name: "IX_Domiciles_ClientID",
                table: "Domiciles",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_CommandeID",
                table: "Factures",
                column: "CommandeID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Implantations_AdresseID",
                table: "Implantations",
                column: "AdresseID");

            migrationBuilder.CreateIndex(
                name: "IX_Implantations_FournisseurID",
                table: "Implantations",
                column: "FournisseurID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_FournisseurID",
                table: "Contacts",
                column: "FournisseurID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_RepresentantID",
                table: "Contacts",
                column: "RepresentantID");

            migrationBuilder.CreateIndex(
                name: "IX_Panier_ClientID",
                table: "Panier",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Panier_ProduitID",
                table: "Panier",
                column: "ProduitID");

            migrationBuilder.CreateIndex(
                name: "IX_Possedes_CommandeID",
                table: "Possedes",
                column: "CommandeID");

            migrationBuilder.CreateIndex(
                name: "IX_Possedes_ProduitID",
                table: "Possedes",
                column: "ProduitID");

            migrationBuilder.CreateIndex(
                name: "IX_Produits_CategorieID",
                table: "Produits",
                column: "CategorieID");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitsMotClefs_MotClefId",
                table: "ProduitsMotClefs",
                column: "MotClefId");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitsMotClefs_ProduitID",
                table: "ProduitsMotClefs",
                column: "ProduitID");

            migrationBuilder.CreateIndex(
                name: "IX_Provients_FournisseurID",
                table: "Provients",
                column: "FournisseurID");

            migrationBuilder.CreateIndex(
                name: "IX_Provients_ProduitID",
                table: "Provients",
                column: "ProduitID");

            migrationBuilder.CreateIndex(
                name: "IX_RepHabites_AdresseID",
                table: "RepHabites",
                column: "AdresseID");

            migrationBuilder.CreateIndex(
                name: "IX_RepHabites_RepresentantID",
                table: "RepHabites",
                column: "RepresentantID");

            migrationBuilder.CreateIndex(
                name: "IX_Repesentants_FournisseurID",
                table: "Repesentants",
                column: "FournisseurID");

            migrationBuilder.CreateIndex(
                name: "IX_Travails_FournisseurID",
                table: "Travails",
                column: "FournisseurID");

            migrationBuilder.CreateIndex(
                name: "IX_Travails_RepresentantID",
                table: "Travails",
                column: "RepresentantID");

            migrationBuilder.CreateIndex(
                name: "IX_Valeur_AttributID",
                table: "Valeur",
                column: "AttributID");

            migrationBuilder.CreateIndex(
                name: "IX_Valeur_ProduitID",
                table: "Valeur",
                column: "ProduitID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Domiciles");

            migrationBuilder.DropTable(
                name: "Evenement");

            migrationBuilder.DropTable(
                name: "Factures");

            migrationBuilder.DropTable(
                name: "Horraire");

            migrationBuilder.DropTable(
                name: "Implantations");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Panier");

            migrationBuilder.DropTable(
                name: "Possedes");

            migrationBuilder.DropTable(
                name: "ProduitsMotClefs");

            migrationBuilder.DropTable(
                name: "Provients");

            migrationBuilder.DropTable(
                name: "RepHabites");

            migrationBuilder.DropTable(
                name: "Travails");

            migrationBuilder.DropTable(
                name: "Valeur");

            migrationBuilder.DropTable(
                name: "Commandes");

            migrationBuilder.DropTable(
                name: "MotClefs");

            migrationBuilder.DropTable(
                name: "Repesentants");

            migrationBuilder.DropTable(
                name: "Attributs");

            migrationBuilder.DropTable(
                name: "Produits");

            migrationBuilder.DropTable(
                name: "Adresses");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Fournisseurs");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
