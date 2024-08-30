using System.ComponentModel.DataAnnotations;

namespace NorthwindBasedWebAPI.Models.Dtos.CustomerDemographicDtos
{
    public class UpdateCustomerDemographicDto
    {

        [Display(Name = "Id")]
        public int Id { get; set; }



        [Display(Name = "Customer Description")]
        public string? CustomerDescription { get; set; }


        [Display(Name = "Picture")]
        public string? PictureUrl { get; set; }
    }
}
