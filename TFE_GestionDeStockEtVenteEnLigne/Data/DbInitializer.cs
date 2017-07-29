using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
           
        }
    }
}
