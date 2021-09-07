using supermarketFrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd.Resources
{
    public class SaveProductResource
    {
        public string name { get; set; }

        public int categoryId { get; set; }
        public SKU saveProductSKUResource { get; set; }
    }
}