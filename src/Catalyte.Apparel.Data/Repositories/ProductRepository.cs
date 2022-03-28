using Catalyte.Apparel.Data.Context;
using Catalyte.Apparel.Data.Filters;
using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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
        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync()
        {
            return await _ctx.Products
                .AsNoTracking()
                .Select(p => new Product { Category = p.Category})
                .Distinct()
                .ToListAsync();
                
                   
        }
        public async Task<IEnumerable<Product>> GetProductsByTypeAsync()
        {
            return await _ctx.Products
                .AsNoTracking()
                .Select(p => new Product { Type = p.Type })
                .Distinct()
                .ToListAsync();
        }
    } 

}
