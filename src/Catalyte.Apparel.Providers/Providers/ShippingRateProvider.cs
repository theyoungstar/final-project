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
    /// This class provides the implementation of the IPromoCodeProvider interface, providing service methods for products.
    /// </summary>
    public class ShippingRateProvider : IShippingRateProvider
    {
        private readonly ILogger<ShippingRateProvider> _logger;
        private readonly IShippingRateRepository _shippingRateRepository;

        public ShippingRateProvider(IShippingRateRepository shippingRateRepository, ILogger<ShippingRateProvider> logger)
        {
            _logger = logger;
            _shippingRateRepository = shippingRateRepository;
        }

        /// <summary>
        /// Asynchronously retrieves the promocode with the provided id from the database.
        /// </summary>
        /// <param name="promoCodeId">The id of the promocode to retrieve.</param>
        /// <returns>The promocode.</returns>
        public async Task<ShippingRate> GetShippingRateByStateAsync(string shippingRateState)
        {
            ShippingRate shippingRate;

            try
            {
                shippingRate = await _shippingRateRepository.GetShippingRateByStateAsync(shippingRateState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            if (shippingRate == null || shippingRate == default)
            {
                _logger.LogInformation($"ShippingRate with state: {shippingRateState} could not be found.");
                throw new NotFoundException($"ShippingRate with state: {shippingRateState} could not be found.");
            }

            return shippingRate;
        }

        /// <summary>
        /// Asynchronously retrieves all promoCodes from the database.
        /// </summary>
        /// <returns>All promoCodes in the database.</returns>
        public async Task<IEnumerable<ShippingRate>> GetShippingRatesAsync()
        {
            IEnumerable<ShippingRate> shippingRates;

            try
            {
                shippingRates = await _shippingRateRepository.GetShippingRatesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return shippingRates;
        }
        /// <summary>
        /// Persists a promo code to the database given the provided title is not already in the database or null
        /// and type field is valid.
        /// </summary>
        /// <param name="newPromoCode">The promo code to persist.</param>
        /// <returns>The promo code.</returns>
        public async Task<ShippingRate> CreateShippingRateAsync(ShippingRate newShippingRate)
        {
            if (newShippingRate.State == null)
            {
                _logger.LogError("Shipping Rate must have a state field.");
                throw new BadRequestException("Shipping Rate must have a state field");
            }

            // CHECK TO MAKE SURE THE PROMOCODE TITLE IS NOT TAKEN
            ShippingRate existingShippingRate;

            try
            {
                existingShippingRate = await _shippingRateRepository.GetShippingRateByStateAsync(newShippingRate.State);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            if (existingShippingRate != default)
            {
                _logger.LogError("Title is taken.");
                throw new ConflictException("Title is taken");
            }

            ShippingRate savedShippingRate;

            try
            {
                savedShippingRate = await _shippingRateRepository.CreateShippingRateAsync(newShippingRate);
                _logger.LogInformation("Promo code saved.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return savedShippingRate;
        }
    }
}