using Catalyte.Apparel.Data.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Data.Interfaces
{
    /// <summary>
    /// This interface provides an abstraction layer for product repository methods.
    /// </summary>
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int productId);

        Task<IEnumerable<Product>> GetProductsAsync();

        Task<IEnumerable<string>> GetAllUniqueCategoriesAsync();

        Task<IEnumerable<string>> GetAllUniqueTypesAsync();

        Task<IEnumerable<Product>> GetProductsByAllFiltersAsync(int pageNumber, List<string> brand, List<string> category, List<string> type, List<string> demographic, List<string> primaryColorCode, List<string> secondaryColorCode, List<string> material, double min, double max);
                
        Task<double> GetActiveProductsCountAsync();

        
    } 
  
}
