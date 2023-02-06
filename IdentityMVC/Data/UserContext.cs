using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityMVC.Data
{
    public class UserContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public UserContext(DbContextOptions options) : base(options)
        {
        }
    }
}
