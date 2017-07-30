using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ROS.Models
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("order")]
        public Guid orderId { get; set; }
        [ForeignKey("product")]
        public int productId { get; set; }
        public int quantity { get; set; }
        public Guid trackingNumber { get; set; }
        [ForeignKey("specialOffer")]
        public int specialOfferId { get; set; }
        public SalesOrder order { get; set; }
        public Recipe product { get; set; }
        public SpecialOffer specialOffer { get; set; }

    }
}