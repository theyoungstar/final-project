namespace Catalyte.Apparel.DTOs.ShippingRates
{
    /// <summary>
    /// Describes a data transfer object for a promo code.
    /// </summary>
    public class ShippingRateDTO
    {
        public int Id { get; set; }

        public string State { get; set; }

        public double Rate { get; set; }

    }
}
