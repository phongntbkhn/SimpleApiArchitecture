using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Simple.API.Infrastructure.Entities;

namespace Simple.API.Infrastructure
{
    public class SimpleApiContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public SimpleApiContext(DbContextOptions<SimpleApiContext> options) : base(options) { }

        public DbSet<Production> Productions { get; set; }
    }
}
