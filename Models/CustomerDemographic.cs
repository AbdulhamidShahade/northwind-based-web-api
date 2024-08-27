using System.ComponentModel.DataAnnotations;

namespace NorthwindBasedWebAPI.Models
{
    public class CustomerDemographic
    {
        [Display(Name = "Customer Description")]
        public string? CustomerDescription { get; set; }



        [Display(Name = "Picture")]
        public string? PictureUrl { get; set; }
    }
}
