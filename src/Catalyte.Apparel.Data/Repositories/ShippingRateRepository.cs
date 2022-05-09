using Catalyte.Apparel.Data.Context;
using Catalyte.Apparel.Data.Filters;
using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Data.Repositories
{
    /// <summary>
    /// This class handles methods for making requests to the promo code repository.
    /// </summary>
    public class ShippingRateRepository : IShippingRateRepository
    {
        private readonly IApparelCtx _ctx;

        public ShippingRateRepository(IApparelCtx ctx)
        {
            _ctx = ctx;
        }

        public async Task<ShippingRate> GetShippingRateByStateAsync(string shippingRateState)
        {
            return await _ctx.ShippingRates
                .AsNoTracking()
                .WhereShippingRateStateEquals(shippingRateState)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ShippingRate>> GetShippingRatesAsync()
        {
            return await _ctx.ShippingRates
                .AsNoTracking()
                .ToListAsync();
        }

        //Delete PromoCode Repo Function
        public async void DeleteShippingRatesAsync(ShippingRate shippingRate)
        {
            _ctx.ShippingRates.Remove(shippingRate);
            await _ctx.SaveChangesAsync();

        }
        public async Task<ShippingRate> GetShippingRateByStaetAsync(string state)
        {
            return await _ctx.ShippingRates.AsNoTracking().WhereShippingRateStateEquals(state).SingleOrDefaultAsync();
        }

        public async Task<ShippingRate> CreateShippingRateAsync(ShippingRate shippingRate)
        {
            _ctx.ShippingRates.Add(shippingRate);
            await _ctx.SaveChangesAsync();

            return shippingRate;
        }
    }

}
