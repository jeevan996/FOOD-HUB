using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ROS.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public Guid userId { get; set; }

        public virtual List<CartItem> cartItems { get; set; }
    }
}