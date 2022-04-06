using Catalyte.Apparel.Data.Interfaces;
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
       
        private readonly List<Data.Model.Purchase> _testPurchases;


        public PurchaseProviderUnitTests()
        {
            _mockRepository = new Mock<IPurchaseRepository>();
            _mockLogger = new Mock<ILogger<PurchaseProvider>>();
            _provider = new PurchaseProvider(_mockRepository.Object, _mockLogger.Object);

            _testPurchases = _purchaseFactory.GenerateRandomPurchases(3);
            _mockRepository.Setup(repo => repo.GetAllPurchasesByEmailAsync("customer@home.com")).ReturnsAsync(_testPurchases);
        }

        [Fact]
        public async Task GetAllPurchasesByEmailAsync_WithPurchase_ReturnsPurchases()
        {
            // Arrange
            var expected = _testPurchases.ToArray();

            // Act
            var actual = await _provider.GetAllPurchasesByEmailAsync("customer@home.com");

            //Assert
            Assert.Equal(actual, expected);
        }
        [Fact]
        public async Task GetAllPurchasesByEmailAsync_WithNoPurchase_ReturnsEmptyArray()
        {
            // Arrange
            var expected = Array.Empty<object>();

            // Act
            var actual = await _provider.GetAllPurchasesByEmailAsync("customer1@home.com");

            // Assert
            Assert.Equal(actual, expected);
        }
    }
}