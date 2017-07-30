using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ROS.Models
{
    public class SalesOrder
    {
        public SalesOrder() {
            shippingDate = DateTime.Now;
            orderDate = DateTime.Now;
            dueDate = DateTime.Now;
            deliveryDate = DateTime.Now;
        }
        [Key]
        public Guid Id { get; set; }
        public Guid customerId { get; set; }
        [ForeignKey("shippingAddress")]
        public int shippingId { get; set; }
        public float totalCost { get; set; }
        public float orderCost { get; set; }
        public float shippingCost { get; set; }
        public string status { get; set; }
        public virtual DateTime? orderDate { get; set; }
        public virtual DateTime? dueDate { get; set; }
        public virtual DateTime? shippingDate { get; set; }
        public virtual DateTime? deliveryDate { get; set; }

        public ShippingAddress shippingAddress { get; set; }

        public List<OrderDetails> orderDetails { get; set; }
    }
}