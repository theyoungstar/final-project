using AutoMapper;
using Catalyte.Apparel.API.Controllers;
using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.SeedData;
using Catalyte.Apparel.DTOs.Purchases;
using Catalyte.Apparel.Providers.Interfaces;
using Catalyte.Apparel.Providers.Providers;
using Catalyte.Apparel.Test.Integration.Utilities;
using Catalyte.Apparel.Utilities.HttpResponseExceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;



namespace Catalyte.Apparel.Test.Unit.Providers.Unit
{
    public class PurchaseControllerUnitTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly PurchaseFactory _factory = new();

        private readonly Mock<ILogger<PurchasesController>> loggerStub;
        private readonly Mock<IPurchaseProvider> providerStub;
        private readonly Mock<IMapper> mapperStub;
        private readonly Mock<IPurchaseRepository> repositoryStub;

        private readonly PurchasesController controller;


        private readonly HttpClient _client;

        private readonly Data.Model.Purchase testProduct;
        private readonly List<Data.Model.Purchase> testPurchases;


        public PurchaseControllerUnitTests(CustomWebApplicationFactory factory)
        {
            loggerStub = new Mock<ILogger<PurchasesController>>();
            providerStub = new Mock<IPurchaseProvider>();
            mapperStub = new Mock<IMapper>();
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
        
        [Fact]
        public async Task GetAllPurchasesByEmailAsync_GivenEmailPath_ReturnsOK()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/purchases/email/customer@home.com");

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }
}