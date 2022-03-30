using Catalyte.Apparel.Data.Context;
using Catalyte.Apparel.Data.Filters;
using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        //public async Boolean CheckProductForActiveAsync(bool Active)
        public async Task<Product> CheckProductForActiveAsync(bool Active)
        {
            return await _ctx.Products
                .AsNoTracking()
                .WhereProductStatusEquals(true)
                .SingleOrDefaultAsync();
        }


        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _ctx.Products
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Product> CheckProductForActiveAsync(int productId)
        {
            throw new NotImplementedException();
        }
    }

}
