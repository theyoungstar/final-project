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
        private readonly List<string> productCategories;
        private readonly List<string> productTypes;


        public ProductProviderTest()
        {
            // Set up initial testing tools that most/all tests will use
            repositoryStub = new Mock<IProductRepository>();
            loggerStub = new Mock<ILogger<ProductProvider>>();
            provider = new ProductProvider(repositoryStub.Object, loggerStub.Object);
            testProduct = _factory.CreateRandomProduct(5);
            testProducts = _factory.GenerateRandomProducts(40);
            productCategories = _factory.GetAllCategories();
            productTypes = _factory.GetAllProductTypes();
            repositoryStub.Setup(repo => repo.GetProductsAsync()).ReturnsAsync(testProducts);
            repositoryStub.Setup(repo => repo.GetProductByIdAsync(5)).ReturnsAsync(testProduct);
            repositoryStub.Setup(repo => repo.GetAllUniqueCategoriesAsync()).ReturnsAsync(productCategories);
            repositoryStub.Setup(repo => repo.GetAllUniqueTypesAsync()).ReturnsAsync(productTypes);
           // repositoryStub.Setup(repo => repo.GetProductsByAllFiltersAsync(null, null, null, null, null, null, null, 0, 0)); <- do this in each test, pass tested params in each
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
            repositoryStub.Setup(repo => repo.GetProductsByAllFiltersAsync(param, null, null, null, null, null, null, 0, 0)).ReturnsAsync(testProducts);
            var result = repositoryStub.Object.GetProductsByAllFiltersAsync(param, null,null,null,null,null,null, 0,0).Result.ToList();
            var isoResult = result.Find(delegate (Product product)
            {
                return product.Brand == "Nike";
            });
            var actual = isoResult.Brand;
            var expected = "Nike";
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetProductsByAllFiltersAsync_ReturnsProductWithCorrectCategory()
        {
            var param = new List<string> { "Golf" };
            repositoryStub.Setup(repo => repo.GetProductsByAllFiltersAsync(null, param, null, null, null, null, null, 0, 0)).ReturnsAsync(testProducts);
            var result = repositoryStub.Object.GetProductsByAllFiltersAsync(null, param, null, null, null, null, null, 0, 0).Result.ToList();
            var isoResult = result.Find(delegate (Product product)
            {
                return product.Category == "Golf";
            });
            var actual = isoResult.Category;
            var expected = "Golf";
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetProductsByAllFiltersAsync_ReturnsProductWithCorrectType()
        {
            var param = new List<string> { "Hat" };
            repositoryStub.Setup(repo => repo.GetProductsByAllFiltersAsync(null, null, param, null, null, null, null, 0, 0)).ReturnsAsync(testProducts);
            var result = repositoryStub.Object.GetProductsByAllFiltersAsync(null, null, param, null, null, null, null, 0, 0).Result.ToList();
            var isoResult = result.Find(delegate (Product product)
            {
                return product.Type == "Hat";
            });
            var actual = isoResult.Type;
            var expected = "Hat";
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetProductsByAllFiltersAsync_ReturnsProductWithCorrectDemographic()
        {
            var param = new List<string> { "Men" };
            repositoryStub.Setup(repo => repo.GetProductsByAllFiltersAsync(null, null, null, param, null, null, null, 0, 0)).ReturnsAsync(testProducts);
            var result = repositoryStub.Object.GetProductsByAllFiltersAsync(null, null, null, param, null, null, null, 0, 0).Result.ToList();
            var isoResult = result.Find(delegate (Product product)
            {
                return product.Demographic == "Men";
            });
            var actual = isoResult.Demographic;
            var expected = "Men";
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetProductsByAllFiltersAsync_ReturnsProductWithCorrectPrimaryColorCode()
        {
            var param = new List<string> { "#000000" };
            repositoryStub.Setup(repo => repo.GetProductsByAllFiltersAsync(null, null, null, null, param, null, null, 0, 0)).ReturnsAsync(testProducts);
            var result = repositoryStub.Object.GetProductsByAllFiltersAsync(null, null, null, null, param, null, null, 0, 0).Result.ToList();
            var isoResult = result.Find(delegate (Product product)
            {
                return product.PrimaryColorCode == "#000000";
            });
            var actual = isoResult.PrimaryColorCode;
            var expected = "#000000";
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetProductsByAllFiltersAsync_ReturnsProductWithCorrectSecondaryColorCode()
        {
            var param = new List<string> { "#000000" };
            repositoryStub.Setup(repo => repo.GetProductsByAllFiltersAsync(null, null, null, null, null, param, null, 0, 0)).ReturnsAsync(testProducts);
            var result = repositoryStub.Object.GetProductsByAllFiltersAsync(null, null, null, null, null, param, null, 0, 0).Result.ToList();
            var isoResult = result.Find(delegate (Product product)
            {
                return product.SecondaryColorCode == "#000000";
            });
            var actual = isoResult.SecondaryColorCode;
            var expected = "#000000";
            Assert.Equal(expected, actual);
        }
    }
}
