using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd.Communication.Product
{
    public class ProductListResponse
    {
        public int totalItems { get; set; }
        public List<supermarketFrontEnd.Models.Product> items { get; set; }
    }
}