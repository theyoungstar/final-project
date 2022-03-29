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
        public async Task GetPurchasesByEmailAsync_GivenExisitingEmail_Returns200()
        {
            var response = await _client.GetAsync("/purchases/email/{customer@home.com}/");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsAsync(< PurchaseDTO >);
            Assert.Equal("customer@home.com", content.BillingEmail);
        }
        [Fact]
        public async Task GetPurchasesByEmailAsync_GivenNonExistingEmail_Returns200()
        {
            var response = await _client.GetAsync("/purchases/email/{customer1@home.com}/");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsAsync(<PurchaseDTO>);
            Assert.Equal("customer@home.com", content.BillingEmail);
        }

        //This passes with ("/purchases/email/customer@home.com") but not ("//"), so I need to check on this
        [Fact]
        public async Task GetPurchasesByEmailAsync_GivenNoEmailPath_Returns404()
        {
            var response = await _client.GetAsync("/purchases");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            //var content = await response.Content.ReadAsAsync(<PurchaseDTO>);
            //Assert.Equal("customer@home.com", content.BillingEmail);
        }
    }
}
