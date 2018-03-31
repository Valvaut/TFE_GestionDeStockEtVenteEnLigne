using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models.Adaptateur
{
    public class ClientAdaptateur
    {
        public Client Client { get; set; }
        public List<Adresse> ListeAdresse;
        

        public ClientAdaptateur()
        {
            Client = new Client();
            ListeAdresse = new List<Adresse>();
        }
    }
}
