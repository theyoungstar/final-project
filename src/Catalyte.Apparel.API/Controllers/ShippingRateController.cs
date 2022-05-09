﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.DTOs.PromoCodes;
using Catalyte.Apparel.DTOs.ShippingRates;
using Catalyte.Apparel.Providers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalyte.Apparel.API.Controllers
{
    /// <summary>
    /// The PromoCodesController exposes endpoints for promo code related actions.
    /// </summary>
    [ApiController]
    [Route("/shippingrates")]
    public class ShippingRatesController : ControllerBase
    {
        private readonly ILogger<ShippingRatesController> _logger;
        private readonly IShippingRateProvider _shippingRateProvider;
        private readonly IMapper _mapper;

        public ShippingRatesController(
            ILogger<ShippingRatesController> logger,
            IShippingRateProvider shippingRateProvider,
            IMapper mapper
            )
        {
            _logger = logger;
            _mapper = mapper;
            _shippingRateProvider = shippingRateProvider;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShippingRateDTO>>> GetShippingRatesAsync()
        {
            _logger.LogInformation("Request received for GetShippingRatesAsync");

            var shippingRates = await _shippingRateProvider.GetShippingRatesAsync();
            var shippingRateDTOs = _mapper.Map<IEnumerable<ShippingRateDTO>>(shippingRates);

            return Ok(shippingRateDTOs);
        }

        [HttpGet("/{state}")]
        public async Task<ActionResult<ShippingRateDTO>> GetShippingRateByStateAsync(string state)
        {
            _logger.LogInformation($"Request received for GetShippingRateByStateAsync for state: {state}");

            var shippingRate = await _shippingRateProvider.GetShippingRateByStateAsync(state);
            var shippingRateDTO = _mapper.Map<ShippingRateDTO>(shippingRate);

            return Ok(shippingRateDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ShippingRateDTO>> CreateShippingRateAsync([FromBody] ShippingRate shippingRateToCreate)
        {
            _logger.LogInformation("Request received for CreateShippingRateAsync");

            var shippingRate = await _shippingRateProvider.CreateShippingRateAsync(shippingRateToCreate);
            var shippingRateDTO = _mapper.Map<ShippingRateDTO>(shippingRate);

            return Created("/shippingrates", shippingRateDTO);
        }
    }
}
