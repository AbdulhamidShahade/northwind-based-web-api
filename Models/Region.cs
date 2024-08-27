using System.ComponentModel.DataAnnotations;

namespace NorthwindBasedWebAPI.Models
{
    public class Region
    {
        [Required(ErrorMessage = "Region description is required!")]
        [Display(Name = "Region Description")]
        public string RegionDescription { get; set; }



        [Display(Name = "Picture")]
        public string? PictureUrl { get; set; }
    }
}
