using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd.Models
{
    public class Category
    {
        [Display(Name = "Category Id")]
        public int id { get; set; }
        [Display(Name = "Category Name")]
        public string name { get; set; }
    }

    
}