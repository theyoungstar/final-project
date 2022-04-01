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
        List<string> _categories = new List<string>();
        List<string> _types = new List<string>();

        public ProductTest()
        {
            _productFactory = new();
            _demographics = _productFactory.GetAllDemographics();
            _categories = _productFactory.GetAllCategories();
            _types = _productFactory.GetAllProductTypes();
        }
      
        [Fact]
        public void TestProductDemographic_checkManyProducts()
        {

            List<Data.Model.Product> products = _productFactory.GenerateRandomProducts(100);
            foreach (var product in products)
            {
                string demographic = product.Demographic;
                Assert.Contains(demographic, _demographics);
            }

        }

        [Fact]
        public void TestProductDemographic_checkForNullDemographics()
        {
            List<Data.Model.Product> products = _productFactory.GenerateRandomProducts(100);
            foreach (var product in products)
            {
                string demographic = product.Demographic;
                Assert.NotNull(demographic);
            }

        }

        [Fact]
        public void TestProductDemographic_checkForCorrectNumberOfDemographics()
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

        [Fact]
        public void TestProductCategory_checkManyProducts()
        {

            List<Data.Model.Product> products = _productFactory.GenerateRandomProducts(100);
            foreach (var product in products)
            {
                string category = product.Category;
                Assert.Contains(category, _categories);
            }

        }

        [Fact]
        public void TestProductCategory_checkForNull()
        {
            List<Data.Model.Product> products = _productFactory.GenerateRandomProducts(100);
            foreach (var product in products)
            {
                string category = product.Category;
                Assert.NotNull(category);
            }

        }

        [Fact]
        public void TestProductCategory_checkForCorrectNumberOfCategories()
        {
            List<Data.Model.Product> products = _productFactory.GenerateRandomProducts(100);
            Dictionary<string, int> categoryCount = new Dictionary<string, int>();
            foreach (var product in products)
            {
                if (!(categoryCount.TryGetValue(product.Category, out int count)))
                {
                    categoryCount.Add(product.Category, 0);
                }
                categoryCount[product.Category]++;
            }
            Assert.Equal(_categories.Count, categoryCount.Count);
        }

        [Fact]
        public void TestProductType_checkManyProducts()
        {

            List<Data.Model.Product> products = _productFactory.GenerateRandomProducts(100);
            foreach (var product in products)
            {
                string type = product.Type;
                Assert.Contains(type, _types);
            }

        }

        [Fact]
        public void TestProductType_checkForNull()
        {
            List<Data.Model.Product> products = _productFactory.GenerateRandomProducts(100);
            foreach (var product in products)
            {
                string type = product.Type;
                Assert.NotNull(type);
            }

        }

        [Fact]
        public void TestProductType_checkForCorrectNumberOfTypes()
        {
            List<Data.Model.Product> products = _productFactory.GenerateRandomProducts(100);
            Dictionary<string, int> typeCount = new Dictionary<string, int>();
            foreach (var product in products)
            {
                if (!(typeCount.TryGetValue(product.Type, out int count)))
                {
                    typeCount.Add(product.Type, 0);
                }
                typeCount[product.Type]++;
            }
            Assert.Equal(_types.Count, typeCount.Count);
        }
    }
}
