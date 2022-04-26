using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.Data.Repositories;
using Catalyte.Apparel.DTOs.PromoCodes;
using Catalyte.Apparel.Test.Integration.Utilities;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Catalyte.Apparel.Test.Integration
{
    [Collection("Sequential")]
    public class PromoCodeIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        private readonly PromoCode _code;

        private readonly PromoCodeRepository _promoCodeRepository;

        public PromoCodeIntegrationTests(CustomWebApplicationFactory factory)
        {
            _code = new PromoCode();

            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }


        [Fact]
        public async Task GetPromoCodes_ReturnsEmptyArrayWhenNoDataIsPresent_Returns201()
        {

            var response = await _client.GetAsync("/promocodes");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var actual = await response.Content.ReadAsAsync<IEnumerable<PromoCodeDTO>>();
            var expected = Array.Empty<object>();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task PostCodeHelper()
        {
            var promoCode = new PromoCode()
            {
                Id = 8,
                Title = "CODE01",
                Type = "%",
                Description = "Description of Promo",
                Rate = 8888,
            };

            var json = JsonContent.Create(promoCode);
            var result = await _client.PostAsync("/promocodes", json);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }
        [Fact]
        public async Task GetCodeHelper()
        {
            var response = await _client.GetAsync("/promocodes");
            var actual = await response.Content.ReadAsAsync<PromoCode>();
            if (actual != null)
            {
                _promoCodeRepository.DeletePromoCodesAsync(actual);
            }
        }
    }
}
