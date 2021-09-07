using supermarketFrontEnd.Communication.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supermarketFrontEnd.Models.Domain.Services
{
    public interface IProductService
    {
        Task<Product> GetByIdAsync(int productId);
        Task<ProductListResponse> ListAsync(int categoryId = 0);
        Task<ProductResponse> SaveAsync(Product product);
        Task<ProductResponse> UpdateAsync(int id, Product product);
        Task<ProductResponse> DeleteAsync(int id);
    }
}
