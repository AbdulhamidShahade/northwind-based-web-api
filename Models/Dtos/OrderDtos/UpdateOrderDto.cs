﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NorthwindBasedWebAPI.Models.Dtos.OrderDtos
{
    public class UpdateOrderDto
    {

        [Display(Name = "Id")]
        [Required(ErrorMessage = "Id is required field")]
        public int Id { get; set; }



        [Required(ErrorMessage = "Order date is required field!")]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }




        [Required(ErrorMessage = "Required date is required field!")]
        [Display(Name = "Required Date")]
        public DateTime RequiredDate { get; set; }




        [Display(Name = "Shipped Date")]
        public DateTime? ShippedDate { get; set; }




        [Required(ErrorMessage = "Freight is required field!")]
        [Display(Name = "Freight")]
        public decimal Freight { get; set; }




        [Required(ErrorMessage = "Ship name is required field!")]
        [Display(Name = "Ship Name")]
        public string ShipName { get; set; }




        [Required(ErrorMessage = "Shipped address is required field!")]
        [Display(Name = "Shipped Address")]
        public string ShipAddress { get; set; }




        [Required(ErrorMessage = "Ship city is required field!")]
        [Display(Name = "Ship City")]
        public string ShipCity { get; set; }





        [Display(Name = "Ship Region")]
        public string? ShipRegion { get; set; }




        [Display(Name = "Ship Postal Code")]
        public string? ShipPostalCode { get; set; }




        [Required(ErrorMessage = "Ship country is required field!")]
        [Display(Name = "Ship Country")]
        public string ShipCountry { get; set; }




        [Display(Name = "Picture")]
        public string? PictureUrl { get; set; }




    }
}
