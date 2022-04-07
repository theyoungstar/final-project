using Catalyte.Apparel.Data.Model;
using System.Collections.Generic;
using System.Linq;

namespace Catalyte.Apparel.Data.Filters
{
    /// <summary>
    /// Filter collection for product context queries.
    /// </summary>
    public static class ProductFilters
    {
        public static IQueryable<Product> WhereProductIdEquals(this IQueryable<Product> products, int productId)
        {
            return products.Where(p => p.Id == productId).AsQueryable();
        }
        public static IQueryable<Product> WhereProductCategoryEquals(this IQueryable<Product> products, string category)
        {
            return products.Where(p => p.Category == category).AsQueryable();
        }
        public static IQueryable<Product> WhereProductTypeEquals(this IQueryable<Product> products, string type)
        {
            return products.Where(p => p.Category == type).AsQueryable();
        }
        
    }
}
