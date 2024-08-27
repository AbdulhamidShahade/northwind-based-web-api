using System.ComponentModel.DataAnnotations;

namespace NorthwindBasedWebAPI.Models
{
    public class Category
    {
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category name is required field!")]
        public string CategoryName { get; set; }



        [Display(Name = "Description")]
        public string? Description { get; set; }



        [Display(Name = "Picture")]
        public string? PictureUrl { get; set; }
    }
}
