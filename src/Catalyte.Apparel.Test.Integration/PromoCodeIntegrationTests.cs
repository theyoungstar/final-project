using Catalyte.Apparel.DTOs.PromoCodes;
using Catalyte.Apparel.Test.Integration.Utilities;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using Catalyte.Apparel.Data.Model;
using System.Net.Http.Json;
using Catalyte.Apparel.Data.Repositories;
using System.IO;

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
        public async Task PostPromoCodes_Returns201()
        {

            PostCodeHelper();

            GetCodeHelper();
            //var task1 = PostCodeHelper();

           //var task2 = GetCodeHelper();

           //await Task.WhenAll(task1, task2);

           //var uri = Path.Combine("/promocodes");

           //var response = await _client.DeleteAsync(uri);
           //response.EnsureSuccessStatusCode();

           //var response = await _client.GetAsync("/promocodes");            

           //var actual = await response.Content.ReadAsAsync<IEnumerable<PromoCodeDTO>>();
            
           //await _promoCodeRepository.DeletePromoCodesAsync(actual);
           //var expected = Array.Empty<object>();
           //Assert.Equal(expected, actual);
           

        }

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

        public async Task GetCodeHelper()
        {
            var response = await _client.GetAsync("/promocodes");
            var actual = await response.Content.ReadAsAsync<PromoCode>();
            //Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            if (actual != null)
            {
                _promoCodeRepository.DeletePromoCodesAsync(actual);
            }
        }                
    }
}
