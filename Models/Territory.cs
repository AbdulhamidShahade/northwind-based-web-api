using NorthwindBasedWebAPI.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindBasedWebAPI.Models
{
    public class Territory : BaseEntity
    {
        [Required]
        [Display(Name = "Territory Description")]
        public string TerritoryDescription { get; set; }



        [Display(Name = "PictureUrl")]
        public string? PictureUrl { get; set; }


        public ICollection<EmployeeTerritory> EmployeeTerritory { get; set; }



        [Required]
        [Display(Name = "Region")]
        [ForeignKey("Region")]
        public int RegionId { get; set; }
        public Region Region { get; set; }
    }
}
