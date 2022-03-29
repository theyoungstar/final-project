using Catalyte.Apparel.Data.Model;
using System.Collections.Generic;
using System.Linq;

namespace Catalyte.Apparel.Data.Filters
{
    /// <summary>
    /// Filter collection for promo code context queries.
    /// </summary>
    public static class PromoCodeFilters
    {
        public static IQueryable<PromoCode> WherePromoCodeIdEquals(this IQueryable<PromoCode> promoCodes, int promoCodeId)
        {
            return promoCodes.Where(p => p.Id == promoCodeId).AsQueryable();
        }
    }
}
