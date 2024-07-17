using Microsoft.AspNetCore.Identity;

namespace Simple.API.Infrastructure.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        public AppRole() { }

        public AppRole(string roleName) : base(roleName) { }
    }
}
