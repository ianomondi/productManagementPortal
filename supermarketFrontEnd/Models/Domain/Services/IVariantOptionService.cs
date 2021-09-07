using supermarketFrontEnd.Communication.VariantOption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supermarketFrontEnd.Models.Domain.Services
{
    public interface IVariantOptionService
    {
        Task<IEnumerable<VariantOption>> ListAsync();
        Task<VariantOptionResponse> SaveAsync(VariantOption category);
        Task<VariantOptionResponse> UpdateAsync(int id, VariantOption category);
        Task<VariantOptionResponse> DeleteAsync(int id);
    }
}
