using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd.Models
{
    public class Variant
    {
        public int id { get; set; }
        [DisplayName("Variant Name")]
        public string name { get; set; }
        [DisplayName("Variant Display Name")]
        public string displayName { get; set; }
        [DisplayName("Variant Frontend Display Name")]
        public string frontEndName { get; set; }
    }
}