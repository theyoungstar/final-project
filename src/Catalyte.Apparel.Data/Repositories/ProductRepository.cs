using Catalyte.Apparel.Data.Context;
using Catalyte.Apparel.Data.Filters;
using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Catalyte.Apparel.Data.Repositories
{
    /// <summary>
    /// This class handles methods for making requests to the product repository.
    /// </summary>

    public class ProductRepository : IProductRepository
    {
        private readonly IApparelCtx _ctx;

        public ProductRepository(IApparelCtx ctx)
        {
            _ctx = ctx;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _ctx.Products
                .AsNoTracking()
                .WhereProductIdEquals(productId)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _ctx.Products
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .ToListAsync();
        }
        
       
        public async Task<double> GetProductsCountByFilterAsync(List<string> brand, List<string> category, List<string> type, List<string> demographic, List<string> primaryColorCode, List<string> secondaryColorCode, List<string> material, double min, double max)
        {
            var products = await _ctx.Products
                .AsNoTracking()
                .WhereProductBrandEquals(brand)
                .WhereProductCategoryEquals(category)
                .WhereProductTypeEquals(type)
                .WhereProductDemographicEquals(demographic)
                .WhereProductPrimaryColorCodeEquals(primaryColorCode)
                .WhereProductSecondaryColorCodeEquals(secondaryColorCode)
                .WhereProductMaterialEquals(material)
                .WhereProductPriceEquals(min, max)
                .WhereProductEqualsActive()
                .ToListAsync();
            var totalActive = products.Count();

            return totalActive;
                
        }

        public async Task<IEnumerable<string>> GetAllUniqueCategoriesAsync()
        {
            return await GetAllUniquesOf(x => x.Category);
        }
        public async Task<IEnumerable<string>> GetAllUniqueTypesAsync()
        {
            return await GetAllUniquesOf(x => x.Type);
        }
        private async Task<IEnumerable<string>> GetAllUniquesOf(Func<Product, string> select)
        {
            var products = await _ctx.Products
                .AsNoTracking()
                .ToListAsync();
            var uniques = new HashSet<string>(products.Select(select)).ToList();
            uniques.Sort();

            return uniques;
        }
  
        public async Task<IEnumerable<Product>> GetProductsByAllFiltersAsync(int pageNumber, List<string> brand, List<string> category, List<string> type, List<string> demographic, List<string> primaryColorCode, List<string> secondaryColorCode, List<string> material, double min, double max)
        {
            return await _ctx.Products.AsNoTracking()
                .WhereProductBrandEquals(brand)
                .WhereProductCategoryEquals(category)
                .WhereProductTypeEquals(type)
                .WhereProductDemographicEquals(demographic)
                .WhereProductPrimaryColorCodeEquals(primaryColorCode)
                .WhereProductSecondaryColorCodeEquals(secondaryColorCode)
                .WhereProductMaterialEquals(material)
                .WhereProductPriceEquals(min, max)
                .WhereProductEqualsActive()
                .OrderBy(p => p.Id)
                .Skip((pageNumber-1) *20)
                .Take(20)
                .ToListAsync();
        }
        public async Task<Product> UpdateProductAsync(Product product)
        {
            _ctx.Products.Update(product);
            await _ctx.SaveChangesAsync();
            return product;

        }

        public async Task<Product> CreateProductAsync(Product newProduct)
        {
            _ctx.Products.Add(newProduct);
            await _ctx.SaveChangesAsync();

            return newProduct;
        }

    }




}

