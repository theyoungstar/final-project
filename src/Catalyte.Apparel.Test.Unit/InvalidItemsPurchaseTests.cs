/*using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Xunit;
using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Providers.Interfaces;
using Catalyte.Apparel.Providers.Providers;
using Catalyte.Apparel.Utilities.HttpResponseExceptions;

namespace Catalyte.Apparel.Test.Unit.Provider.Purchase
{
    public class PurchaseProviderTest
    {
        private readonly Mock<IPurchaseRepository> repositoryStub;
        private readonly Mock<ILogger<PurchaseProvider>> loggerStub;
        private readonly Mock<IProductRepository> productRepositoryStub;
        private readonly IPurchaseProvider purchaseProvider;

        private readonly Data.Model.Purchase testPurchase;
        private readonly List<Data.Model.Purchase> testPurchases;
        private Data.Model.Purchase purchaseDTO;

        private readonly Data.Model.Product testProductInactive1;
        private readonly Data.Model.Product testProductInactive2;
        private readonly Data.Model.Product testProductActive;
        private readonly List<Data.Model.Product> testProducts;
        private readonly List<Data.Model.LineItem> testLineItemInactiveSingle;
        private readonly List<Data.Model.LineItem> testLineItemInactiveMultiple;
        private readonly List<Data.Model.LineItem> testLineItemInactiveAndActive;
        private readonly List<Data.Model.LineItem> testLineItemActive;
        private readonly List<Data.Model.LineItem> testLineItemNull;


        private readonly Data.Model.Purchase testPurchaseSingleInactive;
        private readonly Data.Model.Purchase testPurchaseMultipleInactive;
        private readonly Data.Model.Purchase testPurchaseInactiveAndActive;
        private readonly Data.Model.Purchase testPurchaseActive;
        private readonly Data.Model.Purchase testPurchaseNull;
    
        public PurchaseProviderTest()
        {
            repositoryStub = new Mock<IPurchaseRepository>();
            productRepositoryStub = new Mock<IProductRepository>();
            loggerStub = new Mock<ILogger<PurchaseProvider>>();

            purchaseProvider = new PurchaseProvider(repositoryStub.Object, loggerStub.Object, productRepositoryStub.Object);

            provider = new PurchaseProvider(repositoryStub.Object, loggerStub.Object, productRepositoryStub.Object);

            testProductInactive1 = new Data.Model.Product { Id = 1, Active = false, Name = "Boot Shoes", };
            testProductInactive2 = new Data.Model.Product { Id = 2, Active = false, Name = "Sassy Pants" };
            testProductActive = new Data.Model.Product { Id = 3, Active = true, Name = "Doggy Shirt" };
            testProducts = new List<Data.Model.Product> { testProductInactive1, testProductInactive2, testProductActive };

            testLineItemInactiveSingle = new List<Data.Model.LineItem> { new Data.Model.LineItem { ProductId = testProductInactive1.Id } };
            testLineItemInactiveMultiple = new List<Data.Model.LineItem> { new Data.Model.LineItem { ProductId = testProductInactive2.Id }, new Data.Model.LineItem { ProductId = testProductInactive1.Id } };
            testLineItemInactiveAndActive = new List<Data.Model.LineItem> { new Data.Model.LineItem { ProductId = testProductActive.Id }, new Data.Model.LineItem { ProductId = testProductInactive1.Id }, new Data.Model.LineItem { ProductId = testProductInactive2.Id } };
            testLineItemActive = new List<Data.Model.LineItem> { new Data.Model.LineItem { ProductId = testProductActive.Id } };
            testLineItemNull = new List<Data.Model.LineItem> { new Data.Model.LineItem { ProductId = 6000 } };


            testPurchaseSingleInactive = new Data.Model.Purchase { LineItems = testLineItemInactiveSingle };
            testPurchaseMultipleInactive = new Data.Model.Purchase { LineItems = testLineItemInactiveMultiple };
            testPurchaseInactiveAndActive = new Data.Model.Purchase { LineItems = testLineItemInactiveAndActive };
            testPurchaseActive = new Data.Model.Purchase { LineItems = testLineItemActive };
            testPurchaseNull = new Data.Model.Purchase { LineItems = testLineItemNull };
        }



        /*public ProductTest()
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
        public async Task InactiveProductException_WithInactiveProducts_ThrowsError()
        {

            Func<Task> result = async () => { await purchaseProvider.InactiveProductException(testPurchaseMultipleInactive); };

            await result.Should().ThrowAsync<UnprocessableEntityException>();
        }
    }*/