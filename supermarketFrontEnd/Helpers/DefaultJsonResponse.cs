using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd.Helpers
{
    public class DefaultJsonResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}