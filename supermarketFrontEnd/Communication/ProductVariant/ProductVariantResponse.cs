using supermarketFrontEnd.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd.Communication.ProductVariant
{
    public class ProductVariantResponse : APIError
    {
        public supermarketFrontEnd.Models.ProductVariant ProductVariant { get; set; }
    }
}