using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EshoppingV2._0.Models
{
    public class SalesTransaction
    {
        public int MyProperty { get; set; }
        public string payType { get; set; }
        public DateTime transcationDate { get; set; }
        public Guid orderId { get; set; }
        public long paymentId { get; set; }
    }
}