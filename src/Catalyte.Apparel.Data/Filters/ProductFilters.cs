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
            return products.Where(p => p.Category == category||p.Category == null).AsQueryable();
        }
        public static IQueryable<Product> WhereProductTypeEquals(this IQueryable<Product> products, string type)
        {
            return products.Where(p => p.Type == type||p.Type == null).AsQueryable();
        }
        public static IQueryable<Product> WhereProductDemographicEquals(this IQueryable<Product> products, string demographic)
        {
            return products.Where(p => p.Demographic == demographic||p.Demographic == null).AsQueryable();
        }
        public static IQueryable<Product> WhereProductPriceEquals(this IQueryable<Product> products, string price)
        {
            return products.Where(p => p.Price == price||p.Price == null).AsQueryable();
        }
        public static IQueryable<Product> WhereProductPrimaryColorCodeEquals(this IQueryable<Product> products, string colorCode)
        {
            return products.Where(p => p.PrimaryColorCode == colorCode||p.PrimaryColorCode == null).AsQueryable();
        }
        public static IQueryable<Product> WhereProductSecondaryColorCodeEquals(this IQueryable<Product> products, string colorCode)
        {
            return products.Where(p => p.SecondaryColorCode == colorCode||p.SecondaryColorCode == null).AsQueryable();
        }
        public static IQueryable<Product> WhereProductMaterialEquals(this IQueryable<Product> products, string material)
        {
            return products.Where(p => p.Material == material||p.Material == null).AsQueryable();
        }
        public static IQueryable<Product> WhereProductBrandEquals(this IQueryable<Product> products, string brand)
        {
            return products.Where(p => p.Brand == brand||p.Brand == null).AsQueryable();
        }
    }
}
