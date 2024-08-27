using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindBasedWebAPI.Models
{
    public class EmployeeTerritory
    {
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }



        [ForeignKey("Territory")]
        public int TerritoryId { get; set; }
        public Territory Territory { get; set; }
    }
}
