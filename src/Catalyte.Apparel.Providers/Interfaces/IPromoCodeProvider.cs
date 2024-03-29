﻿using Catalyte.Apparel.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Providers.Interfaces
{
    /// <summary>
    /// This interface provides an abstraction layer for promo code related service methods.
    /// </summary>
    public interface IPromoCodeProvider
    {
        Task<PromoCode> GetPromoCodeByIdAsync(int promoCodeId);

        Task<IEnumerable<PromoCode>> GetPromoCodesAsync();

        Task<PromoCode> CreatePromoCodeAsync(PromoCode promoCode);
    }
}
