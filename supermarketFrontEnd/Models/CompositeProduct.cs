using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd.Models
{
    public class CompositeProduct
    {
        public int id { get; set; }
        [DisplayName("Product")]
        public int productId { get; set; }
        [DisplayName("Related Product")]
        public int relatedId { get; set; }
    }
}