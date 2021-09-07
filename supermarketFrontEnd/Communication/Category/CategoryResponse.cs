using supermarketFrontEnd.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd.Communication.Category
{
    public class CategoryResponse : APIError
    {
        public supermarketFrontEnd.Models.Category Category { get; set; }
    }
}