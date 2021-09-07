using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd.Models
{
    public class VariantOption
    {
        public int id { get; set; }
        [DisplayName("Variant Id")]
        public int variantId { get; set; }
        [DisplayName("Variant Option")]
        public string value { get; set; }
    }
}