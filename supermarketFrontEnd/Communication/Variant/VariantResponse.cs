using supermarketFrontEnd.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd.Communication.Variant
{
    public class VariantResponse : APIError
    {
        public supermarketFrontEnd.Models.Variant Variant { get; set; }
    }
}