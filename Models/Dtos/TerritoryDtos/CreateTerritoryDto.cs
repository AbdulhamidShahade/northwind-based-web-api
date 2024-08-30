using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NorthwindBasedWebAPI.Models.Dtos.TerritoryDtos
{
    public class CreateTerritoryDto
    {


        [Required(ErrorMessage = "Territory Description is required!")]
        [Display(Name = "Territory Description")]
        public string TerritoryDescription { get; set; }


        [Display(Name = "PictureUrl")]
        public string? PictureUrl { get; set; }



    

    }
}
