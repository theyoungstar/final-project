using Catalyte.Apparel.API;
using Catalyte.Apparel.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Text.RegularExpressions;

namespace Catalyte.Apparel.Utilites
{
    
    public class CardValidation
    {
        private static void DateFormat(string field)
        {
            Regex regex = new Regex(@"/^(0[1-9]|1[0-2])\/?([0-9]{2})$/", RegexOptions.Compiled);
            var dateMatch = field;
            Match match = regex.Match(dateMatch);
            {
                if(!match.Success)
                {
                    throw new ArgumentException(nameof(field));
                }
            }
        }
        public void IsLength(string field, int minLength, int maxLength)
        {
            //minLength = 0;
           // maxLength = 0;
                int length = field.Length;
            if(length < minLength || length > maxLength)
            {
                throw new ArgumentException(nameof(field));
            }
        }
        public void IsNumber(string field)
        {
            decimal cardNumber;
            if(!Decimal.TryParse(field, out cardNumber))
            {
                throw new ArgumentException(nameof(field));
            }
        }
        private void IsEmpty(string field)
        {
            if(field == null || field.Length == 0)
            {
                throw new ArgumentException(nameof(field));
            }
        }
        public string CardNumberValidation(string number)
        {

            var cardNumber = number.Trim();
            

            try
            {
                IsEmpty(cardNumber);
            }
            catch (Exception ex)
            {
                return null;
                //logger.LogError(ex, "An error occured seeding the database with the message: " + ex.Message);
            }


            try
            {
                IsNumber(cardNumber);
            }
            catch (Exception ex)
            {

            }
            return cardNumber;
        }

        public string[] CVVNumberValidation(string cvv)
        {
            string cvvNumber = "";
            int cvvLength = cvvNumber.Length;
            if (cvvLength == 0 || cvvNumber == "")
            {
                
            }
            return CVVNumberValidation(cvvNumber);
        }
        
        public string[] ExpirationDateValidation()
        {
            string expirationDate = "";
            int lengthOfDate = expirationDate.Length;
            DateTime today = DateTime.Now;
            if (expirationDate == "" || lengthOfDate == 0)
            {
                throw new NotImplementedException("A valid Expiration date is required");
            }
        
            return ExpirationDateValidation();
          

        }
        
    }
}
