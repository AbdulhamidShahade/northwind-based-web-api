
using System.ComponentModel.DataAnnotations;

namespace NorthwindBasedWebAPI.Models.Base
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt
        {
            get
            {
                return DateTime.UtcNow;
            }
        }
        public DateTime? UpdatedAt
        {
            get
            {
                return DateTime.UtcNow;
            }
        }
    }
}
