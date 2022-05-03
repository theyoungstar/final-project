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
    public class PromoCodeRepository : IPromoCodeRepository
    {
        private readonly IApparelCtx _ctx;

        public PromoCodeRepository(IApparelCtx ctx)
        {
            _ctx = ctx;
        }

        public async Task<PromoCode> GetPromoCodeByIdAsync(int promoCodeId)
        {
            return await _ctx.PromoCodes
                .AsNoTracking()
                .WherePromoCodeIdEquals(promoCodeId)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<PromoCode>> GetPromoCodesAsync()
        {
            return await _ctx.PromoCodes
                .AsNoTracking()
                .ToListAsync();
        }

        //Delete PromoCode Repo Function
        public async void DeletePromoCodesAsync(PromoCode promoCode)
        {
            _ctx.PromoCodes.Remove(promoCode);
            await _ctx.SaveChangesAsync();

        }
        public async Task<PromoCode> GetPromoCodeByTitleAsync(string title)
        {
            return await _ctx.PromoCodes.AsNoTracking().WherePromoCodeTitleEquals(title).SingleOrDefaultAsync();
        }

        public async Task<PromoCode> CreatePromoCodeAsync(PromoCode promoCode)
        {
            _ctx.PromoCodes.Add(promoCode);
            await _ctx.SaveChangesAsync();

            return promoCode;
        }
    }

}
