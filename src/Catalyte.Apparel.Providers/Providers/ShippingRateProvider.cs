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
    /// This class provides the implementation of the IShippingRateProvider interface, providing service methods for products.
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
        /// Asynchronously retrieves the shipping rate with the provided id from the database.
        /// </summary>
        /// <param name="state">The state of the shipping rate to retrieve.</param>
        /// <returns>The shipping rate.</returns>
        public async Task<ShippingRate> GetShippingRateByStateAsync(string state)
        {
            ShippingRate shippingRate;

            try
            {
                shippingRate = await _shippingRateRepository.GetShippingRateByStateAsync(state);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            if (shippingRate == null || shippingRate == default)
            {
                _logger.LogInformation($"ShippingRate with state: {state} could not be found.");
                throw new NotFoundException($"ShippingRate with state: {state} could not be found.");
            }

            return shippingRate;
        }

        /// <summary>
        /// Asynchronously retrieves all shipping rates from the database.
        /// </summary>
        /// <returns>All shipping rates in the database.</returns>
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
        /// Persists a shipping rate to the database.
        /// </summary>
        /// <param name="newShippingRate">The shipping rate to persist.</param>
        /// <returns>The shipping rate model.</returns>
        public async Task<ShippingRate> CreateShippingRateAsync(ShippingRate newShippingRate)
        {
            if (newShippingRate.State == null)
            {
                _logger.LogError("Shipping Rate must have a state field.");
                throw new BadRequestException("Shipping Rate must have a state field");
            }

            // Check to make sure the shipping rate state is not already entered
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
                _logger.LogError("Shipping rate for state already in the database.");
                throw new ConflictException("Shipping rate for state already in the database");
            }

            ShippingRate savedShippingRate;

            try
            {
                savedShippingRate = await _shippingRateRepository.CreateShippingRateAsync(newShippingRate);
                _logger.LogInformation("Shipping rate saved.");
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