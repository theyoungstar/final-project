using Catalyte.Apparel.Data.Model;
using System.Linq;

namespace Catalyte.Apparel.Data.Filters
{
    /// <summary>
    /// Filter collection for user context queries.
    /// </summary>
    public static class PurchaseFilters
    {
        public static IQueryable<Purchase> WhereBillingEmailEquals(this IQueryable<Purchase> purchases, string billingEmail)
        {
            return purchases.Where(p => p.BillingEmail == billingEmail).AsQueryable();
        }

    }
}
