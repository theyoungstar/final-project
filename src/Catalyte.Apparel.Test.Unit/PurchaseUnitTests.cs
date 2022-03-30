using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;
using Catalyte.Apparel.Providers.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalyte.Apparel.Test.Integration.Utilities;
using System.Net.Http;
using Catalyte.Apparel.Data.Model;

namespace Catalyte.Apparel.Test.Unit
{
    
    public class PurchaseProviderTests
    {
        

        PurchaseProvider purchaseProvider;

        //[Fact]
        //public Task GetAllPurchasesByEmailAsync_GivenInputEmail_ReturnsIEnurableList()
        //{
        //    //var response = await _client.GetAsync("/purchases/email/customer@home.com/");
        //    //var input = new object[] { 1, "Max", "customer@home.com" };
        //    var input = "customer@home.com";
        //    //input.Add(new object[] { 2, "Max", "customer@home.com" });

        //    var expected = new List<object>();

        //    var actual = purchaseProvider.GetAllPurchasesByEmailAsync(input);

        //    Assert.Equal(expected, (IEnumerable<object>)actual);

        //}
        //[Fact]
        //public void GetAllPurchasesByEmailAsync_GivenNoPurchasesByEmail_ReturnsEmptyArray()
        //{
        //    //var input = Array.Empty<string>();
        //    var input = "customer@home.com";

        //    var expected = Array.Empty<object>();

        //    var actual = purchaseProvider.GetAllPurchasesByEmailAsync(input);

        //    Assert.Equal(expected, (IEnumerable<object>)actual);
        //}

        [Fact]
        public async Task GetAllPurchasesByEmailAsync_GivenInputEmail_ReturnsIEnurableList()
        {
            var testPurchases = GetTestPurchases();
            //var controller = new SimpleProductController(testPurchases);
            var input = "customer@home.com";

            //var result = await purchaseProvider.GetAllPurchasesByEmailAsync(testPurchases) as List<Purchase>;
            var actual = await purchaseProvider.GetAllPurchasesByEmailAsync(input);
            Assert.Equal(testPurchases, actual);
        }
        private static IEnumerable<Purchase> GetTestPurchases()
        {
            var testPurchases = new List<Purchase>();
            testPurchases.Add(new Purchase { Id = 1, BillingEmail = "customer@home.com" });
            //testPurchases.Add(new Purchase { Id = 2, BillingEmail = "abc@123.com" });
            //testPurchases.Add(new Purchase { Id = 3, BillingEmail = "hello@wahoo.com" });
            //testPurchases.Add(new Purchase { Id = 4, BillingEmail = "zyx@098.com" });

            return testPurchases;
        }
        //var content = await response.Content.ReadAsAsync(< PurchaseDTO >);
        //Assert.Equal("customer@home.com", content.BillingEmail);
    }
}

//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using System.Web.Http.Results;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using StoreApp.Controllers;
//using StoreApp.Models;

//namespace StoreApp.Tests
//    {
//        [TestClass]
//        public class TestSimpleProductController
//        {
//            [TestMethod]
//            public void GetAllProducts_ShouldReturnAllProducts()
//            {
//                var testProducts = GetTestProducts();
//                var controller = new SimpleProductController(testProducts);

//                var result = controller.GetAllProducts() as List<Product>;
//                Assert.AreEqual(testProducts.Count, result.Count);
//            }

//            [TestMethod]
//            public async Task GetAllProductsAsync_ShouldReturnAllProducts()
//            {
//                var testProducts = GetTestProducts();
//                var controller = new SimpleProductController(testProducts);

//                var result = await controller.GetAllProductsAsync() as List<Product>;
//                Assert.AreEqual(testProducts.Count, result.Count);
//            }

//            [TestMethod]
//            public void GetProduct_ShouldReturnCorrectProduct()
//            {
//                var testProducts = GetTestProducts();
//                var controller = new SimpleProductController(testProducts);

//                var result = controller.GetProduct(4) as OkNegotiatedContentResult<Product>;
//                Assert.IsNotNull(result);
//                Assert.AreEqual(testProducts[3].Name, result.Content.Name);
//            }

//            [TestMethod]
//            public async Task GetProductAsync_ShouldReturnCorrectProduct()
//            {
//                var testProducts = GetTestProducts();
//                var controller = new SimpleProductController(testProducts);

//                var result = await controller.GetProductAsync(4) as OkNegotiatedContentResult<Product>;
//                Assert.IsNotNull(result);
//                Assert.AreEqual(testProducts[3].Name, result.Content.Name);
//            }

//            [TestMethod]
//            public void GetProduct_ShouldNotFindProduct()
//            {
//                var controller = new SimpleProductController(GetTestProducts());

//                var result = controller.GetProduct(999);
//                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
//            }

//            private List<Product> GetTestProducts()
//            {
//                var testProducts = new List<Product>();
//                testProducts.Add(new Product { Id = 1, Name = "Demo1", Price = 1 });
//                testProducts.Add(new Product { Id = 2, Name = "Demo2", Price = 3.75M });
//                testProducts.Add(new Product { Id = 3, Name = "Demo3", Price = 16.99M });
//                testProducts.Add(new Product { Id = 4, Name = "Demo4", Price = 11.00M });

//                return testProducts;
//            }
//        }
//    }