using System.ComponentModel.DataAnnotations;

namespace IdentityServerHost.Quickstart.UI
{
    public class RegisterNewAccount
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(5)]
        public string Password { get; set; }
        [Required]
        [MinLength(2)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public string ReturnUrl { get; set; }
    }
}
