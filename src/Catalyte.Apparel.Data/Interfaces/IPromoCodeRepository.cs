using Catalyte.Apparel.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Data.Interfaces
{
    /// <summary>
    /// This interface provides an abstraction layer for promo code repository methods.
    /// </summary>
    public interface IPromoCodeRepository
    {
        Task<PromoCode> GetPromoCodeByIdAsync(int promoCodeId);

        Task<IEnumerable<PromoCode>> GetPromoCodesAsync();
    }
}
