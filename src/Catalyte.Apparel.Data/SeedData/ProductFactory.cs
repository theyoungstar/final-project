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

        private List<bool> _active = new()
        {
            false,
            true
        };

        private List<string> _brand = new()
        {
            "Adidas",
            "Nike",
            "Nivia",
            "Wilson",
            "Puma",
            "Champion",
            "Brooks",
            "ASCICS",
            "Admiral",
            "Reusch"
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
        private string GetDemographic() => _demographics[_rand.Next(0, _demographics.Count)];

        /// <summary>
        /// Generates a random product offering id.
        /// </summary>
        /// <returns>A product offering string.</returns>
        private string GetRandomProductId() => "po-" + RandomString(7);

        /// <summary>
        /// Returns a random boolean indicating active.
        /// </summary>
        /// <returns>Active as either true or false value boolean.</returns>
        private bool GetActive() => _active[_rand.Next(0, _active.Count)];

        /// <summary>
        /// Returns a random cateogry from the list of categories.
        /// </summary>
        /// <returns>A category string.</returns>
        private string GetCategory() => _categories[_rand.Next(0, _categories.Count)];

        /// <summary>
        /// Returns a random product type from the list of types.
        /// </summary>
        /// <returns>A type string.</returns>
        private string GetProductType() => _types[_rand.Next(0, _types.Count)];

        /// <summary>
        /// Returns a random color code from the list of color codes.
        /// </summary>
        /// <param></param>
        /// <returns>A color code string.</returns>
        private string GetColor() => _colors[_rand.Next(0, _colors.Count)];

        /// <summary>
        /// Returns a random color code from the list of color codes.
        /// Color returned is not the same as compareColor.
        /// </summary>
        /// <param name="compareColor"></param>
        /// <returns>A color code string.</returns>
        private string GetColor(string compareColor)
        {
            var color = GetColor();
            while (color == compareColor)
            {
                color = GetColor();
            }
            return color;
        }

        /// <summary>
        /// Returns  a random adjective from the list of adjectives.
        /// </summary>
        /// <returns>An adjective string.</returns>
        private string GetProductAdjective() => _adjectives[_rand.Next(0, _adjectives.Count)];

        /// <summary>
        /// Returns a random product release date ranging from 1/1/2017 to today.
        /// </summary>
        /// <returns>A product release dateTime.</returns>
        private DateTime GetReleaseDate()
        {
            DateTime start = new DateTime(2017, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_rand.Next(range));
        }
        /// <summary>
        /// Generates a random style number.
        /// </summary>
        /// <returns>A style number string.</returns>
        private string GetStyleCode() =>"sc" + _rand.Next(10000,100000).ToString();

        private string GetBrand() => _brand[_rand.Next(0,_brand.Count)];

        private string GetPrice() => (_rand.NextDouble() * 100).ToString();
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
            var product = new Product();

            var adjective = GetProductAdjective();
            product.Id = id;
            product.Category = GetCategory();
            product.Type = GetProductType();
            product.Sku = GetRandomSku();
            product.PrimaryColorCode = GetColor();
            product.SecondaryColorCode = GetColor(product.PrimaryColorCode);
            product.Demographic = GetDemographic();
            product.GlobalProductCode = GetRandomProductId();
            product.StyleNumber = GetStyleCode();
            product.ReleaseDate = GetReleaseDate();
            product.DateCreated = DateTime.UtcNow;
            product.DateModified = DateTime.UtcNow;
            product.Active = GetActive();
            product.Name = $"{adjective} {product.Category} {product.Type}";
            product.Description = $"{product.Category}, {product.Demographic}, {adjective}";
            product.Brand = GetBrand();
            product.Price = GetPrice();
            
            return product;
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
