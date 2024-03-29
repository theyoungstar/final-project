﻿using Catalyte.Apparel.Data.Model;
using System.Linq;

namespace Catalyte.Apparel.Data.Filters
{
    /// <summary>
    /// Filter collection for shipping rate context queries.
    /// </summary>
    public static class ShippingRateFilters
    {
      
        public static IQueryable<ShippingRate> WhereShippingRateStateEquals(this IQueryable<ShippingRate> shippingRates, string state)
        {
            return shippingRates.Where(r => r.State == state).AsQueryable();
        }
    }
}
