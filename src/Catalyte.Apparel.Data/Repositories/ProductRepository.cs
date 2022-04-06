using Catalyte.Apparel.Data.Context;
using Catalyte.Apparel.Data.Filters;
using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

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
                .ToListAsync();
        }
        public async Task<IEnumerable<string>> GetAllUniqueCategoriesAsync()
        {
            return await GetAllUniquesOf(x => x.Category);
        }
        public async Task<IEnumerable<string>> GetAllUniqueTypesAsync()
        {
            return await GetAllUniquesOf(x => x.Type);
        }
        private async Task<IEnumerable<string>> GetAllUniquesOf(Func<Product,string> select)
        {
            var products = await _ctx.Products
                .AsNoTracking()
                .ToListAsync();
            var uniques = new HashSet<string>(products.Select(select)).ToList();
            uniques.Sort();

            return uniques;
        }
        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
        {
            return await _ctx.Products
                .AsNoTracking()
                .WhereProductCategoryEquals(category)
                .ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsByTypeAsync(string type)
        {
            return await _ctx.Products
                .AsNoTracking()
                .WhereProductTypeEquals(type)
                .ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsByDemographicAsync(string demographic)
        {
            return await _ctx.Products
                .AsNoTracking()
                .WhereProductDemographicEquals(demographic)
                .ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsByPrimaryColorCodeAsync(string primaryColorCode)
        {
            return await _ctx.Products
                .AsNoTracking()
                .WhereProductPrimaryColorCodeEquals(primaryColorCode)
                .ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsBySecondaryColorCodeAsync(string secondaryColorCode)
        {
            return await _ctx.Products
                .AsNoTracking()
                .WhereProductPrimaryColorCodeEquals(secondaryColorCode)
                .ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsByMaterialAsync(string material)
        {
            return await _ctx.Products
                .AsNoTracking()
                .WhereProductMaterialEquals(material)
                .ToListAsync();
        }
    } 

}
