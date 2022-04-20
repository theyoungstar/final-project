using Catalyte.Apparel.Data.Context;
using Catalyte.Apparel.Data.SeedData;

namespace Catalyte.Apparel.Test.Integration.Utilities
{
    public static class DatabaseSetupExtensions
    {
        public static void InitializeDatabaseForTests(this ApparelCtx context)
        {
            var productFactory = new ProductFactory();
            var products = productFactory.GenerateRandomProducts(250);
            var purchaseFactory = new PurchaseFactory();
            var purchase = purchaseFactory.GenerateRandomPurchases(10);

            context.Purchases.AddRange(purchase);
            context.Products.AddRange(products);
            context.SaveChanges();
        }

        public static void ReinitializeDatabaseForTests(this ApparelCtx context)
        {
            context.Products.RemoveRange(context.Products);
            context.Purchases.RemoveRange(context.Purchases);
            context.InitializeDatabaseForTests();
        }

    }
}