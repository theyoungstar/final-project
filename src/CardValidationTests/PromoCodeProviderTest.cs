using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.Data.Repositories;
using Catalyte.Apparel.Data.SeedData;
using Catalyte.Apparel.DTOs.PromoCodes;
using Catalyte.Apparel.Providers.Interfaces;
using Catalyte.Apparel.Providers.Providers;
using Catalyte.Apparel.Utilities.HttpResponseExceptions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Catalyte.Apparel.Test.Unit
{
    public class PromoCodeProviderTest
    {
        private readonly Mock<IPromoCodeRepository> _mockRepository;
        private readonly Mock<ILogger<PromoCodeProvider>> _mockLogger;
        private readonly PromoCodeProvider _provider;

        public PromoCodeProviderTest()
        {
            _mockRepository = new Mock<IPromoCodeRepository>();
            _mockLogger = new Mock<ILogger<PromoCodeProvider>>();
            _provider = new PromoCodeProvider(_mockRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task CreatePromoCodeAsync_RateOver100_ShouldThrowException()
        {
            var promo1 = new PromoCode()
            {
                Id = 1,
                Title = "CODE01",
                Type = "%",
                Description = "Description of Promo",
                Rate = 150,
            };
            Func<Task> result = async () => { await _provider.CreatePromoCodeAsync(promo1); };

            await result.Should().ThrowAsync<BadRequestException>();
        }

        [Fact]
        public async Task CreatePromoCodeAsync_RateUnder100_ShouldNotThrowException()
        {
            var promo1 = new PromoCode()
            {
                Id = 1,
                Title = "CODE01",
                Type = "%",
                Description = "Description of Promo",
                Rate = 50,
            };

            Func<Task> result = async () => { await _provider.CreatePromoCodeAsync(promo1); };

            await result.Should().NotThrowAsync<BadRequestException>();
        }

    }
}

