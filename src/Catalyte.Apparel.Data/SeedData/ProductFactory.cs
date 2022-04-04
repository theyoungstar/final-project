using Catalyte.Apparel.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalyte.Apparel.Data.SeedData
{
    /// <summary>
    /// This class provides tools for generating random products.
    /// </summary>
    public class ProductFactory
    {
        Random _rand = new();

        private List<string> _colors = new()
        {
            "#000000", // white
            "#ffffff", // black
            "#39add1", // light blue
            "#3079ab", // dark blue
            "#c25975", // mauve
            "#e15258", // red
            "#f9845b", // orange
            "#838cc7", // lavender
            "#7d669e", // purple
            "#53bbb4", // aqua
            "#51b46d", // green
            "#e0ab18", // mustard
            "#637a91", // dark gray
            "#f092b0", // pink
            "#b7c0c7"  // light gray
        };

        private readonly List<string> _demographics = new()
        {
            "Men",
            "Women",
            "Kids"
        };
        private readonly List<string> _categories = new()
        {
            "Golf",
            "Soccer",
            "Basketball",
            "Hockey",
            "Football",
            "Running",
            "Baseball",
            "Skateboarding",
            "Boxing",
            "Weightlifting"
        };

        private List<string> _adjectives = new()
        {
            "Lightweight",
            "Slim",
            "Shock Absorbing",
            "Exotic",
            "Elastic",
            "Fashionable",
            "Trendy",
            "Next Gen",
            "Colorful",
            "Comfortable",
            "Water Resistant",
            "Wicking",
            "Heavy Duty"
        };

        private List<string> _types = new()
        {
            "Pant",
            "Short",
            "Shoe",
            "Glove",
            "Jacket",
            "Tank Top",
            "Sock",
            "Sunglasses",
            "Hat",
            "Helmet",
            "Belt",
            "Visor",
            "Shin Guard",
            "Elbow Pad",
            "Headband",
            "Wristband",
            "Hoodie",
            "Flip Flop",
            "Pool Noodle"
        };

        private List<string> _skuMods = new()
        {
            "Blue",
            "Red",
            "KJ",
            "SM",
            "RD",
            "LRG",
            "SM"
        };

        /// <summary>
        /// Generates a randomized product SKU.
        /// </summary>
        /// <returns>A SKU string.</returns>
        private string GetRandomSku()
        {
            var builder = new StringBuilder();
            builder.Append(RandomString(3));
            builder.Append('-');
            builder.Append(RandomString(3));
            builder.Append('-');
            builder.Append(_skuMods[_rand.Next(0, 6)]);

            return builder.ToString().ToUpper();
        }

        /// <summary>
        /// Returns a random demographic from the list of demographics.
        /// </summary>
        /// <returns>A demographic string.</returns>
        private string GetDemographic()
        {
            return _demographics[_rand.Next(0, 2)];
        }

        /// <summary>
        /// Generates a random product offering id.
        /// </summary>
        /// <returns>A product offering string.</returns>
        private string GetRandomProductId()
        {
            return "po-" + RandomString(7);
        }

        /// <summary>
        /// Generates a random style code.
        /// </summary>
        /// <returns>A style code string.</returns>
        private string GetStyleCode()
        {
            return "sc" + RandomString(5);
        }

        /// <summary>
        /// Generates a number of random products based on input.
        /// </summary>
        /// <param name="numberOfProducts">The number of random products to generate.</param>
        /// <returns>A list of random products.</returns>
        public List<Product> GenerateRandomProducts(int numberOfProducts)
        {

            var productList = new List<Product>();

            for (var i = 0; i < numberOfProducts; i++)
            {
                productList.Add(CreateRandomProduct(i + 1));
            }

            return productList;
        }

        /// <summary>
        /// Uses random generators to build a products.
        /// </summary>
        /// <param name="id">ID to assign to the product.</param>
        /// <returns>A randomly generated product.</returns>
        private Product CreateRandomProduct(int id)
        {
            return new Product
            {
                Id = id,
                Category = _categories[_rand.Next(0, 9)],
                Type = "Short",
                Sku = GetRandomSku(),
                Demographic = GetDemographic(),
                GlobalProductCode = GetRandomProductId(),
                StyleNumber = GetStyleCode(),
                ReleaseDate = DateTime.Now,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                Active = false
            };
        }

        /// <summary>
        /// Generates a random string of characters.
        /// </summary>
        /// <param name="size">Number of characters in the string.</param>
        /// <param name="lowerCase">Boolean if the character string should be lowercase only; defaults to false.</param>
        /// <returns>A random string of characters.</returns>
        private string RandomString(int size, bool lowerCase = false)
        {

            // ** Learning moment **
            // Code From
            // https://www.c-sharpcorner.com/article/generating-random-number-and-string-in-C-Sharp/

            // ** Learning moment **
            // Always use a string builder when concatenating more than a couple of strings.
            // Why? https://www.geeksforgeeks.org/c-sharp-string-vs-stringbuilder/
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                // ** Learning moment **
                // Because 'char' is a reserved word you can put '@' at the beginning to allow
                // its use as a variable name.  You could do the same thing with 'class'
                var @char = (char)_rand.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}
