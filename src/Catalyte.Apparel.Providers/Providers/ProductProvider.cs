using AutoMapper;
using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.DTOs.Products;
using Catalyte.Apparel.Providers.Interfaces;
using Catalyte.Apparel.Utilities;
using Catalyte.Apparel.Utilities.HttpResponseExceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Providers.Providers
{
    /// <summary>
    /// This class provides the implementation of the IProductProvider interface, providing service methods for products.
    /// </summary>
    public class ProductProvider : IProductProvider
    {
        private readonly ILogger<ProductProvider> _logger;
        private readonly IProductRepository _productRepository;

        public ProductProvider(IProductRepository productRepository, ILogger<ProductProvider> logger)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        /// <summary>
        /// Asynchronously retrieves the product with the provided id from the database.
        /// </summary>
        /// <param name="productId">The id of the product to retrieve.</param>
        /// <returns>The product.</returns>
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            Product product;

            try
            {
                product = await _productRepository.GetProductByIdAsync(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            if (product == null || product == default)
            {
                _logger.LogInformation($"Product with id: {productId} could not be found.");
                throw new NotFoundException($"Product with id: {productId} could not be found.");
            }

            return product;
        }

        /// <summary>
        /// Asynchronously retrieves all products from the database.
        /// </summary>
        /// <returns>All products in the database.</returns>
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            IEnumerable<Product> products;

            try
            {
                products = await _productRepository.GetProductsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return products;
        }
        /// <summary>
        /// Asynchronously retrieves all unique product categories currently in the database.
        /// </summary>
        /// <returns>A list of all unique product categories.</returns>
        /// <exception cref="ServiceUnavailableException"></exception>
        public async Task<IEnumerable<string>> GetAllUniqueCategoriesAsync()
        {
            IEnumerable<string> categories;

            try
            {
                categories = await _productRepository.GetAllUniqueCategoriesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return categories;
        }
        /// <summary>
        /// Asynchronously retrieves all unique product types currently in the database. 
        /// </summary>
        /// <returns>A list of all unique product types.</returns>
        /// <exception cref="ServiceUnavailableException"></exception>
        public async Task<IEnumerable<string>> GetAllUniqueTypesAsync()
        {
            IEnumerable<string> types;
            try
            {
                types = await _productRepository.GetAllUniqueTypesAsync();
            }
            catch(Exception ex)
            {
                _logger?.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }
            return types;
        }
        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
        {
            IEnumerable<Product> products;

            try
            {
                products = await _productRepository.GetProductsByCategoryAsync(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return products;
        }
        public async Task<IEnumerable<Product>> GetProductsByTypeAsync(string type)
        {
            IEnumerable<Product> products;

            try
            {
                products = await _productRepository.GetProductsByTypeAsync(type);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return products;
        }
        public async Task<IEnumerable<Product>> GetProductsByDemographicAsync(List<string> demographic)
        {
            IEnumerable<Product> products;

            try
            {
                products = await _productRepository.GetProductsByDemographicAsync(demographic);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return products;
        }
        public async Task<IEnumerable<Product>> GetProductsByPrimaryColorCodeAsync(List<string> primaryColorCode)
        {
            IEnumerable<Product> products;

            try
            {
                products = await _productRepository.GetProductsByPrimaryColorCodeAsync(primaryColorCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return products;
        }
        public async Task<IEnumerable<Product>> GetProductsBySecondaryColorCodeAsync(List<string> secondaryColorCode)
        {
            IEnumerable<Product> products;

            try
            {
                products = await _productRepository.GetProductsBySecondaryColorCodeAsync(secondaryColorCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return products;
        }
        public async Task<IEnumerable<Product>> GetProductsByMaterialAsync(List<string> material)
        {
            IEnumerable<Product> products;

            try
            {
                products = await _productRepository.GetProductsByMaterialAsync(material);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return products;
        }
        public async Task<IEnumerable<Product>> GetProductsByBrandAsync(List<string> brand)
        {
            IEnumerable<Product> products;

            try
            {
                products = await _productRepository.GetProductsByBrandAsync(brand);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return products;
        }
        /// <summary>
        /// Asynchrously retrieves products based on filter parameters passed in the HTTP request.
        /// </summary>
        /// <param name="brands">The brands of the products you want to see.</param>
        /// <param name="category">The categories of products you want to see.</param>
        /// <param name="type">The type of products you want to see.</param>
        /// <param name="demographic">The demographics of products you want to see.</param>
        /// <param name="primaryColorCode">The color code of products you want to see.</param>
        /// <param name="secondaryColorCode">The color code of products you want to see.</param>
        /// <param name="material">The material of products you want to see.</param>
        /// <param name="price">The price range of products you want to see.</param>
        /// <returns>Filtered list of products.</returns>
        /// <exception cref="ServiceUnavailableException"></exception>
        public async Task<IEnumerable<Product>> GetProductsByAllFiltersAsync(List<string> brands, string category, string type, List<string> demographic, List<string> primaryColorCode, List<string> secondaryColorCode, List<string> material, double min, double max)
        {
            IEnumerable<Product> products;
            if(min > max)
            {
                throw new ServiceUnavailableException("Minimum price value must be less than the maximum.");
            }

            try
            {
                products = await _productRepository.GetProductsByAllFiltersAsync(brands, category, type, demographic, primaryColorCode, secondaryColorCode, material, min, max);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return products;
        }
    }
}
