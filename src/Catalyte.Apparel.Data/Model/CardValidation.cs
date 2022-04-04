using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Catalyte.Apparel.Data.Model
{

    public class CardValidation
    {
        public static List<string> CreditCardValidation(Purchase purchase)
        {
            List<string> errorsList = new List<string>();
            Has14To19Digits(purchase.CardNumber, errorsList);
            CVVHas3To4Digits(purchase.CVV, errorsList);
            ExpirationDateWithDashOrSlash(purchase.Expiration, errorsList);
            IsValidExpirationDate(purchase.Expiration, errorsList);
            CardHolderName(purchase.CardHolder, errorsList);

            return errorsList;
        }
        public static bool Has14To19Digits(string field, List<string> errorsList)
        {
            var cardCheck = new Regex(@"^[0-9]{14,19}$");
            if (!cardCheck.IsMatch(field.Trim())) // <1>check card number has 14 numbers
            {
                errorsList.Add("Card number must contain between 14 to 19 digits.");
                return false;
            }
            return true;
        }
        public static bool CVVHas3To4Digits(string field, List<string> errorsList)
        {
            var cvvCheck = new Regex(@"^[0-9]{3,4}$");
            if (!cvvCheck.IsMatch(field))
                
            {
                errorsList.Add("CVV must have 3 or 4 digits.");
                return false;
            }
            return true;
        }
        public static bool ExpirationDateWithDashOrSlash(string field, List<string> errorsList)
        {
            Regex dateFormat = new Regex(@"^(0[1-9]|1[0-2])([\/-]{1})[0-9]{2}$");
            //expiration date in from MM/yyyy
                
            if (!dateFormat.IsMatch(field.Trim()))
            {
                errorsList.Add("Expiration date must be entered in one of the following formats: mm/yy or mm-yy");
                // ^ check date format is valid as "MM/yy"
                return false;
            }
            return true;

        }
        public static bool IsValidExpirationDate(string field, List<string> errorsList)
        {
            Regex dateFormat = new Regex(@"^(0[1-9]|1[0-2])([\/-]{1})[0-9]{2}$");
          
            //expiration date in from MM/yyyy            
             dateFormat.IsMatch(field.Trim());
              string[] dateParts = new string[0];
            if (field.Trim().Contains('/'))
            {
                dateParts = field.Split('/');
            }
            if (field.Trim().Contains('-'))
            {
                dateParts = field.Split('-');
            }
            
            var month = int.Parse(dateParts[0]);
            var shortYear = int.Parse(dateParts[1]);
            var stringYear = ($"20{shortYear}");
            var year = int.Parse(stringYear);
            var lastDateOfExpirationMonth = DateTime.DaysInMonth(year, month); //get actual expiration date
            var cardExpiration = new DateTime(year, month, lastDateOfExpirationMonth, 23, 59, 59);

            //check if expiration date less than today
            if (cardExpiration > DateTime.Now)
            {
                return true;
            }
            errorsList.Add("Expiration date must be a future date"); 
            return false;

        }
        public static bool CardHolderName(string field, List<string> errorsList)
        {
            Regex cardHolder = new Regex(@"^[a-zA-Z\s]*$");

            if (!cardHolder.IsMatch(field.TrimEnd().TrimStart()))
            {
                errorsList.Add("Cardholder name must contain letters only");
                return false;
            }
            return true;
        }


    }
}
