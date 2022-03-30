using Catalyte.Apparel.Data.Context;
using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.Data.SeedData;

namespace Catalyte.Apparel.Test.Integration.Utilities
{
    public static class DatabaseSetupExtensions
    {
        public static void InitializeDatabaseForTests(this ApparelCtx context)
        {
            var productFactory = new ProductFactory();
            var products = productFactory.GenerateRandomProducts(250);
            var purchase = new Purchase() { 
                
                BillingCity = "Atlanta",
                BillingEmail = "customer@home.com",
                BillingPhone = "(714) 345-8765",
                BillingState = "GA",
                BillingStreet = "123 Main",
                BillingStreet2 = "Apt A",
                BillingZip = "31675",
                DateCreated = System.DateTime.UtcNow,
                DateModified = System.DateTime.UtcNow,
                DeliveryCity = "Birmingham",
                DeliveryState = "AL",
                DeliveryStreet = "123 Hickley",
                DeliveryZip = 43690,
                DeliveryFirstName = "Max",
                DeliveryLastName = "Space",
                CardHolder = "Max Perkins",
                CardNumber = "1435678998761234",
                Expiration = "11/21",
                CVV = 456,
                OrderDate = new System.DateTime(2021, 5, 4)
            };
        
            

            purchase.BillingEmail = "customer@home.com";
            context.Purchases.AddRange(purchase);
            context.Products.AddRange(products);
            context.SaveChanges();
        }

        public static void ReinitializeDatabaseForTests(this ApparelCtx context)
        {
            context.Products.RemoveRange(context.Products);
            context.InitializeDatabaseForTests();
        }

    }
}
