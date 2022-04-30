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
        /// <summary>
        /// Persists a promo code to the database given the provided title is not already in the database or null
        /// and type field is valid.
        /// </summary>
        /// <param name="newPromoCode">The promo code to persist.</param>
        /// <returns>The promo code.</returns>
        public async Task<PromoCode> CreatePromoCodeAsync(PromoCode newPromoCode)
        {
            if (newPromoCode.Title == null)
            {
                _logger.LogError("Promo Code must have a title field.");
                throw new BadRequestException("Promo Code must have an title field");
            }

            if (newPromoCode.Type == null)
            {
                _logger.LogError("Promo Code must have a type.");
                throw new BadRequestException("Promo Code must have a type");
            }

            //If Type is not "flat" or "%" throw error
            if (newPromoCode.Type.ToLower() != "flat" && newPromoCode.Type != "%")
            {
                _logger.LogError("Promo Code must have a type of \"flat\" or \"percent\".");
                throw new BadRequestException("Promo Code must have a type of \"flat\" or \"percent\".");
            }

            // Prevents promo code from persisting if rate is over 100.
            if (newPromoCode.Rate > 100)
            {
                _logger.LogError("Promo Code rate must not exceed 100.");
                throw new BadRequestException("Promo Code rate must not exceed 100.");
            }

            // CHECK TO MAKE SURE THE PROMOCODE TITLE IS NOT TAKEN
            PromoCode existingPromoCode;

            try
            {
                existingPromoCode = await _promoCodeRepository.GetPromoCodeByTitleAsync(newPromoCode.Title);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            if (existingPromoCode != default)
            {
                _logger.LogError("Title is taken.");
                throw new ConflictException("Title is taken");
            }

            PromoCode savedPromoCode;

            try
            {
                savedPromoCode = await _promoCodeRepository.CreatePromoCodeAsync(newPromoCode);
                _logger.LogInformation("Promo code saved.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return savedPromoCode;
        }
    }
}
