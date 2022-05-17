using System;
using System.Collections.Generic;
using System.Linq;
using Catalyte.Apparel.Data.Model;

namespace Catalyte.Apparel.Data.SeedData
{
    /// <summary>
    /// This class provides a function for assigning data to a ShippingRate.
    /// </summary>
    public class ShippingRates
    {
        /// <summary>
        /// Assigns shipping rates and id to each state.
        /// </summary>
        /// <returns>A list of ShippingRates</returns>
        public List<ShippingRate> AssignRates(string[] statesArray)
        {
            var shippingRateList = new List<ShippingRate>();
           
            foreach (string state in statesArray)
            {
                var shippingRate = new ShippingRate();
                shippingRateList.Add(shippingRate);
                
                if (state == "Hawaii" || state == "Alaska")
                {
                    shippingRate.Rate = 10.00;
                    shippingRate.State = state;
                    var id = shippingRateList.IndexOf(shippingRate) + 1;
                    shippingRate.Id = id;
                }
                else
                {
                    shippingRate.Rate = 5.00;
                    shippingRate.State = state;
                    var id = shippingRateList.IndexOf(shippingRate) + 1;
                    shippingRate.Id = id;

                }
            }
            return shippingRateList;

        }
    }
}

