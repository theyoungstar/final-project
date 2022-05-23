namespace Catalyte.Apparel.DTOs.PromoCodes
{
    /// <summary>
    /// Describes a data transfer object for creating a promo code entry.
    /// </summary>
    public class CreatePromoCodeDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public decimal Rate { get; set; }
    }
}
