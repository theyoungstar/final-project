using Catalyte.Apparel.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Data.Interfaces
{
    /// <summary>
    /// This interface provides an abstraction layer for shipping rate repository methods.
    /// </summary>
    public interface IShippingRateRepository
    {
        Task<IEnumerable<ShippingRate>> GetShippingRatesAsync();

        Task<ShippingRate> CreateShippingRateAsync(ShippingRate shippingRate);

        Task<ShippingRate> GetShippingRateByStateAsync(string state);
    }
}
