using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TFE_GestionDeStockEtVenteEnLigne.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public bool Newsletter { get; set; }
        [Required]
        public String Prenom { get; set; }
        [Required]
        public String NumTva { get; set; }
        [Required]
        public String Tel { get; set; }

    }
}
