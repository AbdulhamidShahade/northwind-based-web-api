﻿using NorthwindBasedWebAPI.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindBasedWebAPI.Models
{
    public class CustomerCustomerDemographic : BaseEntity
    {
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }



        [ForeignKey("CustomerType")]
        public int CustomerTypeId { get; set; }
        public CustomerDemographic CustomerType { get; set; }
    }
}
