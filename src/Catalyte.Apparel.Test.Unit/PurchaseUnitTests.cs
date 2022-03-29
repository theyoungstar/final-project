using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;
using Catalyte.Apparel.Providers.Providers;
using System.Collections.Generic;

namespace Catalyte.Apparel.Test.Unit
{
    public class PurchaseProviderTests
    {
        PurchaseProvider purchaseProvider;

        [Fact]
        public void GetAllPurchasesByEmailAsync_GivenInputEmail_ReturnsIEnurableList()
        {
            var input = new object[] { 1, "Max", "customer@home.com" };
            //input.Add(new object[] { 2, "Max", "customer@home.com" });

            var expected = new List<object>();

            var actual = purchaseProvider.GetAllPurchasesByEmailAsync(input);

            Assert.Equal(expected, (IEnumerable<object>)actual);
        }
        [Fact]
        public void GetAllPurchasesByEmailAsync_GivenNoPurchasesByEmail_ReturnsEmptyArray()
        {
            var input = Array.Empty<string>();

            var expected = Array.Empty<string>();

            var actual = purchaseProvider.GetAllPurchasesByEmailAsync(input);

            Assert.Equal(expected, (IEnumerable<string>)actual);
        }
        //var content = await response.Content.ReadAsAsync(< PurchaseDTO >);
        //Assert.Equal("customer@home.com", content.BillingEmail);
    }
}