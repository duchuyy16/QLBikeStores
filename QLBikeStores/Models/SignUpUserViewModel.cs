using System.ComponentModel.DataAnnotations;

using System;
using Microsoft.AspNetCore.Mvc;

namespace QLBikeStores.Models
{
    public class SignUpUserViewModel
    {
        public int Id { get; set; }    

        [Required(ErrorMessage = "Please enter first name")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Please enter last name")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Please enter store")]
        public int StoreId { get; set; } // <=3

        [Required (ErrorMessage ="Please enter username")]
        [Remote(action:"UsernameIsExist",controller:"Account")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}",ErrorMessage ="Please enter Valid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter phone number")]
        [Display(Name ="Phone number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Phone number is not valid.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password",ErrorMessage =("Confirm password can't matched"))]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        //[Display(Name = "Active")]
        //public bool IsActive { get; set; }
    }
}
