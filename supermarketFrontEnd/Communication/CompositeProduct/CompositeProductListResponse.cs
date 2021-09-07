using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd.Communication.CompositeProduct
{
    public class CompositeProductListResponse
    {
        public int totalItems { get; set; }
        public List<supermarketFrontEnd.Models.CompositeProduct> items { get; set; }
    }
}