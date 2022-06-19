using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.Data.SeedData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Catalyte.Apparel.Data.Context
{
    public static class Extensions
    {
        /// <summary>
        /// Produces a set of seed data to insert into the database on startup.
        /// </summary>
        /// <param name="modelBuilder">Used to build model base DbContext.</param>
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            var productFactory = new ProductFactory();
            var shippingRateList = new ShippingRates();


            modelBuilder.Entity<Product>().HasData(productFactory.GenerateRandomProducts(1000));

            var lineItem = new LineItem()
            {
                Id = 1,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                ProductId = 1,
                Quantity = 1,
                PurchaseId = 1
            };

            modelBuilder.Entity<LineItem>().HasData(lineItem);

            var purchase = new Purchase()
            {
                Id = 1,
                BillingCity = "Atlanta",
                BillingEmail = "customer@home.com",
                BillingPhone = "(714) 345-8765",
                BillingState = "GA",
                BillingStreet = "123 Main",
                BillingStreet2 = "Apt A",
                BillingZip = "31675",
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                DeliveryCity = "Birmingham",
                DeliveryState = "AL",
                DeliveryStreet = "123 Hickley",
                DeliveryZip = 43690,
                DeliveryFirstName = "Max",
                DeliveryLastName = "Space",
                CardHolder = "Max Perkins",
                CardNumber = "1435678998761234",
                Expiration = "11/21",
                CVV = "123",
                OrderDate = new DateTime(2021, 5, 4)
            };

            modelBuilder.Entity<Purchase>().HasData(purchase);

            var promoCode = new PromoCode()
            {
                Id = 1,
                Title = "CODE00",
                Type = "flat or %",
                Description = "Description of Promo",
                Rate = 8888,
            };


            //modelBuilder.Entity<PromoCode>().HasData(promoCode);
       
            var statesArray = new string []
            {
            "AL",
            "AK",
            "AS",
            "AZ",
            "AR",
            "CA",
            "CO",
            "CT",
            "DE",
            "DC",
            "FM",
            "FL",
            "GA",
            "GU",
            "HI",
            "ID",
            "IL",
            "IN",
            "IA",
            "KS",
            "KY",
            "LA",
            "ME",
            "MH",
            "MD",
            "MA",
            "MI",
            "MN",
            "MS",
            "MO",
            "MT",
            "NE",
            "NV",
            "NH",
            "NJ",
            "NM",
            "NY",
            "NC",
            "ND",
            "MP",
            "OH",
            "OK",
            "OR",
            "PW",
            "PA",
            "PR",
            "RI",
            "SC",
            "SD",
            "TN",
            "TX",
            "UT",
            "VT",
            "VI",
            "VA",
            "WA",
            "WV",
            "WI",
            "WY",
            };
            modelBuilder.Entity<ShippingRate>().HasData(shippingRateList.AssignRates(statesArray));

            var user = new User()
            {
                Id = 1,
                Email = "blamboy@catalyte.io",
                Role = "customer",
                FirstName = "Benjamin",
                LastName = "Lamboy",
                Street = "123 Main",
                City = "Denver",
                State = "Colorado",
                Zip = 12345
            };

            modelBuilder.Entity<User>().HasData(user);

            var patient = new Patient()
            {
                Id = 1,
                FirstName = "Hulk",
                LastName = "Hogan",
                Ssn = "123-45-6789",
                Email = "hulksnewemailaddress@wwf.com",
                Age = 66,
                Height = 79,
                Weight = 299,
                Insurance = "Self-Insured",
                Gender = "Male",
                Street = "8430 W Sunset Blvd",
                City = "Los Angeles",
                State = "CA",
                Postal = "90049"
            };

            modelBuilder.Entity<Patient>().HasData(patient);

            var encounter = new Encounter()
            {
                Id = 1,
                PatientId = 1,
                Notes = "New Encounter",
                VisitCode = "N3W 3C3",
                Provider = "New Hospital",
                BillingCode = "123.456.789.00",
                ICD10 = "Z99",
                TotalCost = 10.00,
                Copay = 5.25,
                ChiefComplaint = "New Complaint",
                Pulse = "",
                Systolic = "",
                Diastolic = "",
                Date = "2020-08-04"
            };

            modelBuilder.Entity<Encounter>().HasData(encounter);

        }
    }
}
