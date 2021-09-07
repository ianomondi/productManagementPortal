using supermarketFrontEnd.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd.Communication.VariantOption
{
    public class VariantOptionResponse : APIError
    {
        public supermarketFrontEnd.Models.VariantOption VariantOption { get; set; }
    }
}