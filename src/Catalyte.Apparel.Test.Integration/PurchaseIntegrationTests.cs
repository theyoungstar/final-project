using Catalyte.Apparel.DTOs.Purchases;
using Catalyte.Apparel.Test.Integration.Utilities;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using System;

namespace Catalyte.Apparel.Test.Integration
{
    [Collection("Sequential")]
    public class PurchaseIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

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
            var firstPurchase = content.FirstOrDefault();
            Assert.Equal("customer@home.com", firstPurchase.BillingAddress.Email);
        }
        [Fact]
        public async Task GetPurchasesByEmailAsync_GivenEmailWithNoPurchases_Returns200()
        {
            var response = await _client.GetAsync("/purchases/email/customer1@home.com");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsAsync<IEnumerable<PurchaseDTO>>();
            var firstPurchase = content;
            Assert.Equal(Array.Empty<object>(), firstPurchase);
        }

        [Fact]
        public async Task GetPurchasesByEmailAsync_GivenNoEmailPath_Returns404()
        {
            var response = await _client.GetAsync("/purchases");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}

