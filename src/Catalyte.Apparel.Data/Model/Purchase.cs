using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Catalyte.Apparel.Data.Model
{
    /// <summary>
    /// Describes a purchase object that holds the information for a transaction.
    /// </summary>
    public class Purchase : BaseEntity
    {
        public DateTime OrderDate { get; set; }

        [MaxLength(100)]
        public string BillingStreet { get; set; }

        [MaxLength(100)]
        public string BillingStreet2 { get; set; }

        [MaxLength(50)]
        public string BillingCity { get; set; }

        [MaxLength(2)]
        public string BillingState { get; set; }

        [MaxLength(10)]
        public string BillingZip { get; set; }

        [MaxLength(100)]
        public string BillingEmail { get; set; }

        [MaxLength(15)]
        public string BillingPhone { get; set; }

        [MaxLength(50)]
        public string DeliveryFirstName { get; set; }

        [MaxLength(50)]
        public string DeliveryLastName { get; set; }

        [MaxLength(100)]
        public string DeliveryStreet { get; set; }

        [MaxLength(100)]
        public string DeliveryStreet2 { get; set; }

        [MaxLength(50)]
        public string DeliveryCity { get; set; }

        [MaxLength(2)]
        public string DeliveryState { get; set; }

        [MaxLength(10)]
        public int DeliveryZip { get; set; }

        [MaxLength(16)]
        public string CardNumber { get; set; }

        public int CVV { get; set; }

        [MaxLength(5)]
        public string Expiration { get; set; }

        [MaxLength(100)]
        public string CardHolder { get; set; }

        public ICollection<LineItem> LineItems { get; set; }
    }
}
