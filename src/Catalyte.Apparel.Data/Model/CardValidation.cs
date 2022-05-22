using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;


namespace Catalyte.Apparel.Data.Model
{

    public class CardValidation
    {
        /// <summary>
        /// This is the Credit Card validation. It validates that the card fields meet required specifications before
        /// the purchase goes to the Database
        /// </summary>
        /// <param name="purchase"></param>
        /// <returns>errorsList</returns>
        public virtual List<string> CreditCardValidation(Purchase purchase)
        {
            List<string> errorsList = new List<string>();
            Has14To19Digits(purchase.CardNumber, errorsList);
            CVVHas3To4Digits(purchase.CVV, errorsList);
            IsValidExpirationDate(purchase.Expiration, errorsList);
            CardHolderName(purchase.CardHolder, errorsList);

            return errorsList;
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void Has14To19Digits(string field, List<string> errorsList)
        {
            if ((field == null) || field.Length < 1)
            {
                errorsList.Add("Card number is required ");
            }
            else if (field.Length > 0)
            {
                var cardCheck = new Regex(@"^[0-9]{14,19}$");
                if (!cardCheck.IsMatch(field.Trim()))
                {
                    errorsList.Add("Card number must contain between 14 to 19 digits ");
                }
            }
        }
        /// <summary>
        /// This method verifies that the CVV has 3-4 digits, no letters, or special characters.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="errorsList"></param>
        /// <returns>boolean</returns>
        public void CVVHas3To4Digits(string field, List<string> errorsList)
        {
            if ((field == null) || field.Length < 1)
            {
                errorsList.Add(" CVV is required ");
            }
            else if (field.Length > 0)
            {
                var cvvCheck = new Regex(@"^[0-9]{3,4}$");
                if (!cvvCheck.IsMatch(field.Trim()))
                {
                    errorsList.Add(" CVV must have 3 or 4 digits ");
                }
            }
        }
        /// <summary>
        /// This method verifies that the Expiration date is not empty, takes only numbers, a slash, or a dash, is in the
        /// mm/yy or mm-yy format, and is a future date. 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="errorsList"></param>
        /// <returns>boolean</returns>
        public void IsValidExpirationDate(string field, List<string> errorsList)
        {
            if ((field == null) || field == "")
            {
                errorsList.Add(" Expiration date is required ");
            }
            else if (field.Length > 0)
            {
                Regex dateFormat = new Regex(@"^(0[1-9]|1[0-2])([\/-]{1})[0-9]{2}$");
                if (!dateFormat.IsMatch(field))
                {
                    errorsList.Add("Expiration date must have a month between 1-12, " +
                "contain no letters or special characters, and be entered in one of " +
                "the following formats: mm/yy or mm-yy ");
                }
                else if (dateFormat.IsMatch(field.Trim()))
                {
                    string[] dateParts = new string[0];
                    if (field.Trim().Contains('/'))
                    {
                        dateParts = field.Split('/');
                    }
                    else if (field.Trim().Contains('-'))
                    {
                        dateParts = field.Split('-');
                        var month = int.Parse(dateParts[0]);
                        var shortYear = int.Parse(dateParts[1]);
                        var stringYear = $"20{shortYear}";
                        var year = int.Parse(stringYear);

                        var lastDateOfExpirationMonth = DateTime.DaysInMonth(year, month);
                        var cardExpiration = new DateTime(year, month, lastDateOfExpirationMonth, 23, 59, 59);

                    
                        if (cardExpiration < DateTime.Now)
                        {
                            errorsList.Add(" Expiration date must be a future date");

                        }
                    }

                }

            }


        }
        /// <summary>
        /// This method verifies that the Cardholder name doesn't contain numbers and takes only letters, spaces, 
        /// dashes, and apostrophies. 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="errorsList"></param>
        /// <returns>boolean</returns>
        private static void CardHolderName(string field, List<string> errorsList)
        {
            if ((field == null) || field.Length < 1)
            {
                errorsList.Add(" Card Holder name required");
            }
                else if(field.Length > 1)
                {
                Regex cardHolder = new Regex(@"^[a-zA-Z-' ]+[ ]{1}[a-zA-Z-' ]{2,}([^0-9]*)$");
                    if (!cardHolder.IsMatch(field.TrimEnd().TrimStart()))
                    {
                    errorsList.Add(" Cardholder name can only contain spaces, apostrohes, and letters");
                    }

                }
        }

    }
}
