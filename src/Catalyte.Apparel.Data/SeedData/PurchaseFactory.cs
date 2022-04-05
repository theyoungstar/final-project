using Catalyte.Apparel.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalyte.Apparel.Data.SeedData
{
    /// <summary>
    /// This class provides tools for generating random products.
    /// </summary>
    public class PurchaseFactory
    {
        Random _rand = new();

        private List<string> _id = new()
        {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30"
         
        };

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
        /// Generates a randomized product SKU.
        /// </summary>
        /// <returns>A SKU string.</returns>
        private string GetState()
        {
            return _state[_rand.Next(0, 2)];
        }

        /// <summary>
        /// Returns a random demographic from the list of demographics.
        /// </summary>
        /// <returns>A demographic string.</returns>
        private string GetPhone()
        {
            return _phone[_rand.Next(0, 2)];
        }

        /// <summary>
        /// Generates a random product offering id.
        /// </summary>
        /// <returns>A product offering string.</returns>
        private string GetId()
        {
            return _id[_rand.Next(0, 2)];
        }

        /// <summary>
        /// Generates a random style code.
        /// </summary>
        /// <returns>A style code string.</returns>
        private string GetEmail()
        {
            return _email[_rand.Next(0, 2)];
        }

        /// <summary>
        /// Generates a random style code.
        /// </summary>
        /// <returns>A style code string.</returns>
        private string GetCity()
        {
            return _billingcity[_rand.Next(0, 2)];
        }

        /// <summary>
        /// Generates a number of random products based on input.
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
        /// Uses random generators to build a products.
        /// </summary>
        /// <param name="id">ID to assign to the product.</param>
        /// <returns>A randomly generated product.</returns>
        private Purchase CreateRandomPurchase(int id)
        {
            return new Purchase
            {
                Id = id,
                //Category = _categories[_rand.Next(0, 9)],
                BillingCity = GetCity(),
                BillingPhone = GetPhone(),
                BillingState = GetState(),
                BillingEmail = GetEmail(),
                
               
                //Active = false do this as has purchase instead?
            };
        }

        /// <summary>
        /// Generates a random string of characters.
        /// </summary>
        /// <param name="size">Number of characters in the string.</param>
        /// <param name="lowerCase">Boolean if the character string should be lowercase only; defaults to false.</param>
        /// <returns>A random string of characters.</returns>
        //private string RandomString(int size, bool lowerCase = false)
        //{

        //    // ** Learning moment **
        //    // Code From
        //    // https://www.c-sharpcorner.com/article/generating-random-number-and-string-in-C-Sharp/

        //    // ** Learning moment **
        //    // Always use a string builder when concatenating more than a couple of strings.
        //    // Why? https://www.geeksforgeeks.org/c-sharp-string-vs-stringbuilder/
        //    var builder = new StringBuilder(size);

        //    // Unicode/ASCII Letters are divided into two blocks
        //    // (Letters 65–90 / 97–122):
        //    // The first group containing the uppercase letters and
        //    // the second group containing the lowercase.  

        //    // char is a single Unicode character  
        //    char offset = lowerCase ? 'a' : 'A';
        //    const int lettersOffset = 26; // A...Z or a..z: length=26  

        //    for (var i = 0; i < size; i++)
        //    {
        //        // ** Learning moment **
        //        // Because 'char' is a reserved word you can put '@' at the beginning to allow
        //        // its use as a variable name.  You could do the same thing with 'class'
        //        var @char = (char)_rand.Next(offset, offset + lettersOffset);
        //        builder.Append(@char);
        //    }

        //    return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        //}
    }
}
