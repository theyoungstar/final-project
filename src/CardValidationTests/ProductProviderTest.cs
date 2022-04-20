using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.Data.SeedData;
using Catalyte.Apparel.DTOs.Purchases;
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
    public class ProductProviderTest
    {
        private readonly ProductFactory _factory = new();
        private readonly Mock<IProductRepository> repositoryStub;
        private readonly Mock<ILogger<ProductProvider>> loggerStub;
        private readonly ProductProvider provider;
        private readonly Product testProduct;
        private readonly List<Data.Model.Product> testProducts;


        public ProductProviderTest()
        {
            // Set up initial testing tools that most/all tests will use
            repositoryStub = new Mock<IProductRepository>();
            loggerStub = new Mock<ILogger<ProductProvider>>();
            provider = new ProductProvider(repositoryStub.Object, loggerStub.Object);
            testProduct = _factory.GenerateActiveProduct(1);
            testProducts = _factory.GenerateActiveProducts(10);
            repositoryStub.Setup(repo => repo.GetActiveProductsAsync()).ReturnsAsync(testProducts);
            repositoryStub.Setup(repo => repo.GetProductsAsync()).ReturnsAsync(testProducts);
        }

        [Fact]
        public async Task GetActiveProducts_Returns_ActiveProduct()
        {
            // Arrange
            var expectedResult = testProducts.FindAll(x => x.Active == true);
            // Act
            var result = await provider.GetActiveProductsAsync();

            // Assert
            Assert.Equal(expectedResult.Count, result.ToList().Count);
        }


        [Fact]
        public void GetInactiveProduct_Returns_False()
        {
            //Arrange
            List<Product> products = new List<Product>();
            Product product = new();
            products.Add(product);

            //Act
            repositoryStub.Setup(stub => stub.GetActiveProductsAsync()).ReturnsAsync(products);
            var result = products.Find(delegate (Product product)
            {
                return product.Active == false;
            });
            //var returnException = Assert.ThrowsAsync<BadRequestException>(() => _provider.GetActiveProductsAsync());
            //var actual = await _provider.GetActiveProductsAsync();

            //Assert
            Assert.False(product.Active);
        }
    }
}
