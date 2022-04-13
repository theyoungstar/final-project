using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.DTOs.Products;
using Catalyte.Apparel.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Providers.Interfaces
{
    /// <summary>
    /// This interface provides an abstraction layer for product related service methods.
    /// </summary>
    public interface IProductProvider
    {
        Task<Product> GetProductByIdAsync(int productId);

        Task<IEnumerable<Product>> GetProductsAsync();

        Task<IEnumerable<string>> GetAllUniqueCategoriesAsync();

        Task<IEnumerable<string>> GetAllUniqueTypesAsync();

        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);

        Task<IEnumerable<Product>> GetProductsByTypeAsync(string type);

        Task<IEnumerable<Product>> GetProductsByDemographicAsync(List<string> demographic);

        Task<IEnumerable<Product>> GetProductsByPrimaryColorCodeAsync(string primaryColorCode);

        Task<IEnumerable<Product>> GetProductsBySecondaryColorCodeAsync(string secondaryColorCode);

        Task<IEnumerable<Product>> GetProductsByMaterialAsync(List<string> material);

        Task<IEnumerable<Product>> GetProductsByBrandAsync(List<string> brand);

        Task<IEnumerable<Product>> GetProductsByAllFiltersAsync(List<string> brand, string category, string type, List<string> demographic, string primaryColorCode, string secondaryColorCode, List<string> material, string price);
    }
}
