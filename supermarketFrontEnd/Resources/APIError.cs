using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd.Resources
{
    public class APIError
    {
        public bool success { get; set; }
        public List<string> messages { get; set; }
    }
}