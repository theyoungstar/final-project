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
        private readonly List<Data.Model.Product> _products;
        private readonly int _productsToGenerate;
        List<string> _demographics = new();
        List<string> _categories = new();
        List<string> _types = new();
        List<string> _colorCodes = new();
        public ProductTest()
        {
            _productFactory = new();
            _productsToGenerate = 1000;
            _products = _productFactory.GenerateRandomProducts(_productsToGenerate);
            _demographics = _productFactory.GetAllDemographics();
            _categories = _productFactory.GetAllCategories();
            _types = _productFactory.GetAllProductTypes();
            _colorCodes = _productFactory.GetAllColors();
        }

        [Fact]
        public void TestProducts_AllProductsAreDistinct()
        {
            int expected = _productsToGenerate;
            List<Data.Model.Product> distinctProducts = new();
            foreach (var product in _products)
            {
                if (!distinctProducts.Contains(product))
                {
                    distinctProducts.Add(product);
                }
            }
            Assert.Equal(expected, distinctProducts.Count);
        }

        [Fact]
        public void TestProductDemographic_AllProductsContainDemographic()
        {
            int expected = _productsToGenerate;
            int demographicCount = 0;
            foreach (var product in _products)
            {
                if (_demographics.Contains(product.Demographic))
                {
                    demographicCount++;
                }
            }
            Assert.Equal(expected, demographicCount);
        }

        [Fact]
        public void TestProductDemographic_NoProductContainsNullDemographic()
        {
            int expected = 0;
            int nullCount = 0;
            foreach (var product in _products)
            {
                if (product.Demographic == null || product.Demographic.Length == 0)
                {
                    nullCount++;
                }
            }
            Assert.Equal(expected, nullCount);
        }

        [Fact]
        public void TestProductDemographic_AllDemographicsAreRepresented()
        {
            int expected = _demographics.Count;
            Dictionary<string, int> demographicCount = new Dictionary<string, int>();
            foreach (var product in _products)
            {
                if (!(demographicCount.TryGetValue(product.Demographic, out int count)))
                {
                    demographicCount.Add(product.Demographic, 0);
                }
                demographicCount[product.Demographic]++;
            }
            Assert.Equal(expected, demographicCount.Count);
        }

        [Fact]
        public void TestProductCategory_AllProductsContainCategory()
        {
            int expected = _products.Count;
            int categoryCount = 0;
            foreach (var product in _products)
            {
                if (_categories.Contains(product.Category))
                {
                    categoryCount++;
                }
            }
            Assert.Equal(expected, categoryCount);
        }

        [Fact]
        public void TestProductCategory_NoProductContainsNullCategory()
        {
            int expected = 0;
            int nullCount = 0;
            foreach (var product in _products)
            {
                if (product.Category == null || product.Category.Length == 0)
                {
                    nullCount++;
                }

            }
            Assert.Equal(expected, nullCount);
        }

        [Fact]
        public void TestProductCategory_AllCategoriesAreRepresented()
        {
            int expected = _categories.Count;
            Dictionary<string, int> categoryCount = new Dictionary<string, int>();
            foreach (var product in _products)
            {
                if (!(categoryCount.TryGetValue(product.Category, out int count)))
                {
                    categoryCount.Add(product.Category, 0);
                }
                categoryCount[product.Category]++;
            }
            Assert.Equal(expected, categoryCount.Count);
        }

        [Fact]
        public void TestProductType_AllProductsContainType()
        {
            int expected = _productsToGenerate;
            int typeCount = 0;
            foreach (var product in _products)
            {
                if (_types.Contains(product.Type))
                {
                    typeCount++;
                }
            }
            Assert.Equal(expected, typeCount);
        }

        [Fact]
        public void TestProductType_NoProductsContainNullType()
        {
            int expected = 0;
            int nullCount = 0;
            foreach (var product in _products)
            {
                if (product.Type == null || product.Type.Length == 0)
                {
                    nullCount++;
                }
            }
            Assert.Equal(expected, nullCount);
        }

        [Fact]
        public void TestProductType_AllTypesAreRepresented()
        {
            int expected = _types.Count;
            Dictionary<string, int> typeCount = new Dictionary<string, int>();
            foreach (var product in _products)
            {
                if (!(typeCount.TryGetValue(product.Type, out int count)))
                {
                    typeCount.Add(product.Type, 0);
                }
                typeCount[product.Type]++;
            }
            Assert.Equal(expected, typeCount.Count);
        }
        [Fact]
        public void TestProductPrimaryColor_AllProductsContainPrimaryColor()
        {
            int expected = _productsToGenerate;
            int colorCodeCount = 0;
            foreach (var product in _products)
            {
                if (_colorCodes.Contains(product.PrimaryColorCode))
                {
                    colorCodeCount++;
                }
            }
            Assert.Equal(expected, colorCodeCount);
        }

        [Fact]
        public void TestProductPrimaryColor_NoProductsContainNullPrimaryColor()
        {
            int expected = 0;
            int nullCount = 0;
            foreach (var product in _products)
            {
                if (product.PrimaryColorCode == null || product.PrimaryColorCode.Length == 0)
                {
                    nullCount++;
                }
            }
            Assert.Equal(expected, nullCount);
        }

        [Fact]
        public void TestProductPrimaryColor_AllCollorsAreRepresented()
        {
            int expected = _colorCodes.Count;
            Dictionary<string, int> colorCodeCount = new Dictionary<string, int>();
            foreach (var product in _products)
            {
                if (!(colorCodeCount.TryGetValue(product.PrimaryColorCode, out int count)))
                {
                    colorCodeCount.Add(product.PrimaryColorCode, 0);
                }
                colorCodeCount[product.PrimaryColorCode]++;
            }
            Assert.Equal(expected, colorCodeCount.Count);
        }

        [Fact]
        public void TestProductSecondaryColor_AllProductsContainPrimaryColor()
        {
            int expected = _productsToGenerate;
            int colorCodeCount = 0;
            foreach (var product in _products)
            {
                if (_colorCodes.Contains(product.SecondaryColorCode))
                {
                    colorCodeCount++;
                }
            }
            Assert.Equal(expected, colorCodeCount);
        }

        [Fact]
        public void TestProductSecondaryColor_NoProductsContainNullPrimaryColor()
        {
            int expected = 0;
            int nullCount = 0;
            foreach (var product in _products)
            {
                if (product.SecondaryColorCode == null || product.SecondaryColorCode.Length == 0)
                {
                    nullCount++;
                }
            }
            Assert.Equal(expected, nullCount);
        }

        [Fact]
        public void TestProductSecondaryColor_AllCollorsAreRepresented()
        {
            int expected = _colorCodes.Count;
            Dictionary<string, int> colorCodeCount = new Dictionary<string, int>();
            foreach (var product in _products)
            {
                if (!(colorCodeCount.TryGetValue(product.SecondaryColorCode, out int count)))
                {
                    colorCodeCount.Add(product.SecondaryColorCode, 0);
                }
                colorCodeCount[product.SecondaryColorCode]++;
            }
            Assert.Equal(expected, colorCodeCount.Count);
        }

        [Fact]
        public void TestProductColor_NoProductContainsPrimaryColorEqualToSecondaryColor()
        {
            int expected = 0;
            int sameCount = 0;
            foreach (var product in _products)
            {
                if (product.PrimaryColorCode == product.SecondaryColorCode)
                {
                    sameCount++;
                }
            }
            Assert.Equal(expected, sameCount);
        }
        [Fact]
        public void TestProductActive_AllProductsContainActive()
        {
            int expected = _productsToGenerate;
            int activeCount = 0;
            foreach (var product in _products)
            {
                if (product.Active == true || product.Active == false)
                {
                    activeCount++;
                }
            }
            Assert.Equal(expected, activeCount);
        }

        [Fact]
        public void TestProductActive_NotAllProductsContainTrue()
        {
            int expected = _productsToGenerate;
            int trueCount = 0;
            foreach (var product in _products)
            {
                if (product.Active == true)
                {
                    trueCount++;
                }
            }
            Assert.NotEqual(expected, trueCount);
        }

        [Fact]
        public void TestProductActive_NotAllProductsContainFalse()
        {
            int expected = _productsToGenerate;
            int falseCount = 0;
            foreach (var product in _products)
            {
                if (product.Active == false)
                {
                    falseCount++;
                }
            }
            Assert.NotEqual(expected, falseCount);
        }
    }
}
