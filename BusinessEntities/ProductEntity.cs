using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class ProductEntity
    {
        public int ProductId { get; set; }
        public string ProductTaxable { get; set; }
        public string ProductType { get; set; }
        public string ProductName { get; set; }
        public string UnitoMesure { get; set; }
        public string AlternameUom { get; set; }
        public string TaxType { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Discount { get; set; }
        public Nullable<decimal> PurchasePrice { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
        public string Description { get; set; }
        public string Hscno { get; set; }
    }
}
