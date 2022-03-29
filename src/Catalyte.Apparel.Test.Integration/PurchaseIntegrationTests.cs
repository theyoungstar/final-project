using Catalyte.Apparel.DTOs.Purchases;
using Catalyte.Apparel.Test.Integration.Utilities;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;


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
        public async Task GetPurchases_WithResults_Returns200()
        {
            var response = await _client.GetAsync("/products/{email}/");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task GetPurchases_NoResults_Returns200()
        {
            var response = await _client.GetAsync("/products/{email}/");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        //[Fact]
        //public async Task GetPurchasesByEmailAsync_GivenNoEmailPath_Returns404()
        //{
        //    var response = await _client.GetAsync("/purchases");
        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        //    var content = await response.Content.ReadAsAsync<PurchaseDTO>();
        //    Assert.Equal(1, content.BillingEmail);
        //}
    }
}
