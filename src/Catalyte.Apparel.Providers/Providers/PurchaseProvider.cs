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
        private readonly CardValidation _cardValidation;
        private readonly IProductRepository _productRepository;

        public IPurchaseRepository Object1 { get; }
        public ILogger<PurchaseProvider> Object2 { get; }
        public CardValidation Object3 { get; }

        public PurchaseProvider(IPurchaseRepository purchaseRepository, IProductRepository productRepository, ILogger<PurchaseProvider> logger, CardValidation cardValidation)
        {
            _logger = logger;
            _purchaseRepository = purchaseRepository;
            _productRepository = productRepository;
            _cardValidation = cardValidation;
        }

        public PurchaseProvider(IPurchaseRepository object1, ILogger<PurchaseProvider> object2, CardValidation object3)
        {
            Object1 = object1;
            Object2 = object2;
            Object3 = object3;
        }

        /// <summary>
        /// Retrieves purchases from the repository that were filtered.
        /// </summary>
        /// <param name="billingEmail"> Billing email used to make purchase.</param>
        /// <returns>All purchases.</returns>

        public async Task<IEnumerable<Purchase>> GetAllPurchasesByEmailAsync(string billingEmail)
        {
            IEnumerable<Purchase> purchases;

            if (billingEmail == null || billingEmail == "")
            {
                _logger.LogInformation($"Purchases with email: {billingEmail} does not exist.");
                throw new NotFoundException($"Purchases with email: {billingEmail} does not exist.");
            }

            try
            {
                purchases = await _purchaseRepository.GetAllPurchasesByEmailAsync(billingEmail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return purchases;
        }


        /// <summary>
        /// Persists a purchase to the database.
        /// </summary>
        /// <param name="model">PurchaseDTO used to build the purchase.</param>
        /// <returns>The persisted purchase with IDs.</returns>
        public async Task<Purchase> CreatePurchasesAsync(Purchase newPurchase)
        {
            Purchase savedPurchase;
            List<string> errorsList = _cardValidation.CreditCardValidation(newPurchase);
            if (errorsList.Count > 0)
            {
                throw new BadRequestException(errorsList[0]);
            }

            List<string> inactiveItemsList = new List<string>();

            if (newPurchase.LineItems.Count == 0)
            {
                throw new ArgumentException("Purchase is empty and could not be completed");
            }
            foreach (var item in newPurchase.LineItems)
            {
                var product = await _productRepository.GetProductByIdAsync(item.ProductId);

                if (product.Active == false)
                {
                    inactiveItemsList.Add(product.Id.ToString());
                }
            }
            if (inactiveItemsList.Count > 0)
            {
                var inactiveItemsString = string.Join(",", inactiveItemsList);
                throw new UnprocessableEntityException($"Purchase could not be completed because the following product(s) are not active: {inactiveItemsString}");
            }
            else
            {
                try
                {
                    savedPurchase = await _purchaseRepository.CreatePurchaseAsync(newPurchase);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ServiceUnavailableException("There was a problem connecting to the database.");
                }
            }
            return savedPurchase;

        }


    }
}




