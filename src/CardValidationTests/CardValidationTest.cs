using NUnit.Framework;
using Catalyte.Apparel.Data.Model;
using System.Collections.Generic;

namespace CardValidationTests
{
    
    public class CardValidationTest
    {
        /*[Test]
        public static void Card_Number_Contains_Less_Than_14_Digits()
        {
            var CardValidation = new CardValidation();
            var input = "1234567890123";
            List<string> errors = new List<string>();
            var actual = CardValidation.Has14To19Digits(input, errors);
            Assert.IsFalse(actual);

        }
        [Test]
        public static void Card_Number_Does_Not_Accept_Letters()
        {
            var CardValidation = new CardValidation();
            var input = "123456789ab234";
            List<string> errors = new List<string>();
            var actual = CardValidation.Has14To19Digits(input, errors);
            Assert.IsFalse(actual);

        }
        [Test]
        public static void Card_Number_Does_Not_Accept_Special_Characters()
        {
            var CardValidation = new CardValidation();
            var input = "!@3456789ab234";
            List<string> errors = new List<string>();
            var actual = CardValidation.Has14To19Digits(input, errors);
            Assert.IsFalse(actual);

        }
        [Test]
        public static void Card_Number_Contains_More_Than_19_Digits()
        {
            var CardValidation = new CardValidation();
            var input = "12345678912345678912";
            List<string> errors = new List<string>();
            var actual = CardValidation.Has14To19Digits(input, errors);
            Assert.IsFalse(actual);
        }
        [Test]
        public static void CVV_Number_Contains_Less_Than_3_Digits()
        {
            var CardValidation = new CardValidation();
            var input = "12";
            List<string> errors = new List<string>();
            var actual = CardValidation.CVVHas3To4Digits(input, errors);
            Assert.IsFalse(actual);
        }
        [Test]
        public static void CVV_Number_Contains_More_Than_4_Digits()
        {
            var CardValidation = new CardValidation();
            var input = "12345";
            List<string> errors = new List<string>();
            var actual = CardValidation.CVVHas3To4Digits(input, errors);
            Assert.IsFalse(actual);
        }
        [Test]
        public static void CVV_Number_Does_Not_Accept_Letters()
        {
            var CardValidation = new CardValidation();
            var input = "1r3A";
            List<string> errors = new List<string>();
            var actual = CardValidation.CVVHas3To4Digits(input, errors);
            Assert.IsFalse(actual);
        }
        [Test]
        public static void CVV_Number_Does_Not_Accept_Special_Characters()
        {
            var CardValidation = new CardValidation();
            var input = "1%3&";
            List<string> errors = new List<string>();
            var actual = CardValidation.CVVHas3To4Digits(input, errors);
            Assert.IsFalse(actual);
        }
        [Test]
        public static void Expiration_Date_With_No_Dash_Or_Slash()
        {
            var CardValidation = new CardValidation();
            var input = "1122";
            List<string> errors = new List<string>();
            var actual = CardValidation.ExpirationDateWithDashOrSlash(input, errors);
            Assert.IsFalse(actual);
        }
        [Test]
        public static void Expiration_Date_Does_Not_Accept_Letters()
        {
            var CardValidation = new CardValidation();
            var input = "1a/b2";
            List<string> errors = new List<string>();
            var actual = CardValidation.ExpirationDateWithDashOrSlash(input, errors);
            Assert.IsFalse(actual);
        }
        [Test]
        public static void Expiration_Date_Does_Not_Accept_Special_Character()
        {
            var CardValidation = new CardValidation();
            var input = "@a/b2";
            List<string> errors = new List<string>();
            var actual = CardValidation.ExpirationDateWithDashOrSlash(input, errors);
            Assert.IsFalse(actual);
        }
        [Test]
        public static void Expiration_Date_Does_Not_Accept_Whitespace()
        {
            var CardValidation = new CardValidation();
            var input = " 11 22 ";
            List<string> errors = new List<string>();
            var actual = CardValidation.ExpirationDateWithDashOrSlash(input, errors);
            Assert.IsFalse(actual);
        }*/
        /* [Test]
         public static void Expiration_Date_Has_Not_Passed()
         {
             var CardValidation = new CardValidation();
             var input = "11/21";
             List<string> errors = new List<string>();
             var actual = CardValidation.IsValidExpirationDate(input, errors);
             Assert.IsFalse(actual);
         }*/
        /*  [Test]
          public static void Cardholder_Name_Does_Not_Accept_Numbers()
          {
              var CardValidation = new CardValidation();
              var input = "Jul1us Amak3r";
              List<string> errors = new List<string>();
              var actual = CardValidation.CardHolderName(input, errors);
              Assert.IsFalse(actual);
          }*/
   /*     [Test]
        public static void Cardholder_Name_Does_Not_Accept_Special_Characters()
        {
            var CardValidation = new CardValidation();
            var input = "Jul!us @maker";
            List<string> errors = new List<string>();
            var actual = CardValidation.CardHolderName(input, errors);
            Assert.IsFalse(actual);
        }*/
      






    }
}