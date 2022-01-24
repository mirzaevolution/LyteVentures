using System.ComponentModel.DataAnnotations;

namespace IdentityServerHost.Quickstart.UI
{
    public class CreateNewAccountRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(5)]
        public string Password { get; set; }
        [Required]
        [MinLength(2)]
        public string FullName { get; set; }
    }
}
