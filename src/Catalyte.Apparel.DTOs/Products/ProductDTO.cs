using System;

namespace Catalyte.Apparel.DTOs.Products
{
    /// <summary>
    /// Describes a data transfer object for a product.
    /// </summary>
    public class ProductDTO
    {
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Name { get; set; }

        public string Sku { get; set; }

        public string Description { get; set; }

        public string Demographic { get; set; }

        public string Category { get; set; }

        public string Type { get; set; }

        public DateTime ReleaseDate { get; set; }

        private string PrimaryColorCode { get; set; }

        private string SecondaryColorCode { get; set; }

        private string StyleNumber { get; set; }

        private string GlobalProductCode { get; set; }

        private bool Active { get; set; }
    }
}
