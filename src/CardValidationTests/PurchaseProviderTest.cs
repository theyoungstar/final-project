using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.Providers.Providers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Catalyte.Apparel.Test.Unit
{
    public class PurchaseProviderTest
    {
        private readonly PurchaseProvider provider;
        private readonly Mock<IPurchaseRepository> repositoryStub;
        private readonly Mock<ILogger<PurchaseProvider>> loggerStub;
        private readonly Mock<CardValidation> cardValidationStub;


        public PurchaseProviderTest()
        {
            repositoryStub = new Mock<IPurchaseRepository>();
            loggerStub = new Mock<ILogger<PurchaseProvider>>();
            cardValidationStub = new Mock<CardValidation>();
            provider = new PurchaseProvider(repositoryStub.Object, loggerStub.Object, cardValidationStub.Object);
        }

        [Fact]
        public async Task CreatePurchaseReturnsPurchase()
        {
            //Arrange
            Purchase purchase = new();
            List<string> errorsList = new List<string>();
            cardValidationStub.Setup(stub => stub.CreditCardValidation(It.IsAny<Purchase>())).Returns(errorsList);
            repositoryStub.Setup(stub => stub.CreatePurchaseAsync(It.IsAny<Purchase>())).ReturnsAsync(purchase);

            //Act
            var actual = await provider.CreatePurchasesAsync(purchase);

            //Assert
            Assert.Equal(actual, purchase);
        }
    }
}
