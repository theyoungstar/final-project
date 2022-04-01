using Catalyte.Apparel.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Data.Interfaces
{
    /// <summary>
    /// This interface provides an abstraction layer for purchase repository methods.
    /// </summary>
    public interface IPurchaseRepository
    {
        Task<IEnumerable<Purchase>> GetAllPurchasesByEmailAsync(string billingEmail);

        Task<Purchase> CreatePurchaseAsync(Purchase purchase);
        void GetAllPurchasesByEmailAsync(int v);
    }
}
