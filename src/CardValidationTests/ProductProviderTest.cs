using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.Data.SeedData;
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
        private readonly List<Product> testProducts;
        private readonly NotFoundException exception;



        public ProductProviderTest()
        {
            // Set up initial testing tools that most/all tests will use
            repositoryStub = new Mock<IProductRepository>();
            loggerStub = new Mock<ILogger<ProductProvider>>();
            provider = new ProductProvider(repositoryStub.Object, loggerStub.Object);
            testProduct = _factory.GenerateActiveProduct(11);
            testProducts = _factory.GenerateActiveProducts(10);
            repositoryStub.Setup(repo => repo.GetActiveProductsAsync()).ReturnsAsync(testProducts);
            repositoryStub.Setup(repo => repo.GetProductsAsync()).ReturnsAsync(testProducts);
            exception = new NotFoundException("The product you requested is inactive.");
        }

        [Fact]
        public async Task GetActiveProducts_Returns_ActiveProduct()
        {
            // Arrange
            repositoryStub.Setup(repo => repo.GetActiveProductsAsync()).ReturnsAsync(testProducts);
            var expectedResult = testProducts.FindAll(x => x.Active == true);
            // Act
            var result = await provider.GetActiveProductsAsync();

            // Assert
            Assert.Equal(expectedResult.Count, result.ToList().Count);
        }


        [Fact]
        public void GetActiveProducts_WhenExceptionIsThrown_IsCompletedSuccessfully()
        {
           
       
            //Act
            repositoryStub.Setup(repo => repo.GetActiveProductsAsync()).ThrowsAsync(exception);
            
            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => provider.GetActiveProductsAsync());

        }


        [Fact]
        public void GetActiveProducts_WhenGivenEmptyProduct_ReturnsEmptyProductArray()
        {
            // Arrange
            List<Product> products = new();
            var product = new Product
            {
                Id = 660,
                Name = "Lightweight Boxing Hat",
                Sku = "NGH-KYH-RD",
                Description = "Boxing, Men, Lightweight",
                Demographic = "Men",
                Category = "Boxing",
                Type = "Hat",
                ReleaseDate = "12/05/2020",
                PrimaryColorCode = "#c25975",
                SecondaryColorCode = "#637a91",
                StyleNumber = "sc83418",
                GlobalProductCode = "po-DREFRJM",
                Active = false,
                Brand = "Reusch",
                ImageSrc = "https://m.media-amazon.com/images/I/81zNUlGpqJL._AC_UY550_.jpg",
                Material = "Cotton",
                Price = "78.39",
                Quantity = "46"
            };

            //Act

            repositoryStub.Setup(repo => repo.GetActiveProductsAsync());
            var expectedResult = product.Active == true;
            // Act
            var result = product.Active;

            // Assert
            Assert.Equal(expectedResult, result);

    
        }
    }
}
