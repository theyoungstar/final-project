using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Catalyte.Apparel.Data.Model
{

    public class CardValidation
    {
        public virtual List<string> CreditCardValidation(Purchase purchase)
        {
            List<string> errorsList = new List<string>();
            Has14To19Digits(purchase.CardNumber, errorsList);
            CVVHas3To4Digits(purchase.CVV, errorsList);
            IsValidExpirationDate(purchase.Expiration, errorsList);
            CardHolderName(purchase.CardHolder, errorsList);

            return errorsList;
        }
        public bool Has14To19Digits(string field, List<string> errorsList)
        {
            var cardCheck = new Regex(@"^[0-9]{14,19}$");
            if (!cardCheck.IsMatch(field.Trim())) // checks card number has between 14-19 numbers
            {
                errorsList.Add("Card number must contain between 14 to 19 digits.");
                return false;
            }
            return true;
        }
        public bool CVVHas3To4Digits(string field, List<string> errorsList)
        {
            var cvvCheck = new Regex(@"^[0-9]{3,4}$");
            if (!cvvCheck.IsMatch(field) || field.Length < 1)
              //checks if CVV field is empty or has between 3-4 digits  
            {
                errorsList.Add("CVV must have 3 or 4 digits.");
                return false;
            }
            return true;
        }
        public bool IsValidExpirationDate(string field, List<string> errorsList)
        {
            Regex dateFormat = new Regex(@"^(0[1-9]|1[0-2])([\/-]{1})[0-9]{2}$");
            //^ checks if date is in either of the following formats: mm/yy mm-yy
            if (field.Length == 0 || !dateFormat.IsMatch(field.Trim()))
            {
                errorsList.Add("Expiration date must have a month between 1-12, contain no letters or special characters, and be entered in one of the following formats: mm/yy or mm-yy");
                return false;
            }
            
            dateFormat.IsMatch(field.Trim());
            //expiration date in from mm/yy
            string[] dateParts = new string[0];
            //Array to catch date parts
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
            //converts date parts to int
            var stringYear = ($"20{shortYear}");
            //interpolates short year to 4 digit year
            var year = int.Parse(stringYear);
            //converts year string to int
            var lastDateOfExpirationMonth = DateTime.DaysInMonth(year, month); //gets actual expiration date
            var cardExpiration = new DateTime(year, month, lastDateOfExpirationMonth, 23, 59, 59);

            //check if expiration date less than today
            if (cardExpiration < DateTime.Now)
            {
                errorsList.Add("Expiration date must be a future date");
                return false;
            } 
            return true;

        }
        public bool CardHolderName(string field, List<string> errorsList)
        {
            Regex cardHolder = new Regex(@"^[a-zA-Z\s]*$");
            //checks if field has only letters and spaces

            if (!cardHolder.IsMatch(field.TrimEnd().TrimStart()) || field.Length < 1)
            {
                errorsList.Add("Cardholder name must contain letters only");
                return false;
            }
            return true;
        }


    }
}
