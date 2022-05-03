using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Catalyte.Apparel.Data.Model
{
    /// <summary>
    /// This class is a representation of a promo code.
    /// </summary>
    public class PromoCode : BaseEntity
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public int Rate { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        public static IEqualityComparer<PromoCode> PromoCodeComparer { get; } = new PromoCodeEqualityComparer();

        private sealed class PromoCodeEqualityComparer : IEqualityComparer<PromoCode>
        {
            public bool Equals(PromoCode x, PromoCode y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Title == y.Title && x.Description == y.Description && x.Type == y.Type && x.Rate == y.Rate;
            }

            public int GetHashCode(PromoCode obj)
            {
                var hashCode = new HashCode();
                hashCode.Add(obj.Title);
                hashCode.Add(obj.Description);
                hashCode.Add(obj.Type);
                hashCode.Add(obj.Rate);
                return hashCode.ToHashCode();
            }
        }

    }

}
