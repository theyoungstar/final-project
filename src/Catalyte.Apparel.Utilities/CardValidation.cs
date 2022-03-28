using Catalyte.Apparel.API;
using Catalyte.Apparel.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Catalyte.Apparel.Utilites
{
    
    public class CardValidation
    {
        /*public static bool IsCreditCardInfoValid(string cardNo, string expiryDate, string cvv)
        {
            var cardCheck = new Regex(@"^(1298|1267|4512|4567|8901|8933)([\-\s]?[0-9]{4}){3}$");
            var monthCheck = new Regex(@"^(0[1-9]|1[0-2])$");
            var yearCheck = new Regex(@"^20[0-9]{2}$");
            var cvvCheck = new Regex(@"^\d{3}$");

            if (!cardCheck.IsMatch(cardNo)) // <1>check card number is valid
                return false;
            if (!cvvCheck.IsMatch(cvv)) // <2>check cvv is valid as "999"
                return false;

            var dateParts = expiryDate.Split('/'); //expiry date in from MM/yyyy            
            if (!monthCheck.IsMatch(dateParts[0]) || !yearCheck.IsMatch(dateParts[1])) // <3 - 6>
                return false; // ^ check date format is valid as "MM/yyyy"

            var year = int.Parse(dateParts[1]);
            var month = int.Parse(dateParts[0]);
            var lastDateOfExpiryMonth = DateTime.DaysInMonth(year, month); //get actual expiry date
            var cardExpiry = new DateTime(year, month, lastDateOfExpiryMonth, 23, 59, 59);

            //check expiry greater than today & within next 6 years <7, 8>>
            return (cardExpiry > DateTime.Now && cardExpiry < DateTime.Now.AddYears(6));
        }*/
        /// <summary>
        /// THis function checks the date format to make sure it entered in the following formats: 
        /// mm/yy || mm-yy.
        /// </summary>
        /// <param name="field"></param>
        private static void IsDate(string field)
        {
            //var logger = scopedServices.GetRequiredService<ILogger<WebApplicationFactory<Startup>>>();
            //logger.LogError(ex, "An error occured seeding the database with the message: " + ex.Message);
            List<string> error = new List<string>();
            Regex regex = new Regex(@"/^(0[1-9]|1[0-2])\/?([0-9]{2})$/");
            Regex regex2 = new Regex(@"/^(0[1-9]|1[0-2])-/?([0-9]{2})$/");
            var dateMatch = field;
            Match match = regex.Match(dateMatch);
            Match match2 = regex2.Match(dateMatch);
            {
                if (!match.Success || !match2.Success)
                {
                    error.Add("The expiration date must in the correct format(dd/yy or dd-yy)");
                    //throw new ArgumentException(nameof(field);
                }
            }
        }
        public void IsLength(string field, int minLength, int maxLength)
        {
            List<string> error = null;
            int length = field.Length;
            if (length < minLength || length > maxLength)
            {
                error.Add("Must be more than 14 digits and less than 20 digits");
            }
        }
        public void IsNumber(string field)
        {
            List<string> error = null;
            decimal cardNumber;
            if (!Decimal.TryParse(field, out cardNumber))
            {
                error.Add("Must be a number");
                //throw new ArgumentException(nameof(field));
            }
        }
        public string IsEmpty(string field)
        {
            List<string> error = null;
            if (field == null || field.Length == 0)
            {
                error.Add("Must not be left blank");
                //throw new ArgumentException(nameof(field));
            }
            return field;
        }
        public string CardNumberValidation(string number)
        {
            int minLength = 14;
            int maxLength = 19;
            var cardNumber = number.Trim();
            if (cardNumber.Length == 0)
            {
                return IsEmpty(cardNumber);
            }
            if (cardNumber.Length < minLength || cardNumber.Length > maxLength)
            {
                IsLength(cardNumber);
            }
            return cardNumber;
        }

        public string CVVNumberValidation(string field)
        {
            int minLength = 3;
            int maxLength = 4;
            var cvvNumber = field.Trim();
            if (cvvNumber.Length == 0)
            {
                IsEmpty(cvvNumber);
            }
            if (cvvNumber.Length > 0)
            {
                IsLength(field);
                IsNumber(field);
            }

            return cvvNumber;
        }

        public string ExpirationDateValidation(string field)
        {
            string expirationDate = "";
            int lengthOfDate = expirationDate.Length;

            if (expirationDate == "" || lengthOfDate == 0)
            {
                IsEmpty(expirationDate);
            }

            return field;


        }

    }
}
