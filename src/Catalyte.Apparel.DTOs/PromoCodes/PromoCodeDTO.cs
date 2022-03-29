using System;

namespace Catalyte.Apparel.DTOs.PromoCodes
{
    /// <summary>
    /// Describes a data transfer object for a promo code.
    /// </summary>
    public class PromoCodeDTO
    {
        public int Id { get; set; }
               
        public string Title { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }
               
        public int Rate { get; set; }
               
    }
}
