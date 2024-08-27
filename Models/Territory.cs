using System.ComponentModel.DataAnnotations;

namespace NorthwindBasedWebAPI.Models
{
    public class Territory
    {
        [Required]
        [Display(Name = "Territory Description")]
        public string TerritoryDescription { get; set; }



        [Display(Name = "PictureUrl")]
        public string? PictureUrl { get; set; }
    }
}
