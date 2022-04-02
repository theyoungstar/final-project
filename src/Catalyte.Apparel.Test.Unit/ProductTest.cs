using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text.RegularExpressions;
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
        List<string> _brands = new();
        List<string> _materials = new();
        List<string> _adjectives = new();

        public ProductTest()
        {
            _productFactory = new();
            _productsToGenerate = 1000;
            _products = _productFactory.GenerateRandomProducts(_productsToGenerate);
            _demographics = _productFactory.GetAllDemographics();
            _categories = _productFactory.GetAllCategories();
            _types = _productFactory.GetAllProductTypes();
            _colorCodes = _productFactory.GetAllColors();
            _brands = _productFactory.GetAllBrands();
            _materials = _productFactory.GetAllMaterials();
            _adjectives = _productFactory.GetAllAdjectives();
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

        [Fact]
        public void TestProductBrand_AllProductsContainBrand()
        {
            int expected = _productsToGenerate;
            int brandCount = 0;
            foreach (var product in _products)
            {
                if (_brands.Contains(product.Brand))
                {
                    brandCount++;
                }
            }
            Assert.Equal(expected, brandCount);
        }

        [Fact]
        public void TestProductBrand_NoProductContainsNullBrand()
        {
            int expected = 0;
            int nullCount = 0;
            foreach (var product in _products)
            {
                if (product.Brand == null || product.Brand.Length == 0)
                {
                    nullCount++;
                }
            }
            Assert.Equal(expected, nullCount);
        }

        [Fact]
        public void TestProductBrand_AllBrandsAreRepresented()
        {
            int expected = _brands.Count;
            Dictionary<string, int> brandCount = new Dictionary<string, int>();
            foreach (var product in _products)
            {
                if (!(brandCount.TryGetValue(product.Brand, out int count)))
                {
                    brandCount.Add(product.Brand, 0);
                }
                brandCount[product.Brand]++;
            }
            Assert.Equal(expected, brandCount.Count);
        }

        [Fact]
        public void TestProductMaterial_AllProductsContainMaterial()
        {
            int expected = _productsToGenerate;
            int materialCount = 0;
            foreach (var product in _products)
            {
                if (_materials.Contains(product.Material))
                {
                    materialCount++;
                }
            }
            Assert.Equal(expected, materialCount);
        }

        [Fact]
        public void TestProductMaterial_NoProductContainsNullMaterials()
        {
            int expected = 0;
            int nullCount = 0;
            foreach (var product in _products)
            {
                if (product.Material == null || product.Material.Length == 0)
                {
                    nullCount++;
                }
            }
            Assert.Equal(expected, nullCount);
        }

        [Fact]
        public void TestProductMaterial_AllMaterialsAreRepresented()
        {
            int expected = _materials.Count;
            Dictionary<string, int> materialCount = new Dictionary<string, int>();
            foreach (var product in _products)
            {
                if (!(materialCount.TryGetValue(product.Material, out int count)))
                {
                    materialCount.Add(product.Material, 0);
                }
                materialCount[product.Material]++;
            }
            Assert.Equal(expected, materialCount.Count);
        }

        [Fact]
        public void TestProductName_AllProductsHaveName()
        {
            int expected = _productsToGenerate;
            int nameCount = 0;
            foreach (var product in _products)
            {
                if (product.Name != null && product.Name.Length > 0)
                {
                    nameCount++;
                }
            }
            Assert.Equal(expected, nameCount);
        }

        [Fact]
        public void TestProductName_AllProductsHaveCorrectlyFormattedName()
        {
            int expected = _productsToGenerate;
            int formattedNameCount = 0;

            foreach (var product in _products)
            {
                string productName = $" {product.Category} {product.Type}";
                string productAdjective = product.Name.Substring(0, product.Name.Length - productName.Length);
                if (_adjectives.Contains(productAdjective))
                {
                    formattedNameCount++;
                }
            }
            Assert.Equal(expected, formattedNameCount);
        }
        [Fact]
        public void TestProductDescription_AllProductsHaveDescription()
        {
            int expected = _productsToGenerate;
            int descriptionCount = 0;
            foreach (var product in _products)
            {
                if (product.Description != null && product.Description.Length > 0)
                {
                    descriptionCount++;
                }
            }
            Assert.Equal(expected, descriptionCount);
        }

        [Fact]
        public void TestProductDescription_AllProductsHaveCorrectlyFormattedDescription()
        {
            int expected = _productsToGenerate;
            int formattedDescriptionCount = 0;

            foreach (var product in _products)
            {
                string[] descriptionElements = product.Description.Split(", ");
                if (descriptionElements.Length == 3 && _categories.Contains(descriptionElements[0]) && _demographics.Contains(descriptionElements[1]) && _adjectives.Contains(descriptionElements[2]))
                {
                    formattedDescriptionCount++;
                }
            }
            Assert.Equal(expected, formattedDescriptionCount);
        }

        [Fact]
        public void TestProductSytleNumber_AllProductsHaveStyleNumber()
        {
            int expected = _productsToGenerate;
            int styleNumberCount = 0;

            foreach (var product in _products)
            {
                if (product.StyleNumber != null && product.StyleNumber.Length > 0)
                {
                    styleNumberCount++;
                }
            }
            Assert.Equal(expected, styleNumberCount);
        }

        [Fact]
        public void TestProductStyleNumber_AllProductsHaveCorrectlyFormattedStyleNumber()
        {
            int expected = _productsToGenerate;
            int styleNumberCount = 0;
            Regex regex = new Regex(@"sc\d{5}");

            foreach (var product in _products)
            {
                if ((product.StyleNumber.Length == 7) && regex.IsMatch(product.StyleNumber))
                {
                    styleNumberCount++;
                }
            }
            Assert.Equal(expected, styleNumberCount);
        }

        [Fact]
        public void TestProductGlobalProductCode_AllProductsHaveGlobalProductCode()
        {
            int expected = _productsToGenerate;
            int globalProductCodeCount = 0;

            foreach (var product in _products)
            {
                if (product.GlobalProductCode != null && product.GlobalProductCode.Length > 0)
                {
                    globalProductCodeCount++;
                }
            }
            Assert.Equal(expected, globalProductCodeCount);
        }

        [Fact]
        public void TestProductGlobalProductCode_AllProductsHaveCorrectlyFormattedGlobalProductCode()
        {
            int expected = _productsToGenerate;
            int globalProductCodeCount = 0;
            Regex regex = new Regex(@"po-[A-Z]{7}");

            foreach (var product in _products)
            {
                if ((product.GlobalProductCode.Length == 10) && regex.IsMatch(product.GlobalProductCode))
                {
                    globalProductCodeCount++;
                }
            }
            Assert.Equal(expected, globalProductCodeCount);
        }

        [Fact]
        public void TestProductReleaseDate_AllProductsHaveReleaseDate()
        {
            int expected = _productsToGenerate;
            int releaseDateCount = 0;

            foreach (var product in _products)
            {
                if (product.ReleaseDate != null && product.ReleaseDate.Length > 0)
                {
                    releaseDateCount++;
                }
            }
            Assert.Equal(expected, releaseDateCount);
        }

        [Fact]
        public void TestProductReleaseDate_AllProductsHaveCorrectlyFormattedReleaseDate()
        {
            int expected = _productsToGenerate;
            int releaseDateCount = 0;

            foreach (var product in _products)
            {
                if (DateTime.TryParse(product?.ReleaseDate, out DateTime date))
                {

                    releaseDateCount++;
                }
            }
            Assert.Equal(expected, releaseDateCount);
        }


        [Fact]
        public void TestProductQuantity_AllProductsQuantity()
        {
            int expected = _productsToGenerate;
            int quantityCount = 0;

            foreach (var product in _products)
            {
                if (product.Quantity != null && product.Quantity.Length > 0)
                {
                    quantityCount++;
                }
            }
            Assert.Equal(expected, quantityCount);
        }

        [Fact]
        public void TestProductQuantity_AllProductsHaveCorrectlyFormattedQuantity()
        {
            int expected = _productsToGenerate;
            int quantityCount = 0;

            foreach (var product in _products)
            {
                if (int.TryParse(product?.Quantity, out int quantity))
                {
                    if (quantity >= 0)
                    {
                        quantityCount++;
                    }
                }
            }
            Assert.Equal(expected, quantityCount);
        }
    }
}
