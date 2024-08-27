using NorthwindBasedWebAPI.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace NorthwindBasedWebAPI.Models
{
    public class Shipper : BaseEntity
    {
        [Required(ErrorMessage = "Company name is required field!")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }



        [Required(ErrorMessage = "Shipper phone is required field!")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }



        [Display(Name = "Picture")]
        public string? PictureUrl { get; set; }


        public ICollection<Order> Orders { get; set; }
    }
}
