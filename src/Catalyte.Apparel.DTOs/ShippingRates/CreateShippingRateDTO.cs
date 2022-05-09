using System;
namespace Catalyte.Apparel.DTOs.ShippingRates
{
    public class CreateShippingRateDTO
    {
        public class ShippingRateDTO
        {
            public int Id { get; set; }

            public string State { get; set; }

            public int Rate { get; set; }

        }
    }
}
