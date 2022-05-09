using Catalyte.Apparel.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Providers.Interfaces
{
    /// <summary>
    /// This interface provides an abstraction layer for promo code related service methods.
    /// </summary>
    public interface IShippingRateProvider
    {
        Task<ShippingRate> GetShippingRateByStateAsync(string shippingRateState);

        Task<IEnumerable<ShippingRate>> GetShippingRatesAsync();

        Task<ShippingRate> CreateShippingRateAsync(ShippingRate shippingRate);
    }
}
