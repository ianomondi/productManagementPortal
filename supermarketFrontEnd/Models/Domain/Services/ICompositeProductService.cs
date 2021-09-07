using supermarketFrontEnd.Communication.CompositeProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supermarketFrontEnd.Models.Domain.Services
{
    public interface ICompositeProductService
    {
        Task<CompositeProductListResponse> GetAsync(int productId);
        Task<List<CompositeProductResponse>> SaveRangeAsync(List<CompositeProduct> compositeProducts);
        Task<CompositeProductResponse> SaveAsync(CompositeProduct compositeProduct);
        Task<CompositeProductResponse> UpdateAsync(int id, CompositeProduct compositeProduct);
        Task<CompositeProductResponse> DeleteAsync(int id);
    }
}
