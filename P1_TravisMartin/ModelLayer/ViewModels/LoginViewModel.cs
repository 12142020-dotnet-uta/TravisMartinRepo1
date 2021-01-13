using System;
using System.ComponentModel.DataAnnotations;

namespace ModelLayer.ViewModels
{
    public class LoginViewModel
    {
        public Guid CustomerId { get; set; } = Guid.NewGuid();

        [StringLength(20, MinimumLength = 3, ErrorMessage = "The username must be from 3-20 characters in length.")]
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; } // customer user name used for login

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", ErrorMessage = "Must use a lower case letter, an upper case letter, a number, and a special character.")]
        [DataType(DataType.Password)]
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; } // customer password used login

    }
}
