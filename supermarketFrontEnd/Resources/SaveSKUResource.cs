using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd.Resources
{
    public class SaveSKUResource
    {
        public string unitOfMeasure { get; set; }
        public int productId { get; set; }
        public string quantity { get; set; }
        public string unitPrice { get; set; }
    }
}