using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Catalyte.Apparel.DTOs.Purchases;
using Catalyte.Apparel.Test.Integration.Utilities;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Catalyte.Apparel.DTOs.Purchases;

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
        public async Task CreatePurchaseAsync_GivenInactiveItemsAreInPurchase_Returns422()
        {
            var testLineItem = new List<LineItemDTO>
            { new LineItemDTO
            {
                ProductId = 3,
                Quantity = 1
            }
            };

            var testDeliveryAddress = new DeliveryAddressDTO
            {
                DeliveryCity = "Atlanta",
                DeliveryFirstName = "Joe",
                DeliveryLastName = "Deer",
                DeliveryState = "GA",
                DeliveryStreet = "123 Main st",
                DeliveryStreet2 = "Apt A",
                DeliveryZip = 12345
            };

            var testBillingAddress = new BillingAddressDTO
            {
                BillingCity = "Atlanta",
                BillingState = "GA",
                BillingStreet = "123 Main St",
                BillingStreet2 = "Apt A",
                BillingZip = 12345,
                Email = "abc@123.org",
                Phone = "123-234-2342"
            };

            var testCreditCard = new CreditCardDTO
            {
                CardNumber = "4532520100683461",
                CVV = 789,
                Expiration = "11/22",
                CardHolder = "Max Perkins"
            };

            var purchaseDTO = new CreatePurchaseDTO
            {
                OrderDate = System.DateTime.Now,
                DeliveryAddress = testDeliveryAddress,
                BillingAddress = testBillingAddress,
                CreditCard = testCreditCard,
                LineItems = testLineItem
            };
            var inactiveProdPurchaseDTO = JsonContent.Create(purchaseDTO);
            var response = await _client.PostAsync("/purchases", inactiveProdPurchaseDTO);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

        [Fact]
        public async Task CreatePurchaseAsync_GivenAllItemsAreActiveInPurchase_Returns200()
        {
            var testLineItem = new List<LineItemDTO>
            { new LineItemDTO
            {
                ProductId = 1,
                Quantity = 1,

            }
            };

            var testDeliveryAddress = new DeliveryAddressDTO
            {
                DeliveryCity = "Atlanta",
                DeliveryFirstName = "Joe",
                DeliveryLastName = "Deer",
                DeliveryState = "GA",
                DeliveryStreet = "123 Main st",
                DeliveryStreet2 = "Apt A",
                DeliveryZip = 12345
            };

            var testBillingAddress = new BillingAddressDTO
            {
                BillingCity = "Atlanta",
                BillingState = "GA",
                BillingStreet = "123 Main St",
                BillingStreet2 = "Apt A",
                BillingZip = 12345,
                Email = "abc@123.org",
                Phone = "123-234-2342"
            };

            var testCreditCard = new CreditCardDTO
            {
                CardNumber = "4532520100683461",
                CVV = 789,
                Expiration = "11/22",
                CardHolder = "Max Perkins"
            };

            var purchaseDTO = new CreatePurchaseDTO
            {
                OrderDate = System.DateTime.Now,
                DeliveryAddress = testDeliveryAddress,
                BillingAddress = testBillingAddress,
                CreditCard = testCreditCard,
                LineItems = testLineItem
            };
            var inactiveProdPurchaseDTO = JsonContent.Create(purchaseDTO);
            var response = await _client.PostAsync("/purchases", inactiveProdPurchaseDTO);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        }
    }
}
        