using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace supermarketFrontEnd.Models.ViewModels
{
    public class CreateProductViewModel
    {
        public int id { get; set; }

        [DisplayName("Product Name")]
        [Required]
        public string name { get; set; }

        [DisplayName("Category")]
        [Required]
        public Category category { get; set; }
        public List<ProductVariant> productVariants { get; set; }
        
        public SKU sku { get; set; }

        public List<SelectListItem> relatedProducts { get; set; }
        public List<int> compositeProductIds { get; set; }
        public List<Variant> variants { get; set; }
        public List<VariantOption> variantOptions { get; set; }
    }
}