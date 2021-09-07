using supermarketFrontEnd.Communication.ProductVariant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supermarketFrontEnd.Models.Domain.Services
{
    public interface IProductVariantService
    {
        Task<IEnumerable<ProductVariant>> GetAsync(int productId);
        Task<List<ProductVariantResponse>> SaveRangeAsync(List<ProductVariant> productVariants);
        Task<ProductVariantResponse> SaveAsync(ProductVariant productVariant);
        Task<ProductVariantResponse> UpdateAsync(int id, ProductVariant productVariant);
        Task<ProductVariantResponse> DeleteAsync(int id);
    }
}
