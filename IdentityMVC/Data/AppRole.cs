using Microsoft.AspNetCore.Identity;

namespace IdentityMVC.Data
{
    public class AppRole : IdentityRole
    {
        public string? Detail { get; set; }
    }


}
