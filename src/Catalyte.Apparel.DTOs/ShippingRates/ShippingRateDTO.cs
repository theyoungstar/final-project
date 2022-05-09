namespace Catalyte.Apparel.DTOs.PromoCodes
{
    /// <summary>
    /// Describes a data transfer object for a promo code.
    /// </summary>
    public class ShippingRateDTO
    {
        public int Id { get; set; }

        public string State { get; set; }

        public int Rate { get; set; }

    }
}
