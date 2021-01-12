using System;
using System.ComponentModel.DataAnnotations;

namespace ModelLayer.ViewModels
{
    public class RegistrationViewModel
    {
        public Guid CustomerId { get; set; } = Guid.NewGuid();

        [StringLength(20, MinimumLength = 3, ErrorMessage = "The first name must be from 3-20 characters in length.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "No numbers allowed in the first name.")]
        [Required]
        [Display(Name = "First Name")]
        public string FName { get; set; } // customer first name used for registration

        [StringLength(20, MinimumLength = 3, ErrorMessage = "The last name must be from 3-20 characters in length.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "No numbers allowed in the last name.")]
        [Required]
        [Display(Name = "Last Name")]
        public string LName { get; set; } // customer last name used for registration

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Required]
        [Display(Name = "Email Address")]
        public string Email { get; set; } // customer email address used for registration

        [StringLength(20, MinimumLength = 3, ErrorMessage = "The username must be from 3-20 characters in length.")]
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; } // customer user name used for login

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", ErrorMessage = "Must use a lower case letter, an upper case letter, a number, and a special character.")]
        [DataType(DataType.Password)]
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; } // customer password used login

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", ErrorMessage = "Must use a lower case letter, an upper case letter, a number, and a special character.")]
        [DataType(DataType.Password)]
        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } // confirms that password was entered in correctly
    }
}
