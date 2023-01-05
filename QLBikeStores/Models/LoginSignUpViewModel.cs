using System.ComponentModel.DataAnnotations;

namespace QLBikeStores.Models
{
    public class LoginSignUpViewModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool isRemember { get; set; }
    }
}
