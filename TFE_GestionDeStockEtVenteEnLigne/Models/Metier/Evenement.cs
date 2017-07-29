using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    public class Evenement
    {
        public int ID  { get; set; }
        public DateTime Date { get; set; }
        public String Description { get; set; }
    }
}
