using NorthwindBasedWebAPI.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindBasedWebAPI.Models
{
    public class SystemLog : BaseEntity
    {
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Level { get; set; }
        public string Message { get; set; }
        public string MethodType { get; set; }
        public string User { get; set; }
        public string Role { get; set; }
        public string Details { get; set; }
        public string StatusCode { get; set; }
        public bool? Success { get; set; }
        public bool? Failed { get; set; }
        public string? ErrorMessage { get; set; }

        [NotMapped]
        public override DateTime? UpdatedAt { get; set; }
    }
}
