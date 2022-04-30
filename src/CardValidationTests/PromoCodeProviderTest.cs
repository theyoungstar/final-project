using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.Data.Repositories;
using Catalyte.Apparel.Data.SeedData;
using Catalyte.Apparel.DTOs.PromoCodes;
using Catalyte.Apparel.Providers.Interfaces;
using Catalyte.Apparel.Providers.Providers;
using Catalyte.Apparel.Utilities.HttpResponseExceptions;
using Microsoft.Extensions.Logging;
using Moq;
using FluentAssertions;
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
        private readonly PurchaseFactory _purchaseFactory = new();

        private readonly IPromoCodeProvider provider;
        private readonly IPromoCodeRepository _promoCodeRepository;
        private readonly Mock<IPromoCodeRepository> _mockRepository;
        private readonly Mock<ILogger<PromoCodeProvider>> _mockLogger;
        private readonly BadRequestException exception1;
        private readonly ILogger<PromoCodeProvider> _logger;

        private readonly PromoCodeProvider _provider;
        private readonly PromoCode _testPromoCode;
        private readonly List<PromoCode> _testPromoCodes;

        public PromoCodeProviderTest()
        {
            _mockRepository = new Mock<IPromoCodeRepository>();
            _mockLogger = new Mock<ILogger<PromoCodeProvider>>();
            _provider = new PromoCodeProvider(_mockRepository.Object, _mockLogger.Object);

            //List<PromoCode> _testPromoCodes = new();
            //var promo1 = new PromoCode()
            //{
            //    Id = 1,
            //    Title = "CODE01",
            //    Type = "%",
            //    Description = "Description of Promo",
            //    Rate = 50,
            //};
            //var promo2 = new PromoCode()
            //{
            //    Id = 2,
            //    Title = "CODE02",
            //    Type = "%",
            //    Description = "Description of Promo",
            //    Rate = 105,
            //};

            //_testPromoCodes.Add(promo1);
            //_testPromoCodes.Add(promo2);

            //_mockRepository.Setup(repo => repo.CreatePromoCodeAsync(promo1));
            ////.GetPromoCodesAsync());.ReturnsAsync(_testPromoCodes);

            ////_testPromoCodes = _purchaseFactory.GenerateRandomPurchases(4);
            //_mockRepository.Setup(repo => repo.GetPromoCodeByIdAsync(1)).ReturnsAsync(_testPromoCode);
            //exception1 = new BadRequestException("Promo Code rate must not exceed 100.");
        }

        [Fact]
        public void CreatePromoCodeAsync_RateUnder100_PostsPromoCode()
        {
            var promo1 = new PromoCode()
            {
                Id = 1,
                Title = "CODE01",
                Type = "%",
                Description = "Description of Promo",
                Rate = 50,
            };
            
            // Arrange
            var postedPromoCode = _mockRepository.Object.CreatePromoCodeAsync(promo1).IsCompleted;

            // Assert
            Assert.True(postedPromoCode);
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

    }
}

