﻿using System.ComponentModel.DataAnnotations;

namespace NorthwindBasedWebAPI.Models.Dtos.CategoryDtos
{
    public class ReadCategoryDto
    {

        [Display(Name = "Category Id")]
        public int Id { get; set; }



        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category name is required field!")]

        public string CategoryName { get; set; }



        [Display(Name = "Description")]

        public string? Description { get; set; }




        [Display(Name = "Picture")]

        public string? PictureUrl { get; set; }
    }
}
