using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TFE_GestionDeStockEtVenteEnLigne.Models;

namespace TFE_GestionDeStockEtVenteEnLigne.Data
{
    public class DbInitializer 
    {


        public static void Initialize(TFEContext context)
        {
            context.Database.EnsureCreated();

            if (context.Horraire.Any())
            {
                return;
            }

            var Horraire = new Horraire[]
            {
                new Horraire{Lundi="10-19",Mardi="10-19",Mercredi="10-19",Jeudi="10-19",Vendredi="10-19",Samedi="Sur Rendez-vous",Dimanche="Fermer" }
            };
            foreach (Horraire h in Horraire)
            {
                context.Horraire.Add(h);
            }
            var Address = new Adresse[]
            {
                new Adresse{Localite = "Toernich",Rue="A-kreides",Numero=43,NumeroBoite="",Pays="Belgique",CodePostal=6700,Comune="Arlon" }
            };
            foreach (Adresse a in Address)
            {
                context.Adresses.Add(a);
            }
            context.SaveChanges();
            var Attribut = new Attribut[]
            {
                new Attribut{ Nom= "a",Mesure = "a"}
            };
            foreach (Attribut a in Attribut)
            {
                context.Attributs.Add(a);
            }
            context.SaveChanges();

            var MotClef = new MotClef[]
            {
                new MotClef{ Valeur= "a"}
            };
            foreach (MotClef a in MotClef)
            {
                context.MotClefs.Add(a);
            }
            context.SaveChanges();

            var Fournisseur = new Fournisseur[]
            {
                new Fournisseur{ Nom= "a",Reference="a",Mail="a",Telephone="a",Fax="a",NumCompte="a",SiteNet="a",NumTva="a" }
            };
            foreach (Fournisseur a in Fournisseur)
            {
                context.Fournisseurs.Add(a);
            }
            context.SaveChanges();
            var cat = new Categorie[]
            {
                new Categorie{ Nom="a" }
            };
            foreach (Categorie a in cat)
            {
                context.Categories.Add(a);
            }
            context.SaveChanges();
         
        }
        public static async void Initialize2(ApplicationDbContext contextIdentity,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (contextIdentity.Users.Any())
            {
                return;   // exit method, Database has been    
            }

            // crée le compte gérant par défault
            var user = new ApplicationUser { UserName = "michele.tinant@skynet.be", Email = "michele.tinant@skynet.be", Prenom ="Michele", NumTva ="", Tel ="063218952"};
            await userManager.CreateAsync(user, "Rofl_1");
            IdentityRole role = new IdentityRole { Name = "gestionnaire", NormalizedName = "GESTIONNAIRE" };
            IdentityResult RoleResult = await roleManager.CreateAsync(role);
            if (RoleResult.Succeeded)//puis l'ajoute a l'utilisateur
                 await userManager.AddToRoleAsync(user, role.Name);
        }
    }
}
