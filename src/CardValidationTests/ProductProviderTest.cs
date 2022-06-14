/*using Catalyte.Apparel.Data.Interfaces;
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
        private readonly List<Product> testProducts;
        private readonly List<string> productCategories;
        private readonly List<string> productTypes;
        private readonly List<Product> mensLeatherGolfProducts;
        private readonly List<Product> pricedProducts;
        private readonly ServiceUnavailableException exception;
        private readonly Product invalidProduct;
        private readonly NotFoundException exception1;
        private readonly Product testProduct1;
        private readonly List<Product> testProducts1;

        public ProductProviderTest()
        {
            // Set up initial testing tools that most/all tests will use
            repositoryStub = new Mock<IProductRepository>();
            loggerStub = new Mock<ILogger<ProductProvider>>();
            provider = new ProductProvider(repositoryStub.Object, loggerStub.Object);
            testProduct = _factory.CreateRandomProduct(5);
            testProducts = _factory.GenerateRandomProducts(100);
            pricedProducts = testProducts.Where(p => p.Price <= 10 && p.Price >= 5).ToList();
            mensLeatherGolfProducts = testProducts.Where(p => p.Material == "Leather" && p.Type == "Golf" && p.Demographic == "Men").ToList();
            productCategories = _factory.GetAllCategories();
            productTypes = _factory.GetAllProductTypes();
            exception = new ServiceUnavailableException("There was a problem connecting to the database.");
            invalidProduct = null;
            repositoryStub.Setup(repo => repo.GetProductsAsync()).ReturnsAsync(testProducts);
            repositoryStub.Setup(repo => repo.GetProductByIdAsync(5)).ReturnsAsync(testProduct);
            repositoryStub.Setup(repo => repo.GetAllUniqueCategoriesAsync()).ReturnsAsync(productCategories);
            repositoryStub.Setup(repo => repo.GetAllUniqueTypesAsync()).ReturnsAsync(productTypes);
            testProduct1 = _factory.GenerateActiveProduct(11);
            testProducts1 = _factory.GenerateActiveProducts(10);
            exception1 = new NotFoundException("The product you requested is inactive.");
        }
        [Fact]
        public void GetProductsAsync_ReturnsAllProducts()
        {
            var totalCount = testProducts;
            var actualCount = repositoryStub.Object.GetProductsAsync().Result;
            Assert.Equal(totalCount, actualCount);

        }
        [Fact]
        public void GetProductsAsync_ThrowsServiceUnavailableExceptionIfDatabaseIsInactive()
        {
            repositoryStub.Setup(repo => repo.GetProductsAsync()).ThrowsAsync(exception);

            Assert.ThrowsAsync<ServiceUnavailableException>(() => provider.GetProductsAsync());
        }
        [Fact]
        public void GetProductById_ReturnsCorrectProduct()
        {
            var target = testProduct;
            var queried = repositoryStub.Object.GetProductByIdAsync(5).Result;
            Assert.Equal(target, queried);
        }
        [Fact]
        public void GetProductById_ThrowsServiceUnavailableExceptionIfDatabaseIsInactive()
        {
            repositoryStub.Setup(repo => repo.GetProductByIdAsync(3)).ThrowsAsync(exception);

            Assert.ThrowsAsync<ServiceUnavailableException>(() => provider.GetProductByIdAsync(3));
        }
        [Fact]
        public void GetProductById_ThrowsNotFoundExceptionIfRequestedProductDoesNotExist()
        {
            var productId = 6;

            repositoryStub.Setup(repo => repo.GetProductByIdAsync(productId)).ReturnsAsync(invalidProduct);

            Assert.Null(repositoryStub.Object.GetProductByIdAsync(productId).Result);
        }


        [Fact]
        public void GetAllUniqueCategories_ReturnsAllCategories()
        {
            var expected = productCategories;
            var actual = repositoryStub.Object.GetAllUniqueCategoriesAsync().Result.ToList();
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetAllUniqueCategories_ThrowsServiceUnavailableExceptionIfDatabaseIsInactive()
        {
            repositoryStub.Setup(repo => repo.GetAllUniqueCategoriesAsync()).ThrowsAsync(exception);

            Assert.ThrowsAsync<ServiceUnavailableException>(() => provider.GetAllUniqueCategoriesAsync());
        }
        [Fact]
        public void GetAllUniqueTypes_ReturnsAllTypes()
        {
            var expected = productTypes;
            var actual = repositoryStub.Object.GetAllUniqueTypesAsync().Result.ToList();
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetAllUniqueTypes_ThrowsServiceUnavailableExceptionIfDatabaseIsInactive()
        {
            repositoryStub.Setup(repo => repo.GetAllUniqueTypesAsync()).ThrowsAsync(exception);

            Assert.ThrowsAsync<ServiceUnavailableException>(() => provider.GetAllUniqueTypesAsync());
        }
        [Fact]
        public void GetProductsByAllFilters_ReturnsProductsWithMultipleCorrectAttributes()
        {
            var category = new List<string> { "Golf" };
            var material = new List<string> { "Leather" };
            var demographic = new List<string> { "Men" };
            repositoryStub.Setup(repo => repo.GetProductsByAllFiltersAsync(null, category, null, demographic, null, null, material, 0, 0))
                .ReturnsAsync(mensLeatherGolfProducts);
            var result = repositoryStub.Object.GetProductsByAllFiltersAsync(null, category, null, demographic, null, null, material, 0, 0).Result.ToList();
            var filteredResults = result.All(p => p.Material == "Leather" && p.Category == "Golf" && p.Demographic == "Men");
            Assert.True(filteredResults);
        }
        [Fact]
        public void GetProductsByAllFilters_ReturnsProperlyPricedProducts()
        {
            var min = 5;
            var max = 10;
            repositoryStub.Setup(repo => repo.GetProductsByAllFiltersAsync(null, null, null, null, null, null, null, min, max)).ReturnsAsync(pricedProducts);
            var result = repositoryStub.Object.GetProductsByAllFiltersAsync(null, null, null, null, null, null, null, min, max).Result.ToList();
            var filteredResults = result.All(p => p.Price <= 10 && p.Price >= 5);
            Assert.True(filteredResults);
        }
        [Fact]
        public void GetProductsByAllFilters_ThrowsServiceUnavailableExceptionIfDatabaseIsInactive()
        {
            repositoryStub.Setup(repo => repo.GetProductsByAllFiltersAsync(null, null, null, null, null, null, null, 0, 0)).ThrowsAsync(exception);

            Assert.ThrowsAsync<ServiceUnavailableException>(() => provider.GetProductsByAllFiltersAsync(null, null, null, null, null, null, null, 0, 0));
        }
        ////////Active Products testing
        [Fact]
        public async Task GetActiveProducts_Returns_ActiveProduct()
        {
            // Arrange
            repositoryStub.Setup(repo => repo.GetActiveProductsAsync()).ReturnsAsync(testProducts1);
            var expectedResult = testProducts1.FindAll(x => x.Active == true);
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
                Price = 78.39,
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

*/