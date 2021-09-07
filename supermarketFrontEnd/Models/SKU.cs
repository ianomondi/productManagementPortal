using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd.Models
{
    public class SKU
    {
        public int id { get; set; }
        [DisplayName("Unit of Measure")]
        public string unitOfMeasure { get; set; }
        [DisplayName("Unit Price")]
        public string unitPrice { get; set; }

        [DisplayName("Product")]
        public int productId { get; set; }

        [DisplayName("Quantity")]
        public string quantity { get; set; }
    }
}