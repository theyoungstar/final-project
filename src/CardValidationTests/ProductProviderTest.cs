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
            repositoryStub.Setup(repo => repo.GetProductsAsync()).ReturnsAsync(testProducts);
            repositoryStub.Setup(repo => repo.GetAllUniqueCategoriesAsync()).ReturnsAsync(productCategories);
            repositoryStub.Setup(repo => repo.GetAllUniqueTypesAsync()).ReturnsAsync(productTypes);
           // repositoryStub.Setup(repo => repo.GetProductsByAllFiltersAsync(null, null, null, null, null, null, null, 0, 0)); <- do this in each test, pass tested params in each
        }

        
    }
}
