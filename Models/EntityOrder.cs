using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PaymentGateway.Models
{
    public class EntityOrder
    {
        public string ID { get; set; }
        public string Name { get; set; }    

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string Amount { get; set; }
        public string TransactionID { get; set; }

        public string OrderID { get; set; }
        
    }
}
