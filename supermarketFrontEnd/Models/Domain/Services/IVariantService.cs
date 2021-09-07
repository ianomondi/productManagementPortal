using supermarketFrontEnd.Communication.Variant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supermarketFrontEnd.Models.Domain.Services
{
    public interface IVariantService
    {
        Task<IEnumerable<Variant>> ListAsync();
        Task<VariantResponse> SaveAsync(Variant category);
        Task<VariantResponse> UpdateAsync(int id, Variant category);
        Task<VariantResponse> DeleteAsync(int id);
    }
}
