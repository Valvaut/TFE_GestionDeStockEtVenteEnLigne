using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TFE_GestionDeStockEtVenteEnLigne.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name="Mot de passe")]
        public string Password { get; set; }

        [Display(Name = "souvenir des informations")]
        public bool RememberMe { get; set; }
    }
}
