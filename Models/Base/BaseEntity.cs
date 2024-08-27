
using System.ComponentModel.DataAnnotations;

namespace NorthwindBasedWebAPI.Models.Base
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
