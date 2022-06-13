using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;


namespace Catalyte.Apparel.Data.Model
{

    public class PatientValidation
    {
        /// <summary>
        /// This is the Credit Card validation. It validates that the card fields meet required specifications before
        /// the purchase goes to the Database
        /// </summary>
        /// <param name="purchase"></param>
        /// <returns>errorsList</returns>
        public virtual List<string> ValidationForPatient(Patient patient)
        {
            List<string> errorsList = new List<string>();
            HasValidFirstName(patient.FirstName, errorsList);
            HasValidLastName(patient.LastName, errorsList);
            HasValidSsn(patient.Ssn, errorsList);
            HasValidEmail(patient.Email, errorsList);
            HasValidStreet(patient.Street, errorsList);
            HasValidCity(patient.City, errorsList);
            HasValidState(patient.State, errorsList);
            HasValidPostalCode(patient.Postal, errorsList);
            HasValidAge(patient.Age, errorsList);
            HasValidHeight(patient.Height, errorsList);
            HasValidWeight(patient.Weight, errorsList);
            HasValidInsurance(patient.Insurance, errorsList);
            HasValidGender(patient.Gender, errorsList);

            return errorsList;
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidFirstName(string firstName, List<string> errorsList)
        {
            if ((firstName == null) || firstName.Length < 1 || firstName == "")
            {
                errorsList.Add("First Name is required ");
            }
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidLastName(string lastName, List<string> errorsList)
        {
            if ((lastName == null) || lastName.Length < 1 || lastName == "")
            {
                errorsList.Add("Last Name is required ");
            }
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidSsn(string ssn, List<string> errorsList)
        {
            if ((ssn == null) || ssn.Length < 1 || ssn == "")
            {
                errorsList.Add("Social Security Number is required ");
            }
            else if (ssn.Length > 0)
            {
                var ssnCheck = new Regex(@"^\d{3}\-?\d{2}\-?\d{4}$");
                if (!ssnCheck.IsMatch(ssn.Trim()))
                {
                    errorsList.Add("Social Security Number must be in this format: 012-34-5678 ");
                }
            }
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidEmail(string email, List<string> errorsList)
        {
            if ((email == null) || email.Length < 1 || email == "")
            {
                errorsList.Add("Email Address is required ");
            }
            else if (email.Length > 0)
            {
                var emailCheck = new Regex(@"^\w+@([a-z]+\.)+[a-z]+$");
                if (!emailCheck.IsMatch(email.Trim()))
                {
                    errorsList.Add("Email Address must be in this format: x@x.x ");
                }
            }
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidStreet(string street, List<string> errorsList)
        {
            if ((street == null) || street.Length < 1 || street == "")
            {
                errorsList.Add("Street Address is required ");
            }
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidCity(string city, List<string> errorsList)
        {
            if ((city == null) || city.Length < 1 || city == "")
            {
                errorsList.Add("A City is required for Address ");
            }

        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidState(string state, List<string> errorsList)
        {
            if ((state == null) || state.Length < 1 || state == "")
            {
                errorsList.Add("A State is required for Address ");
            }
            else if (state.Length > 0 && state.Length < 3)
            {
                var stateCheck = new Regex(@"^(A[KLRZ]|C[AOT]|D[CE]|FL|GA|HI|I[ADLN]|K[SY]|LA|M[ADEINOST]|N[CDEHJMVY]|O[HKR]|PA|RI|S[CD]|T[NX]|UT|V[AT]|W[AIVY])$");
                if (!stateCheck.IsMatch(state.ToUpper().Trim()))
                {
                    errorsList.Add("Must be a valid State Abbreviation, ex. SC");
                }
            }
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidPostalCode(string pCode, List<string> errorsList)
        {
            if ((pCode == null) || pCode.Length < 1 || pCode == "")
            {
                errorsList.Add("A Postal Code is required for Address ");
            }
            else if (pCode.Length > 0 && pCode.Length < 3)
            {
                var pCodeCheck = new Regex(@"^[0-9]{5}(?:-[0-9]{4})?$");
                if (!pCodeCheck.IsMatch(pCode.ToUpper().Trim()))
                {
                    errorsList.Add("Postal Code must be in one of the following formats : 12345 or 12345-1234");
                }
            }
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidAge(int age, List<string> errorsList)
        {
            if (age == default || age.ToString() == null)
            {
                errorsList.Add("An age is required ");
            }
            else if (age > 0)
            {
                var ageCheck = new Regex(@"^150|[1-9]?\d$");
                var ageString = age.ToString();
                if (!ageCheck.IsMatch(ageString.Trim()))
                {
                    errorsList.Add("Postal Code must be in one of the following formats : 12345 or 12345-1234");
                }
            }
            else
            {
                errorsList.Add("Age must be greater than 0");
            }
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidHeight(int height, List<string> errorsList)
        {
            if (height == default || height.ToString() == null)
            {
                errorsList.Add("A height is required ");
            }
            else if (height > 0)
            {
                var heightCheck = new Regex(@"^99|[1-9]?\d$");
                var heightString = height.ToString();
                if (!heightCheck.IsMatch(heightString.Trim()))
                {
                    errorsList.Add("Height must be at least 21 inches");
                }
            }
            else
            {
                errorsList.Add("height must be greater than 0");
            }
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidWeight(int weight, List<string> errorsList)
        {
            if (weight == default || weight.ToString() == null)
            {
                errorsList.Add("A weight is required ");
            }
            else if (weight > 0)
            {
                var weightCheck = new Regex(@"^99|[1-9]?\d$");
                var weightString = weight.ToString();
                if (!weightCheck.IsMatch(weightString.Trim()))
                {
                    errorsList.Add("Weight must be at least 21 inches");
                }
            }
            else
            {
                errorsList.Add("Weight must be greater than 0");
            }
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidInsurance(string ins, List<string> errorsList)
        {
            if ((ins == null) || ins.Length < 1)
            {
                errorsList.Add("Insurance is required ");
            }
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidGender(string gender, List<string> errorsList)
        {
            if ((gender == null) || gender.Length < 1)
            {
                errorsList.Add("Gender is required ");
            }
        }
    }
}