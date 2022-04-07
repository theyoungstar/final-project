﻿using AutoMapper;
using Catalyte.Apparel.DTOs.PromoCodes;
using Catalyte.Apparel.Providers.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Catalyte.Apparel.Data.Model;

namespace Catalyte.Apparel.API.Controllers
{
    /// <summary>
    /// The PromoCodesController exposes endpoints for promo code related actions.
    /// </summary>
    [ApiController]
    [Route("/promocodes")]
    public class PromoCodesController : ControllerBase
    {
        private readonly ILogger<PromoCodesController> _logger;
        private readonly IPromoCodeProvider _promoCodeProvider;
        private readonly IMapper _mapper;

        public PromoCodesController(
            ILogger<PromoCodesController> logger,
            IPromoCodeProvider promoCodeProvider,
            IMapper mapper
            )
        {
            _logger = logger;
            _mapper = mapper;
            _promoCodeProvider = promoCodeProvider;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromoCodeDTO>>> GetPromoCodesAsync()
        {
            _logger.LogInformation("Request received for GetPromoCodesAsync");

            var promoCodes = await _promoCodeProvider.GetPromoCodesAsync();
            var promoCodeDTOs = _mapper.Map<IEnumerable<PromoCodeDTO>>(promoCodes);

            return Ok(promoCodeDTOs);
        }

        [HttpGet("/{id}")]
        public async Task<ActionResult<PromoCodeDTO>> GetPromoCodeByIdAsync(int id)
        {
            _logger.LogInformation($"Request received for GetPromoCodeByIdAsync for id: {id}");

            var promoCode = await _promoCodeProvider.GetPromoCodeByIdAsync(id);
            var promoCodeDTO = _mapper.Map<PromoCodeDTO>(promoCode);

            return Ok(promoCodeDTO);
        }

        [HttpPost]
        public async Task<ActionResult<PromoCodeDTO>> CreatePromoCodeAsync([FromBody] PromoCode promoCodeToCreate)
        {
            _logger.LogInformation("Request received for CreatePromoCodeAsync");

            var promoCode = await _promoCodeProvider.CreatePromoCodeAsync(promoCodeToCreate);
            var promoCodeDTO = _mapper.Map<PromoCodeDTO>(promoCode);

            return Created("/promoCodes", promoCodeDTO);
        }
    }
}
