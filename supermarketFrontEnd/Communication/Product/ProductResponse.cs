using supermarketFrontEnd.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd.Communication.Product
{
    public class ProductResponse : APIError
    {
        public supermarketFrontEnd.Models.Product Product { get; set; }
    }
}