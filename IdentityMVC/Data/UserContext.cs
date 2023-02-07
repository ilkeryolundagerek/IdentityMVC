using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityMVC.Models;

namespace IdentityMVC.Data
{
    public class UserContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public UserContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<IdentityMVC.Models.Employee> Employee { get; set; }
    }
}
