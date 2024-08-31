
using System.ComponentModel.DataAnnotations;

namespace NorthwindBasedWebAPI.Models.Base
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt
        {
            set
            {
                value = DateTime.UtcNow;
            }
            get
            {
                return DateTime.UtcNow;
            }
        }
        public virtual DateTime? UpdatedAt
        {
            set
            {
                value = DateTime.UtcNow;
            }
            get
            {
                return DateTime.UtcNow;
            }
        }
    }
}
