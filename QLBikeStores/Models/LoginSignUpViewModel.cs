using System.ComponentModel.DataAnnotations;

namespace QLBikeStores.Models
{
    public class LoginSignUpViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool isRemember { get; set; }
    }
}
