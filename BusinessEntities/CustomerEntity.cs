using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class CustomerEntity
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string ContacntNo { get; set; }
        public string EmailId { get; set; }
        public string GSTIN { get; set; }
        public string PAN { get; set; }
        public string TIN { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
    }
}
