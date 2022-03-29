using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.Providers.Interfaces;
using Catalyte.Apparel.Utilities.HttpResponseExceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Providers.Providers
{
    /// <summary>
    /// This class provides the implementation of the IPurchaseProvider interface, providing service methods for purchases.
    /// </summary>
    public class PurchaseProvider : IPurchaseProvider
    {
        private readonly ILogger<PurchaseProvider> _logger;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IProductRepository _productRepository;

        public PurchaseProvider(IPurchaseRepository purchaseRepository, ILogger<PurchaseProvider> logger)
        {
            _logger = logger;
            _purchaseRepository = purchaseRepository;
        }

        /// <summary>
        /// Retrieves all purchases from the database.
        /// </summary>
        /// <param name="page">Number of pages.</param>
        /// <param name="pageSize">How many purchases per page.</param>
        /// <returns>All purchases.</returns>
        public async Task<IEnumerable<Purchase>> GetAllPurchasesAsync()
        {
            List<Purchase> purchases;

            try
            {
                purchases = await _purchaseRepository.GetAllPurchasesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return purchases;
        }

        public async Task<Product> CheckProductForActiveAsync(int productId)
        {
            Product savedProduct;

            try
            {
                savedProduct = await _productRepository.CheckProductForActiveAsync(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ArgumentException("Purchase could not be completed the following products are not active [list of inactive products that they tried to buy].");
            }

            return savedProduct;
        }

        /// <summary>
        /// Persists a purchase to the database.
        /// </summary>
        /// <param name="model">PurchaseDTO used to build the purchase.</param>
        /// <returns>The persisted purchase with IDs.</returns>
        public async Task<Purchase> CreatePurchasesAsync(Purchase newPurchase)
        {
            Purchase savedPurchase;

            try
            {
                savedPurchase = await _purchaseRepository.CreatePurchaseAsync(newPurchase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return savedPurchase;
        }

        Task<IEnumerable<Purchase>> IPurchaseProvider.GetAllPurchasesAsync()
        {
            throw new NotImplementedException();
        }

        Task<Purchase> IPurchaseProvider.CreatePurchasesAsync(Purchase model)
        {
            throw new NotImplementedException();
        }

        Task<Product> IPurchaseProvider.CheckProductForActiveAsync(int productId)
        {
            throw new NotImplementedException();
        }

        Task IPurchaseProvider.CheckProductForActiveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
