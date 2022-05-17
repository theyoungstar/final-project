using Catalyte.Apparel.Data.Context;
using Catalyte.Apparel.Data.Filters;
using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Data.Repositories
{
    /// <summary>
    /// This class handles methods for making requests to the shipping rate repository.
    /// </summary>
    public class ShippingRateRepository : IShippingRateRepository
    {
        private readonly IApparelCtx _ctx;

        public ShippingRateRepository(IApparelCtx ctx)
        {
            _ctx = ctx;
        }

        //Retrieves shipping rate by state
        public async Task<ShippingRate> GetShippingRateByStateAsync(string state)
        {
            return await _ctx.ShippingRates
                .AsNoTracking()
                .WhereShippingRateStateEquals(state)
                .SingleOrDefaultAsync();
        }

        //Retrieves all shipping rates
        public async Task<IEnumerable<ShippingRate>> GetShippingRatesAsync()
        {
            return await _ctx.ShippingRates
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        //Deletes Shipping Rate
        public async void DeleteShippingRatesAsync(ShippingRate shippingRate)
        {
            _ctx.ShippingRates.Remove(shippingRate);
            await _ctx.SaveChangesAsync();

        }

        //Creates Shipping Rates
        public async Task<ShippingRate> CreateShippingRateAsync(ShippingRate shippingRate)
        {
            _ctx.ShippingRates.Add(shippingRate);
            await _ctx.SaveChangesAsync();

            return shippingRate;
        }
    }

}
