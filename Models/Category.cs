﻿using NorthwindBasedWebAPI.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace NorthwindBasedWebAPI.Models
{
    public class Category : BaseEntity
    {
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category name is required field!")]
        public string CategoryName { get; set; }



        [Display(Name = "Description")]
        public string? Description { get; set; }



        [Display(Name = "Picture")]
        public string? PictureUrl { get; set; }


        public ICollection<Product>? Products { get; set; }
    }
}
