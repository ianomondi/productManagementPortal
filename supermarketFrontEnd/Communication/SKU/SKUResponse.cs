using supermarketFrontEnd.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd.Communication.SKU
{
    public class SKUResponse : APIError
    {
        public  supermarketFrontEnd.Models.SKU SKU { get; set; }
    }
}