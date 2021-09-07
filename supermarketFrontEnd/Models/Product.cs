using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd.Models
{
    public class Product
    {
        public int id { get; set; }

        [DisplayName("Product Name")]
        public string name { get; set; }

        [DisplayName("Category")]
        public Category category { get; set; }
        public List<Variant> variants { get; set; }
        public SKU sku { get; set; }
    }
}