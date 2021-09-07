using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd.Resources
{
    public class SaveProductVariantResource
    {
        public int productId { get; set; }
        public int variantOptionId { get; set; }
        public int variantId { get; set; }
    }
}