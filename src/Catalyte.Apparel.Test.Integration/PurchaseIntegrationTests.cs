using Catalyte.Apparel.DTOs.Purchases;
using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.Data.SeedData;
using Catalyte.Apparel.Test.Integration.Utilities;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Net.Http.Json;
using Catalyte.Apparel.Data.Interfaces;
using Moq;
using Microsoft.Extensions.Logging;
using Catalyte.Apparel.Providers.Providers;

namespace Catalyte.Apparel.Test.Integration
{
    [Collection("Sequential")]
    public class PurchaseIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    public class PurchaseIntegrationTests
    {
        private readonly HttpClient _client;
        private readonly PurchaseFactory _factory = new();

        private readonly Mock<IPurchaseRepository> repositoryStub;
        private readonly Mock<ILogger<PurchaseProvider>> loggerStub;

        private readonly PurchaseProvider provider;

        private readonly Purchase testPurchase;
        private readonly List<Purchase> testPurchaseList;

        public PurchaseIntegrationTests()
        {
            repositoryStub = new Mock<IPurchaseRepository>();
            loggerStub = new Mock<ILogger<PurchaseProvider>>();
            provider = new PurchaseProvider(repositoryStub.Object, loggerStub.Object);

            /*testPurchase = _factory.CreateRandomPurchase(1);
            repositoryStub.Setup(repo => repo.GetAllPurchasesAsync(1))
                .ReturnsAsync(testPurchase);*/

            testPurchaseList = _factory.GenerateRandomPurchases(1);
            repositoryStub.Setup(repositoryStub => repositoryStub.GetAllPurchasesAsync())
                .ReturnsAsync(testPurchaseList);
        }

        public PurchaseIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task GetPurchasesByEmailAsync_GivenEmailWithPurchases_Returns200()
        {

            var response = await _client.GetAsync("/purchases/email/customer@home.com");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsAsync<IEnumerable<PurchaseDTO>>();
            var actual = content.FirstOrDefault().BillingAddress.Email;
            var expected = "customer@home.com";
            Assert.Equal(expected, actual);
        }
        [Fact]
        public async Task GetPurchasesByEmailAsync_GivenEmailWithNoPurchases_Returns200()
        {
            var response = await _client.GetAsync("/purchases/email/customer1@home.com");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var actual = await response.Content.ReadAsAsync<IEnumerable<PurchaseDTO>>();
            var expected = Array.Empty<object>();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetPurchasesByEmailAsync_GivenNoEmailPath_Returns404()
        {
            var response = await _client.GetAsync("/purchases");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Card_Number_Contains_Less_Than_14_Digits_Returns_Bad_Request()
        {
            
            var response = await _factory.GenerateRandomPurchases(1);
            //response.StatusCode = HttpStatusCode.BadRequest;
         
            var content = await response.Content.ReadAsStringAsync<List<string>>();

            var CardValidation = new CardValidation();
            //List<string> errors = new List<string>();
            //var stringResponse = HttpStatusCode.BadRequest.ToString();
            //var actual = CardValidation.Has14To19Digits(response, errors);
            //Assert.(actual);
            //Assert.IsFalse(actual);
            //Assert.False(actual);
        }
        [Fact]
        public async Task Card_Number_Does_Not_Accept_Letters()
        {
            //var response = await _client.GetAsync("/purchases");
            //response.StatusCode = HttpStatusCode.BadRequest;

            var CardValidation = new CardValidation();
            List<string> errors = new List<string>();
            var stringResponse = HttpStatusCode.BadRequest.ToString();
            var actual = CardValidation.Has14To19Digits(stringResponse, errors);
            //Assert.IsFalse(actual);
            Assert.False(actual);
        }

    }
}