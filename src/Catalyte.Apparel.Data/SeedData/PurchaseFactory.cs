using Catalyte.Apparel.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalyte.Apparel.Data.SeedData
{
    /// <summary>
    /// This class provides tools for generating random purchases.
    /// </summary>
    public class PurchaseFactory
    {
        Random _rand = new();

        private readonly List<string> _billingcity = new()
        {
            "Atlanta",
            "Nashville",
            "New York",
            "San Diego",
            "Portland",
            "Rio",
            "Jacksonville",
            "Raleigh",
            "Des Moines",
            "Dallas",
            "Kalamazoo",
            "Richmond",
        };
        private readonly List<string> _email = new()
        {
            "customer@home.com",
            "hello@home.com",
            "bonjour@home.com",
            "hola@home.com",
            "gutentag@home.com",
            "oi@home.com",
            "konnichiwa@home.com",
            "salve@home.com",
        };

        private List<string> _phone = new()
        {
            "(714) 345-8765",
            "(325) 236-1532",
            "(756) 654-4326",
            "(459) 234-8734",
            "(653) 384-9034",
            "(234) 967-4892",
            "(753) 348-2094",
            "(152) 975-1845",
            "(968) 163-1849",
            "(947) 327-1957",
        };

        private List<string> _state = new()
        {
            "New York",
            "Georgia",
            "North Carolina",
            "Iowa",
            "Tennessee",
            "California",
            "Arizona",
            "Oregon",
            "Texas",
            "Michigan",
            "Kentucky",
            "Virginia",
            "Utah",
            "Washington",
            "Alabama",
            "Vermont"
            
        };

        /// <summary>
        /// Generates a randomized state from the state list.
        /// </summary>
        /// <returns>A state string.</returns>
        private string GetState()
        {
            return _state[_rand.Next(0, 2)];
        }

        /// <summary>
        /// Generates a randomized phone from the phone list.
        /// </summary>
        /// <returns>A demographic string.</returns>
        private string GetPhone()
        {
            return _phone[_rand.Next(0, 2)];
        }

        /// <summary>
        /// Generates a randomized email from the email list.
        /// </summary>
        /// <returns>A style code string.</returns>
        private string GetEmail()
        {
            return _email[_rand.Next(0, 2)];
        }

        /// <summary>
        /// Generates a randomized city from the city list.
        /// </summary>
        /// <returns>A style code string.</returns>
        private string GetCity()
        {
            return _billingcity[_rand.Next(0, 2)];
        }

        /// <summary>
        /// Generates a number of random purchases based on input.
        /// </summary>
        /// <param name="numberOfProducts">The number of random products to generate.</param>
        /// <returns>A list of random products.</returns>
        public List<Purchase> GenerateRandomPurchases(int numberOfPurchases)
        {

            var purchaseList = new List<Purchase>();

            for (var i = 0; i < numberOfPurchases; i++)
            {
                purchaseList.Add(CreateRandomPurchase(i + 1));
            }

            return purchaseList;
        }

        /// <summary>
        /// Uses random generators to build a purchase.
        /// </summary>
        /// <param name="id">ID to assign to the product.</param>
        /// <returns>A randomly generated product.</returns>
        public Purchase CreateRandomPurchase(int id)
        {
            return new Purchase
            {
                Id = id,
                BillingCity = GetCity(),
                BillingPhone = GetPhone(),
                BillingState = GetState(),
                BillingEmail = GetEmail(),
            };
        }
    }
}
