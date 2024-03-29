﻿using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.Providers.Interfaces;
using Catalyte.Apparel.Utilities.HttpResponseExceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            if (newPromoCode.Type.ToLower() != "flat" && newPromoCode.Type != "percent")
            {
                _logger.LogError("Promo Code must have a type of \"flat\" or \"percent\".");
                throw new BadRequestException("Promo Code must have a type of \"flat\" or \"percent\".");
            }

            // Prevents promo code from persisting if rate is entered with a negative sign.
            if (newPromoCode.Rate.ToString().Contains('-'))
            {
                _logger.LogError("Promo Code rate must not be negative.");
                throw new BadRequestException("Promo Code rate must not be negative.");
            }

            //// Validation to only submit with numbers and decimals for flat rate
            if (newPromoCode.Type.ToLower().Equals("flat"))
            {
                Regex rx = new(@"^(0(?!\.00)|[1-9]\d{0,6})\.\d{2}$");
                string pattern = newPromoCode.Rate.ToString();
                //if (pattern.Contains("-"))
                if (!rx.IsMatch(pattern))
                {
                    _logger.LogError("Flat rate promo codes must contain two digits following a decimal. Example: 10.99 or 100.00.");
                    throw new BadRequestException("Flat rate promo codes must contain two digits following the decimal. Example: 10.99 or 100.00.");
                }
            }
            // Validation for % rate
            if (newPromoCode.Type.Equals("percent"))
            {
                //  Prevents promo code from persisting if rate is entered with decimal.
                if (newPromoCode.Rate.ToString().Contains("."))
                {
                    _logger.LogError("Percent promo code rate must be a whole number.");
                    throw new BadRequestException("Percent promo code rate must be a whole number.");
                }
                // Prevents promo code from persisting if rate is over 100 or less than 0.
                if (newPromoCode.Rate > 99 || newPromoCode.Rate < 1)
                {
                    _logger.LogError("Percent promo code rate must be between 1-99.");
                    throw new BadRequestException("Percent promo code rate must be between 1-99.");
                }
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