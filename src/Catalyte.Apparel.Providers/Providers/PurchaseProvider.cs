﻿using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.Providers.Interfaces;
using Catalyte.Apparel.Utilities.HttpResponseExceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public PurchaseProvider(IPurchaseRepository purchaseRepository, ILogger<PurchaseProvider> logger, CardValidation cardValidation)
        {
            _logger = logger;
            _purchaseRepository = purchaseRepository;
            _cardValidation = cardValidation;
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
    }
}
