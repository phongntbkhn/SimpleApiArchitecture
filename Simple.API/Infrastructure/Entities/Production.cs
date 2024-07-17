using System.ComponentModel.DataAnnotations;

namespace Simple.API.Infrastructure.Entities
{
    public class Production : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
