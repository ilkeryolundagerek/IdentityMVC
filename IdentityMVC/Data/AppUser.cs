using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IdentityMVC.Data
{
    public class AppUser : IdentityUser
    {
        [Required,MaxLength(25)]
        public string Firstname { get; set; }

        [Required, MaxLength(25)]
        public string Lastname { get; set; }
    }


}
