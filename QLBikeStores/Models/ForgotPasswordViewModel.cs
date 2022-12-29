using System.ComponentModel.DataAnnotations;

namespace QLBikeStores.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
