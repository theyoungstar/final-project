using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.Data.SeedData;
using Catalyte.Apparel.DTOs.Purchases;
using Catalyte.Apparel.Providers.Providers;
using Catalyte.Apparel.Utilities.HttpResponseExceptions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Catalyte.Apparel.Test.Unit.Providers.Unit
{
    public class PurchaseProviderUnitTests
    {
        private readonly PurchaseFactory _purchaseFactory = new();

        private readonly Mock<IPurchaseRepository> _mockRepository;
        private readonly Mock<ILogger<PurchaseProvider>> _mockLogger;

        private readonly PurchaseProvider _provider;
        private readonly Purchase _testPurchase;
        private readonly List<Purchase> _testPurchases;

        public PurchaseProviderUnitTests()
        {
            _mockRepository = new Mock<IPurchaseRepository>();
            _mockLogger = new Mock<ILogger<PurchaseProvider>>();
            _provider = new PurchaseProvider(_mockRepository.Object, _mockLogger.Object);

            _testPurchase = _purchaseFactory.CreateRandomPurchase(5);
            _testPurchase.BillingEmail = "customer@home.com";
            _mockRepository.Setup(repo => repo.GetAllPurchasesByEmailAsync("customer@home.com")).ReturnsAsync(_testPurchases);

            _testPurchases = _purchaseFactory.GenerateRandomPurchases(4);
            _mockRepository.Setup(repo => repo.GetAllPurchasesByEmailAsync("customer@home.com")).ReturnsAsync(_testPurchases);
        }

        [Fact]
        public async Task GetAllPurchasesByEmailAsync_WithPurchase_ReturnsPurchases()
        {
            var expected = _testPurchases;

            var actual = await _provider.GetAllPurchasesByEmailAsync("customer@home.com");

            Assert.Equal(actual, expected);
        }

        [Fact]
        public async Task GetAllPurchasesByEmailAsync_WithNoPurchase_ReturnsEmptyArray()
        {
            var expected = Array.Empty<object>();

            var actual = await _provider.GetAllPurchasesByEmailAsync("customer1@home.com");

            Assert.Equal(actual, expected);
        }

        [Fact]
        public async Task GetAllPurchasesByEmailAsync_WithNullEmail_CheckForThrownException()
        {
            Func<Task> result = async () => { await _provider.GetAllPurchasesByEmailAsync(""); };

            await result.Should().ThrowAsync<NotFoundException>();
        }

    }
}