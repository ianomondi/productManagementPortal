using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd
{
    public class Configs
    {
        public static string CompanyName = "";
        public static string ToastSeparator = ";";
        public static string DefaultErrorMessage = "";
        public static string BASE_API_URL = "https://localhost:44341/";

        public struct Endpoints
        {
            public static string categories_list = "productCategories";
            public static string categories_save = "productCategory";
            public static string categories_update = $"productCategoy/";
            public static string categories_delete = $"productCategoy/";

            public static string variants_list = "variants";
            public static string variants_save = "variant/";
            public static string variants_update = $"variant/";
            public static string variants_delete = $"variant/";


            public static string variantoptions_list = "variantoptions";
            public static string variantoptions_save = "variantoption";
            public static string variantoptions_update = $"variantoption/";
            public static string variantoptions_delete = $"variantoption/";


            public static string skus_list = "sku/";
            public static string skus_save = "sku/";
            public static string skus_update = $"sku/";
            public static string skus_delete = $"sku/";
            
            
            public static string productVariants_list = "productVariants";
            public static string productVariants_save = "productVariant";
            public static string productVariants_update = $"productVariant/";
            public static string productVariants_delete = $"productVariant/";
            
            
            public static string compositeProducts_list = "compositeProducts/";
            public static string compositeProducts_save = "compositeProduct";
            public static string compositeProducts_update = $"compositeProduct/";
            public static string compositeProducts_delete = $"compositeProduct/";
            
            
            public static string products_list = "products/";
            public static string products_save = "product";
            public static string products_update = $"product/";
            public static string products_delete = $"product/";


        }
    }
}