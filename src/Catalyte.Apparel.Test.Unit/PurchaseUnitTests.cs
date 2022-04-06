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
        private readonly PurchaseFactory _factory = new();

        private readonly Mock<IPurchaseRepository> repositoryStub;
        private readonly Mock<ILogger<PurchaseProvider>> loggerStub;

        private readonly PurchaseProvider provider;

        private readonly Data.Model.Purchase testProduct;
        private readonly List<Data.Model.Purchase> testPurchases;


        public PurchaseProviderUnitTests()
        {
            repositoryStub = new Mock<IPurchaseRepository>();
            loggerStub = new Mock<ILogger<PurchaseProvider>>();
            provider = new PurchaseProvider(repositoryStub.Object, loggerStub.Object);

            testPurchases = _factory.GenerateRandomPurchases(3);
            repositoryStub.Setup(repo => repo.GetAllPurchasesByEmailAsync("customer@home.com")).ReturnsAsync(testPurchases);
        }

        [Fact]
        public async Task GetAllPurchasesByEmailAsync_WithPurchase_ReturnsPurchases()
        {
            // Arrange
            var expected = testPurchases.ToArray();

            // Act
            var actual = await provider.GetAllPurchasesByEmailAsync("customer@home.com");

            //Assert
            Assert.Equal(actual, expected);
        }
        [Fact]
        public async Task GetAllPurchasesByEmailAsync_WithNoPurchase_ReturnsEmptyArray()
        {
            // Arrange
            var expected = Array.Empty<object>();

            // Act
            var actual = await provider.GetAllPurchasesByEmailAsync("customer1@home.com");

            // Assert
            Assert.Equal(actual, expected);
        }

    }
}