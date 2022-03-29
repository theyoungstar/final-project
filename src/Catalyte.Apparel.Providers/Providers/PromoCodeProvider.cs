using AutoMapper;
using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.DTOs.PromoCodes;
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
    /// This class provides the implementation of the IPromoCodeProvider interface, providing service methods for products.
    /// </summary>
    public class PromoCodeProvider : IPromoCodeProvider
    {
        private readonly ILogger<PromoCodeProvider> _logger;
        private readonly IPromoCodeRepository _promoCodeRepository;

        public PromoCodeProvider(IPromoCodeRepository promoCodeRepository, ILogger<PromoCodeProvider> logger)
        {
            _logger = logger;
            _promoCodeRepository = promoCodeRepository;
        }

        /// <summary>
        /// Asynchronously retrieves the promocode with the provided id from the database.
        /// </summary>
        /// <param name="promoCodeId">The id of the promocode to retrieve.</param>
        /// <returns>The promocode.</returns>
        public async Task<PromoCode> GetPromoCodeByIdAsync(int promoCodeId)
        {
            PromoCode promoCode;

            try
            {
                promoCode = await _promoCodeRepository.GetPromoCodeByIdAsync(promoCodeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            if (promoCode == null || promoCode == default)
            {
                _logger.LogInformation($"PromoCode with id: {promoCodeId} could not be found.");
                throw new NotFoundException($"PromoCode with id: {promoCodeId} could not be found.");
            }

            return promoCode;
        }

        /// <summary>
        /// Asynchronously retrieves all promoCodes from the database.
        /// </summary>
        /// <returns>All promoCodes in the database.</returns>
        public async Task<IEnumerable<PromoCode>> GetPromoCodesAsync()
        {
            IEnumerable<PromoCode> promoCodes;

            try
            {
                promoCodes = await _promoCodeRepository.GetPromoCodesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return promoCodes;
        }
    }
}
