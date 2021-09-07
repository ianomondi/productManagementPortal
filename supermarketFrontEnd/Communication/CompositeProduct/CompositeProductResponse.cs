using supermarketFrontEnd.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd.Communication.CompositeProduct
{
    public class CompositeProductResponse : APIError
    {
        public supermarketFrontEnd.Models.CompositeProduct CompositeProduct { get; set; }
    }
}