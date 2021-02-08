using Adelanka.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Adelanka.DAL
{
    public class AdelankaDBContext : IdentityDbContext
    {
        public AdelankaDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SystemUser> SystemUser { get; set; }
    }
}