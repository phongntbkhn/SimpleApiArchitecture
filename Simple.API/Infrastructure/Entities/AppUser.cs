using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Simple.API.Infrastructure.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public DateTime? LastLogin { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public bool IsDelete { get; set; } = false;
    }
}
