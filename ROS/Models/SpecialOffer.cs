using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ROS.Models
{
    public class SpecialOffer
    {
        public int Id { get; set; }
        public string offerName { get; set; }
        public float discount { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int MinQ { get; set; }
        public int MaxQ { get; set; }
        public string description { get; set; }
    }
}