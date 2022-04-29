using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.Data.SeedData;
using Catalyte.Apparel.DTOs.PromoCodes;
using Catalyte.Apparel.Providers.Providers;
using Catalyte.Apparel.Utilities.HttpResponseExceptions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Catalyte.Apparel.Test.Unit
{
    public class PromoCodeProviderTest
    {
        private readonly PurchaseFactory _purchaseFactory = new();

        private readonly Mock<IPromoCodeRepository> _mockRepository;
        private readonly Mock<ILogger<PromoCodeProvider>> _mockLogger;
        private readonly BadRequestException exception1;

        private readonly PromoCodeProvider _provider;
        private readonly PromoCode _testPromoCode;
        private readonly List<PromoCode> _testPromoCodes;

        public PromoCodeProviderTest()
        {

            _mockRepository = new Mock<IPromoCodeRepository>();
            _mockLogger = new Mock<ILogger<PromoCodeProvider>>();
            _provider = new PromoCodeProvider(_mockRepository.Object, _mockLogger.Object);

            _testPromoCode = new PromoCode()
            {
                Id = 1,
                Title = "CODE00",
                Type = "flat or %",
                Description = "Description of Promo",
                Rate = 105,
            };
            _mockRepository.Setup(repo => repo.GetPromoCodeByIdAsync(1)).ReturnsAsync(_testPromoCode);

            //_testPromoCodes = _purchaseFactory.GenerateRandomPurchases(4);
            //_mockRepository.Setup(repo => repo.GetPromoCodesAsync()).ReturnsAsync(_testPromoCodes);
            exception1 = new BadRequestException("Promo Code was not returned due to rate exceeding 100.");
        }
        [Fact]
        public async Task GetPromoCodesAsync_RateOver100_ReturnsException()
        {
            var expected = exception1;

            var actual = await _provider.GetPromoCodeByIdAsync(1);

            Assert.Equal(actual, expected);


            //Func<Task> result = async () => { await _provider.GetPromoCodeByIdAsync(1); };

            //await result.Should().ThrowsAsync<BadRequestException>();
            //_mockRepository.Setup(repo => repo.GetPromoCodesAsync()).ThrowsAsync(exception1);

            //Assert.ThrowsAsync<BadRequestException>(() => _provider.GetPromoCodesAsync());
        }

        [Fact]
        public async Task GetPromoCodesAsync_RateUnder100_ReturnsException()
        {
            var expected = exception1;

            var actual = await _provider.GetPromoCodeByIdAsync(1);

            Assert.Equal(actual, expected);


            //Func<Task> result = async () => { await _provider.GetPromoCodeByIdAsync(1); };

            //await result.Should().ThrowsAsync<BadRequestException>();
            //_mockRepository.Setup(repo => repo.GetPromoCodesAsync()).ThrowsAsync(exception1);

            //Assert.ThrowsAsync<BadRequestException>(() => _provider.GetPromoCodesAsync());
        }
    }
}
