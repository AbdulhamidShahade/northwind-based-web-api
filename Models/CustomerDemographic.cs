using NorthwindBasedWebAPI.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace NorthwindBasedWebAPI.Models
{
    public class CustomerDemographic : BaseEntity
    {
        [Display(Name = "Customer Description")]
        public string? CustomerDescription { get; set; }



        [Display(Name = "Picture")]
        public string? PictureUrl { get; set; }


        public ICollection<CustomerCustomerDemographic> CustomerCustomerDemographic { get; set; }
    }
}
