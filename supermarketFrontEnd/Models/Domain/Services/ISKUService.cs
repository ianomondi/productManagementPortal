using supermarketFrontEnd.Communication.SKU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supermarketFrontEnd.Models.Domain.Services
{
    public interface ISKUService
    {
        Task<SKU> GetAsync(int productId);
        Task<SKUResponse> SaveAsync(SKU sku);
        Task<SKUResponse> UpdateAsync(int id, SKU sku);
        Task<SKUResponse> DeleteAsync(int id);
    }
}
