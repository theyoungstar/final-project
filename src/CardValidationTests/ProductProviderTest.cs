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
        private readonly List<Product> testProducts;
        private readonly List<string> productCategories;
        private readonly List<string> productTypes;
        private readonly List<Product> nikeProducts;
        private readonly List<Product> golfProducts;
        private readonly List<Product> hatProducts;
        private readonly List<Product> mensProducts;
        private readonly List<Product> primaryColorProducts;
        private readonly List<Product> secondaryColorProducts;

        public ProductProviderTest()
        {
            // Set up initial testing tools that most/all tests will use
            repositoryStub = new Mock<IProductRepository>();
            loggerStub = new Mock<ILogger<ProductProvider>>();
            provider = new ProductProvider(repositoryStub.Object, loggerStub.Object);
            testProduct = _factory.CreateRandomProduct(5);
            testProducts = _factory.GenerateRandomProducts(100);
            nikeProducts = testProducts.Where(p => p.Brand == "Nike").ToList();
            golfProducts = testProducts.Where(p => p.Category == "Golf").ToList();
            hatProducts = testProducts.Where(p => p.Type == "Hat").ToList();
            mensProducts = testProducts.Where(p => p.Demographic == "Men").ToList();
            primaryColorProducts = testProducts.Where(p => p.PrimaryColorCode == "#000000").ToList();
            secondaryColorProducts = testProducts.Where(p => p.SecondaryColorCode == "#000000").ToList();
            productCategories = _factory.GetAllCategories();
            productTypes = _factory.GetAllProductTypes();
            repositoryStub.Setup(repo => repo.GetProductsAsync()).ReturnsAsync(testProducts);
            repositoryStub.Setup(repo => repo.GetProductByIdAsync(5)).ReturnsAsync(testProduct);
            repositoryStub.Setup(repo => repo.GetAllUniqueCategoriesAsync()).ReturnsAsync(productCategories);
            repositoryStub.Setup(repo => repo.GetAllUniqueTypesAsync()).ReturnsAsync(productTypes);
          
        }
        [Fact]
        public void GetProductsAsync_ReturnsAllProducts()
        {
            var totalCount = testProducts;
            var actualCount = repositoryStub.Object.GetProductsAsync().Result;
            Assert.Equal(totalCount, actualCount);
            
        }
        [Fact]
        public void GetProductById_ReturnsCorrectProduct()
        {
            var target = testProduct;
            var queried = repositoryStub.Object.GetProductByIdAsync(5).Result;
            Assert.Equal(target, queried);
        }
        [Fact]
        public void GetAllUniqueCategories_ReturnsAllCategories()
        {
            var expected = productCategories;
            var actual = repositoryStub.Object.GetAllUniqueCategoriesAsync().Result.ToList();
            Assert.Equal(expected, actual);

        }
        [Fact]
        public void GetAllUniqueTypes_ReturnsAllTypes()
        {
            var expected = productTypes;
            var actual = repositoryStub.Object.GetAllUniqueTypesAsync().Result.ToList();
            Assert.Equal(expected,actual);  
        }
        [Fact]
        public void GetProductsByAllFiltersAsync_ReturnsProductWithCorrectBrand()
        {
            var param = new List<string> { "Nike" };
            repositoryStub.Setup(repo => repo.GetProductsByAllFiltersAsync(param, null, null, null, null, null, null, 0, 0)).ReturnsAsync(nikeProducts);
            var result = repositoryStub.Object.GetProductsByAllFiltersAsync(param, null,null,null,null,null,null, 0,0).Result.ToList();
            var isoResult = result.First();
            var actual = isoResult.Brand;
            var expected = "Nike";
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetProductsByAllFiltersAsync_ReturnsProductWithCorrectCategory()
        {
            var param = new List<string> { "Golf" };
            repositoryStub.Setup(repo => repo.GetProductsByAllFiltersAsync(null, param, null, null, null, null, null, 0, 0)).ReturnsAsync(golfProducts);
            var result = repositoryStub.Object.GetProductsByAllFiltersAsync(null, param, null, null, null, null, null, 0, 0).Result.ToList();
            var isoResult = result.First();
            var actual = isoResult.Category;
            var expected = "Golf";
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetProductsByAllFiltersAsync_ReturnsProductWithCorrectType()
        {
            var param = new List<string> { "Hat" };
            repositoryStub.Setup(repo => repo.GetProductsByAllFiltersAsync(null, null, param, null, null, null, null, 0, 0)).ReturnsAsync(hatProducts);
            var result = repositoryStub.Object.GetProductsByAllFiltersAsync(null, null, param, null, null, null, null, 0, 0).Result.ToList();
            var isoResult = result.First();
            var actual = isoResult.Type;
            var expected = "Hat";
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetProductsByAllFiltersAsync_ReturnsProductWithCorrectDemographic()
        {
            var param = new List<string> { "Men" };
            repositoryStub.Setup(repo => repo.GetProductsByAllFiltersAsync(null, null, null, param, null, null, null, 0, 0)).ReturnsAsync(mensProducts);
            var result = repositoryStub.Object.GetProductsByAllFiltersAsync(null, null, null, param, null, null, null, 0, 0).Result.ToList();
            var isoResult = result.Last();
            var actual = isoResult.Demographic;
            var expected = "Men";
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetProductsByAllFiltersAsync_ReturnsProductWithCorrectPrimaryColorCode()
        {
            var param = new List<string> { "#000000" };
            repositoryStub.Setup(repo => repo.GetProductsByAllFiltersAsync(null, null, null, null, param, null, null, 0, 0)).ReturnsAsync(primaryColorProducts);
            var result = repositoryStub.Object.GetProductsByAllFiltersAsync(null, null, null, null, param, null, null, 0, 0).Result.ToList();
            var isoResult = result.First();
            var actual = isoResult.PrimaryColorCode;
            var expected = "#000000";
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetProductsByAllFiltersAsync_ReturnsProductWithCorrectSecondaryColorCode()
        {
            var param = new List<string> { "#000000" };
            repositoryStub.Setup(repo => repo.GetProductsByAllFiltersAsync(null, null, null, null, null, param, null, 0, 0)).ReturnsAsync(secondaryColorProducts);
            var result = repositoryStub.Object.GetProductsByAllFiltersAsync(null, null, null, null, null, param, null, 0, 0).Result.ToList();
            var isoResult = result.First();
            var actual = isoResult.SecondaryColorCode;
            var expected = "#000000";
            Assert.Equal(expected, actual);
        }
    }
}
