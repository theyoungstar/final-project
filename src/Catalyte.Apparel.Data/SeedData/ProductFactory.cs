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
        public readonly List<string> _categories = new()
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

        public readonly List<string> _types = new()
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

        private Dictionary<string, List<string>> _material = new()
        {
            { "Pant", new List<string>() { "Cotton", "Wool", "Polyester", "Linen", "Knit" } },
            { "Short", new List<string>() { "Cotton", "Polyester", "Linen", "Nylon" } },
            { "Shoe", new List<string>() { "Leather", "Textiles", "Synthetic", "Foam" } },
            { "Glove", new List<string>() { "Leather", "Metal Mesh", "Canvas", "Neoprene" } },
            { "Jacket", new List<string>() { "Leather", "Wool", "Nylon", "Polyester", "Silk" } },
            { "Tank Top", new List<string>() { "Cotton", "Jersey", "Synthetic", "Polyester" } },
            { "Sock", new List<string>() { "Cotton", "Wool", "Cotton/Poly Blend", "Cotton/Silk Blend", "Cotton/Wool Blend" } },
            { "Sunglasses", new List<string>() { "Glass", "Acrylic", "Polycarbonate", "CR-39", "Polyurethene" } },
            { "Hat", new List<string>() { "Cotton", "Cotton Blends", "Wool", "Jersey Mesh", "Wool", "Acrylic/Wool Blend" } },
            { "Helmet", new List<string>() { "Polyethylene", "Aluminum", "Fiberglass" } },
            { "Belt", new List<string>() { "Leather", "Cotton", "Nylon", "Polyesterh" } },
            { "Visor", new List<string>() { "Polycarbonate", "Nylon Mesh", "Acetate", "Steel Mesh" } },
            { "Shin Guard", new List<string>() { "Fiberglass", "Polyurethane", "Foam Rubber" } },
            { "Elbow Pad", new List<string>() { "Polyester 4", "EPE 3", "EVA 3", "Nylon 3", "PVC 2" } },
            { "Headband", new List<string>() { "Terry Clot", "Polyester", "Nylon" } },
            { "Wristband", new List<string>() { "Silicone", "Vinyl", "Tyvek" } },
            { "Hoodie", new List<string>() { "Cotton", "Polyester", "Cotton Blends", "Wool" } },
            { "Flip Flop", new List<string>() { "Rubber", "Foam", "Plastic", "Suede", "Leather" } },
            { "Pool Noodle", new List<string>() { "Polyethylene Foam" } }
        };

        private List<string> _randomMaterials = new()
        {
            "Cotton",
            "Synthetic",
            "Wool",
            "Leather"
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
        /// Returns list of demographics.
        /// </summary>
        /// <returns>A list of demographic strings</returns>
        public List<string> GetAllDemographics() => _demographics;

        /// <summary>
        /// Generates a random product offering id.
        /// </summary>
        /// <returns>A product offering string.</returns>
        private string GetGlobalProductCode() => "po-" + RandomString(7);

        /// <summary>
        /// Returns a random boolean indicating active.
        /// </summary>
        /// <returns>Active as either true or false value boolean.</returns>
        private bool GetActive(string inventory)
        {
            bool active = false;
            if (int.TryParse(inventory, out int quantity))
            {
                if (quantity > 0)
                {
                    active = _active[_rand.Next(0, _active.Count)];
                }
            }
            return active;
        }
        /// <summary>
        /// Returns a random boolean indicating active.
        /// </summary>
        /// <returns>Active as either true or false value boolean.</returns>
        public bool GetOnlyActive()
        {
            return true;
        }

        /// <summary>
        /// Returns a random cateogry from the list of categories.
        /// </summary>
        /// <returns>A category string.</returns>
        private string GetCategory() => _categories[_rand.Next(0, _categories.Count)];

        /// <summary>
        /// Returns a list of all categories used in testing.
        /// </summary>
        /// <returns>A list of category strings.</returns>
        /// 
        public List<string> GetAllCategories() => _categories;
        /// <summary>
        /// Returns a random product type from the list of types.
        /// </summary>
        /// <returns>A type string.</returns>
        private string GetProductType() => _types[_rand.Next(0, _types.Count)];

        /// <summary>
        /// Returns a list of all types used in testing.
        /// </summary>
        /// <returns>A list of type strings.</returns>
        public List<string> GetAllProductTypes() => _types;
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
        ///  Returns a list of color codes used in testing.
        /// </summary>
        /// <returns>A list of color code strings.</returns>
        public List<string> GetAllColors() => _colors;

        /// <summary>
        /// Returns  a random adjective from the list of adjectives.
        /// </summary>
        /// <returns>An adjective string.</returns>
        private string GetProductAdjective() => _adjectives[_rand.Next(0, _adjectives.Count)];

        /// <summary>
        /// Returns a list of all adjectives used in testing;
        /// </summary>
        /// <returns>A list of adjective strings.</returns>
        public List<string> GetAllAdjectives() => _adjectives;

        /// <summary>
        /// Returns a random product release date ranging from 1/1/2017 to today.
        /// </summary>
        /// <returns>A product release string MM/DD/YYYY format.</returns>
        private string GetReleaseDate()
        {
            DateTime start = DateTime.UtcNow.AddYears(-3);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_rand.Next(range)).ToString("MM/dd/yyyy");
        }
        /// <summary>
        /// Generates a random style number.
        /// </summary>
        /// <returns>A style number string sc#####.</returns>
        private string GetStyleCode() => "sc" + _rand.Next(0, 100000).ToString("D5");

        /// <summary>
        /// Returns a random brand name from a list of brand names.
        /// </summary>
        /// <returns>A brand string.</returns>
        private string GetBrand() => _brand[_rand.Next(0, _brand.Count)];

        /// <summary>
        /// Returns a list of all brands used in testing.
        /// </summary>
        /// <returns>A list of brand strings.</returns>
        public List<string> GetAllBrands() => _brand;

        /// <summary>
        /// Returns a random generated price.
        /// </summary>
        /// <returns>A formated price string with 2 digits to the right of the decimal.</returns>
        private string GetPrice() => $"{Math.Round(_rand.NextDouble() * 100, 2, MidpointRounding.AwayFromZero).ToString("F2")}";

        /// <summary>
        /// Returns a random material from a list of materials.
        /// </summary>
        /// <returns>A material string.</returns>
        private string GetMaterial(string type)
        {
            string material;

            if (_material.TryGetValue(type, out List<string> materialList))
            {
                material = materialList[_rand.Next(0, materialList.Count)];
            }
            else
            {
                material = _randomMaterials[_rand.Next(0, _randomMaterials.Count)];
            }
            return material;
        }

        /// <summary>
        /// Returns a list of all materials used in testing;
        /// </summary>
        /// <returns>A dictionary containing  key<type>, value<list of strings> pairs.</returns>
        public Dictionary<string, List<string>> GetAllMaterials() => _material;
        /// <summary>
        /// Returns a random quantity.
        /// </summary>
        /// <returns>A quantity string</returns>
        /// 
        private string GetQuantity() => _rand.Next(0, 101).ToString();

        private string GetImageSrc()
        {
            return new Uri("https://m.media-amazon.com/images/I/81zNUlGpqJL._AC_UY550_.jpg").ToString();
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
                productList.Add(GenerateRandomProduct(i + 1));
            }

            return productList;
        }
        /// <summary>
        /// Generates a number of active products based on input.
        /// </summary>
        /// <param name="numberOfProducts"></param>
        /// <returns></returns>
        public List<Product> GenerateActiveProducts(int numberOfProducts)
        {

            var productList = new List<Product>();

            for (var i = 0; i < numberOfProducts; i++)
            {
                productList.Add(GenerateActiveProduct(i + 1));
            }

            return productList;
        }
        /// <summary>
        /// Uses random generators to build a products.
        /// </summary>
        /// <param name="id">ID to assign to the product.</param>
        /// <returns>A randomly generated product.</returns>
        private Product GenerateRandomProduct(int id)
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
            product.GlobalProductCode = GetGlobalProductCode();
            product.StyleNumber = GetStyleCode();
            product.ReleaseDate = GetReleaseDate();
            product.DateCreated = DateTime.UtcNow;
            product.DateModified = DateTime.UtcNow;
            product.Quantity = GetQuantity();
            product.Active = GetActive(product.Quantity);
            product.Name = $"{adjective} {product.Category} {product.Type}";
            product.Description = $"{product.Category}, {product.Demographic}, {adjective}";
            product.Brand = GetBrand();
            product.Price = GetPrice();
            product.Material = GetMaterial(product.Type);

            product.ImageSrc = GetImageSrc();

            return product;
        }
        /// <summary>
        /// Generates an active product based on product Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GenerateActiveProduct(int id)
        {
            var product = GenerateRandomProduct(id);
            product.Active = true;
            return product;
        }
        /// <summary>
        /// Generates an inactive product based on product Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GenerateInactiveProduct(int id)
        {
            var product = GenerateRandomProduct(id);
            product.Active = false;
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
