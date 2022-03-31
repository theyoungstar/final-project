using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.Data.SeedData;

namespace Catalyte.Apparel.Test.Unit.Product
{
    public class ProductTest
    {
        private readonly ProductFactory _productFactory;
        List<string> _demographics = new List<string>();

        public ProductTest()
        {
            _productFactory = new();
            _demographics = _productFactory.GetAllDemographics();
        }

        [Fact]
        public void TestProductDemograpic_checkOneProduct()
        {

            List<Data.Model.Product> products = _productFactory.GenerateRandomProducts(1);
            string demographic = products[0].Demographic;
            Assert.Contains(demographic, _demographics);

        }

        [Fact]
        public void TestProductDemograpic_checkManyProducts()
        {

            List<Data.Model.Product> products = _productFactory.GenerateRandomProducts(100);
            foreach (var product in products)
            {
                string demographic = product.Demographic;
                Assert.Contains(demographic, _demographics);
            }

        }

        [Fact]
        public void TestProductDemograpic_checkForNullDemographics()
        {
            List<Data.Model.Product> products = _productFactory.GenerateRandomProducts(100);
            foreach (var product in products)
            {
                string demographic = products[0].Demographic;
                Assert.NotNull(demographic);
            }

        }

        [Fact]
        public void TestProductDemograpic_checkForCorrectNumberOfDemographics()
        {
            List<Data.Model.Product> products = _productFactory.GenerateRandomProducts(100);
            Dictionary<string, int> demographicCount = new Dictionary<string, int>();
            foreach (var product in products)
            {
                if (!(demographicCount.TryGetValue(product.Demographic, out int count)))
                {
                    demographicCount.Add(product.Demographic, 0);
                }
                demographicCount[product.Demographic]++;
            }
            Assert.Equal(_demographics.Count, demographicCount.Count);
        }
    }
}
