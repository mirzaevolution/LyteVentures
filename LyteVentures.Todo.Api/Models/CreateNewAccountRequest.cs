using System.ComponentModel.DataAnnotations;

namespace LyteVentures.Todo.Api.Models
{
    public class CreateNewAccountRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(5)]
        public string Password { get; set; }
        [Required]
        [MinLength(2)]
        public string FullName { get; set; }
    }
}
