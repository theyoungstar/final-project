﻿using Catalyte.Apparel.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Providers.Interfaces
{
    /// <summary>
    /// This interface provides an abstraction layer for shipping rate related service methods.
    /// </summary>
    public interface IShippingRateProvider
    {
        Task<ShippingRate> GetShippingRateByStateAsync(string state);

        Task<IEnumerable<ShippingRate>> GetShippingRatesAsync();

        Task<ShippingRate> CreateShippingRateAsync(ShippingRate shippingRate);
    }
}
