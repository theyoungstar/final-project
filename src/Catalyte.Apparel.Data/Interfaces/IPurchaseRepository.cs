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
        Task<List<Purchase>> GetAllPurchasesAsync();

        Task<Purchase> CreatePurchaseAsync(Purchase purchase);
    }
}
