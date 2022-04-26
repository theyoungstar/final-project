using Catalyte.Apparel.Data.Model;
using System.Linq;
using System.Collections.Generic;

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
        public static IQueryable<Product> WhereProductCategoryEquals(this IQueryable<Product> products, List<string> category)
        {
            if (category.Count == 0) { return products; }

            return products.Where(p => category.Contains(p.Category)).AsQueryable();
        }
        public static IQueryable<Product> WhereProductTypeEquals(this IQueryable<Product> products, List<string> type)
        {
            if (type.Count == 0) { return products; }

            return products.Where(p => type.Contains(p.Type)).AsQueryable();
        }
        public static IQueryable<Product> WhereProductDemographicEquals(this IQueryable<Product> products, List<string> demographic)
        {
            if (demographic.Count == 0) { return products; }

            return products.Where(p => demographic.Contains(p.Demographic)).AsQueryable();
        }
        public static IQueryable<Product> WhereProductPriceEquals(this IQueryable<Product> products, double min, double max)
        {
            if (min == 0 && max == 0) { return products; }
            if (min > 0 && max == 0) { return products.Where(p => p.Price >= min).AsQueryable(); }

            return products.Where(p => p.Price <= max && p.Price >= min).AsQueryable();
        }
        public static IQueryable<Product> WhereProductPrimaryColorCodeEquals(this IQueryable<Product> products, List<string> colorCode)
        {
            if (colorCode.Count == 0) { return products; }

            return products.Where(p => colorCode.Contains(p.PrimaryColorCode)).AsQueryable();
        }
        public static IQueryable<Product> WhereProductSecondaryColorCodeEquals(this IQueryable<Product> products, List<string> colorCode)
        {
            if (colorCode.Count == 0) { return products; }
            return products.Where(p => colorCode.Contains(p.SecondaryColorCode)).AsQueryable();
        }
        public static IQueryable<Product> WhereProductMaterialEquals(this IQueryable<Product> products, List<string> material)
        {
            if (material.Count == 0) { return products; }

            return products.Where(p => material.Contains(p.Material)).AsQueryable();
        }
        public static IQueryable<Product> WhereProductBrandEquals(this IQueryable<Product> products, List<string> brand)
        {
            if (brand.Count == 0) { return products; }

            return products.Where(p => brand.Contains(p.Brand)).AsQueryable();
        }
        public static IQueryable<Product> WhereProductEqualsActive(this IQueryable<Product> products)
        {
            return products.Where(p => p.Active == true).AsQueryable();
        }
    }
}
