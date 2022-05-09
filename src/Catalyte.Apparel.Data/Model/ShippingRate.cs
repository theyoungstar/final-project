using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Catalyte.Apparel.Data.Model
{
    /// <summary>
    /// This class is a representation of a promo code.
    /// </summary>
    public class ShippingRate : BaseEntity
    {

        public string State { get; set; }

        public int Rate { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        public static IEqualityComparer<ShippingRate> ShippingRateComparer { get; } = new ShippingRateEqualityComparer();

        private sealed class ShippingRateEqualityComparer : IEqualityComparer<ShippingRate>
        {
            public bool Equals(ShippingRate x, ShippingRate y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.State == y.State && x.Rate == y.Rate;
            }

            public int GetHashCode(ShippingRate obj)
            {
                var hashCode = new HashCode();
                hashCode.Add(obj.State);
                hashCode.Add(obj.Rate);
                return hashCode.ToHashCode();
            }
        }

    }

}