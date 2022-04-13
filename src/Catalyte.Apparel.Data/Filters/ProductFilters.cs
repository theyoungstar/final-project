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
            if(category == null) { return products; }

            return products.Where(p => p.Category == category).AsQueryable();
        }
        public static IQueryable<Product> WhereProductTypeEquals(this IQueryable<Product> products, string type)
        {
            if(type == null) { return products; }
 
            return products.Where(p => p.Type == type).AsQueryable();
        }
        public static IQueryable<Product> WhereProductDemographicEquals(this IQueryable<Product> products, List<string> demographic)
        {
            if(demographic.Count == 0) { return products; }

            return products.Where(p => demographic.Contains(p.Demographic)).AsQueryable();
        }
        public static IQueryable<Product> WhereProductPriceEquals(this IQueryable<Product> products, double min, double max)
        {
            if(min == 0 && max == 0) { return products; }
            if(min > 0 && max == 0) { return products.Where(p => p.Price >= min).AsQueryable(); }

            return products.Where(p => p.Price <= max && p.Price >= min).AsQueryable();
        }
        public static IQueryable<Product> WhereProductPrimaryColorCodeEquals(this IQueryable<Product> products, string colorCode)
        {
            if(colorCode == null) { return products; }

            return products.Where(p => p.PrimaryColorCode == colorCode).AsQueryable();
        }
        public static IQueryable<Product> WhereProductSecondaryColorCodeEquals(this IQueryable<Product> products, string colorCode)
        {
            if(colorCode == null) { return products; }    
            return products.Where(p => p.SecondaryColorCode == colorCode).AsQueryable();
        }
        public static IQueryable<Product> WhereProductMaterialEquals(this IQueryable<Product> products, List<string> material)
        {
            if(material.Count == 0) { return products; }

            return products.Where(p => material.Contains(p.Material)).AsQueryable();
        }
        public static IQueryable<Product> WhereProductBrandEquals(this IQueryable<Product> products, List<string> brand)
        {
            if(brand.Count == 0) { return products; }

            return products.Where(p => brand.Contains(p.Brand)).AsQueryable();
        }
        
    }
}
