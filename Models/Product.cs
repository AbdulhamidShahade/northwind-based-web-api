using System.ComponentModel.DataAnnotations;

namespace NorthwindBasedWebAPI.Models
{
    public class Product
    {
        [Required(ErrorMessage = "Product name is required field!")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }



        [Required(ErrorMessage = "Quantity per unit is required field!")]
        [Display(Name = "Quantity Per Unit")]
        public string QuantityPerUnit { get; set; }



        [Required(ErrorMessage = "Unit Price is required field!")]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }



        [Required(ErrorMessage = "Units in stock is required field!")]
        [Display(Name = "Units In Stock")]
        public short UnitsInStock { get; set; }



        [Required(ErrorMessage = "Units on order is required field!")]
        [Display(Name = "Units On Order")]
        public short UnitsOnOrder { get; set; }



        [Required(ErrorMessage = "Reorder level is required field!")]
        [Display(Name = "Reorder Level")]
        public short ReorderLevel { get; set; }



        [Display(Name = "Discontinued")]
        [Required(ErrorMessage = "Discontinued is required field!")]
        public bool Discontinued { get; set; }



        [Display(Name = "Picture")]
        public string? PictureUrl { get; set; }
    }
}
