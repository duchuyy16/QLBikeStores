using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLBikeStores.Models
{
    public class User :IdentityUser
    {
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [StringLength(500)]
        public string NickName { get; set; }

        [NotMapped]
        public string UserPassword { get; set; }

        [StringLength(450)]
        public string Teaser { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public DateTime Birthday { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public DateTime? LatestLogin { get; set; }
    }
}
