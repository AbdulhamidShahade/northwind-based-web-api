using System.ComponentModel.DataAnnotations;

namespace NorthwindBasedWebAPI.Models.Dtos.RegionDtos
{
    public class CreateRegionDto
    {

        [Required(ErrorMessage = "Region description is required!")]
        [Display(Name = "Region Description")]
        public string RegionDescription { get; set; }


        [Display(Name = "Picture")]
        public string? PictureUrl { get; set; }

    }
}
