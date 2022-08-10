using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Projekt.Models
{
    public class UserViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Repeated password")]
        public string RepeatedPassword { get; set; }
        public IFormFile Avatar { get; set; }

    }
}
