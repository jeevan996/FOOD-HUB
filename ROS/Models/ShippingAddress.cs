using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ROS.Models
{
    public class ShippingAddress
    {
        public int Id { get; set; }
        public string location { get; set; }
        public string state { get; set; }
        public int pincode { get; set; }
        public Guid userId { get; set; }
        public string phoneNumber { get; set; }
    }
}