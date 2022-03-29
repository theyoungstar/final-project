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

        public static IQueryable<Product> WhereProductBrandEquals(this IQueryable<Product> products, string brand)
        {
            return products.Where(p => p.Brand == brand).AsQueryable();
        }

        public static IQueryable<Product> WhereProductCategoryEquals(this IQueryable<Product> products, string category)
        {
            return products.Where(p => p.Category == category).AsQueryable();
        }

        public static IQueryable<Product> WhereProductDemographicEquals(this IQueryable<Product> products, string demographic)
        {
            return products.Where(p => p.Demographic == demographic).AsQueryable();
        }

        public static IQueryable<Product> WhereProductPriceEquals(this IQueryable<Product> products, string price)
        {
            return products.Where(p => p.Price == price).AsQueryable();
        }

        public static IQueryable<Product> WhereProductPrimaryColorEquals(this IQueryable<Product> products, string color)
        {
            return products.Where(p => p.PrimaryColorCode == color).AsQueryable();
        }

        public static IQueryable<Product> WhereProductSecondaryColorEquals(this IQueryable<Product> products, string color)
        {
            return products.Where(p => p.SecondaryColorCode == color).AsQueryable();
        }

        public static IQueryable<Product> WhereProductMaterialEquals(this IQueryable<Product> products, string material)
        {
            return products.Where(p => p.Material == material).AsQueryable();
        }

    }
}
