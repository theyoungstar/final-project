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
    }

}
