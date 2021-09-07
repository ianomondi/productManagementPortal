using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd.Models
{
    public class ProductVariant
    {
        public int id { get; set; }
        [DisplayName("Product")]
        public int productId { get; set; }
        [DisplayName("Value")]
        public int variantOptionId { get; set; }
        [DisplayName("Variant")]
        public int variantId { get; set; }
    }
}