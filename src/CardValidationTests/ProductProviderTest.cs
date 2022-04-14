using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.Data.SeedData;
using Catalyte.Apparel.DTOs.Purchases;
using Catalyte.Apparel.Providers.Providers;
using Catalyte.Apparel.Utilities.HttpResponseExceptions;
//using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;


namespace Catalyte.Apparel.Test.Unit
{
    public class ProductProviderTest
    {
        private readonly ProductFactory _productFactory = new();

        private readonly Mock<IProductRepository> repositoryStub;
        private readonly Mock<ILogger<ProductProvider>> loggerStub;

        private readonly ProductProvider _provider;
        //private readonly Product _testProduct;
        //private readonly List<Product> _testProducts;


        public ProductProviderTest()
        {
            List<Data.Model.Product> products = _productFactory.GenerateRandomProducts(1);
            repositoryStub = new Mock<IProductRepository>();
            loggerStub = new Mock<ILogger<ProductProvider>>();
            _provider = new ProductProvider(repositoryStub.Object, loggerStub.Object);
        }
        /*  _testProducts = _productFactory.GetActive();
          repositoryStub.Setup(repo => repo.GetActiveProductsAsync()).ReturnsAsync(_testProducts);

          var result = products.Find(delegate (Product product)
          {
              return product.Active == true;
          });
          repositoryStub.Setup(repo => repo.GetActiveProductsAsync());*/



        [Fact]
        public async Task GetActiveProducts()
        {
            List<Product> products = null;
            Product product = new()
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
                Active = true,
                Brand = "Reusch",
                ImageSrc = "https://m.media-amazon.com/images/I/81zNUlGpqJL._AC_UY550_.jpg",
                Material = "Cotton",
                Price = "78.39",
                Quantity = "46"
            };
            products.Add(product);
            repositoryStub.Setup(stub => stub.GetActiveProductsAsync()).ReturnsAsync(products);
            var expected = products;
            //IEnumerable<Product> enumerable = products;
            var actual = await _provider.GetActiveProductsAsync();
            Assert.Equal(expected, actual);
            //Assert.Equal(actual, expected);
        }
    }
}
