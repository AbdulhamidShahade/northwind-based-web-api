using System.ComponentModel.DataAnnotations;

namespace NorthwindBasedWebAPI.Models.Dtos.RegionDtos
{ 
    public class UpdateRegionDto
    {

        [Display(Name = "Id")]
        public int Id { get; set; }



        [Required(ErrorMessage = "Region description is required!")]
        [Display(Name = "Region Description")]
        public string RegionDescription { get; set; }


        [Display(Name = "Picture")]
        public string? PictureUrl { get; set; }

    }
}
