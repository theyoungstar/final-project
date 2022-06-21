using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;


namespace Catalyte.Apparel.Data.Model
{

    public class PatientValidation
    {
        /// <summary>
        /// This is the Patient validation. It validates that the Patient fields meet required specifications before
        /// the patient goes to the Database
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
        /// This method verifies the last name is not blank and only accepts letters, spaces, hyphens, periods, and
        /// apostrophes
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidFirstName(string firstName, List<string> errorsList)
        {
            if ((firstName == null) || firstName.Length < 1 || firstName == "")
            {
                errorsList.Add("First Name is required ");
            } else if 
                (firstName.Length > 0)
            {
                var nameCheck = new Regex(@"^[A-Za-z][A-Za-z'-]+([\ A-Za-z][A-Za-z'-]+)*$");
                if (!nameCheck.IsMatch(firstName))
                {
                    errorsList.Add("First name accepts letters, hyphens, apostrophes, and periods ");
                }
            }

        }
        /// <summary>
        /// This method verifies the last name is not blank and only accepts letters, spaces, hyphens, periods, and
        /// apostrophes
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
            else if
               (lastName.Length > 0)
            {
                var nameCheck = new Regex(@"^[A-Za-z][A-Za-z'-]+([ A-Za-z][A-Za-z'-]+)*$");
                if (!nameCheck.IsMatch(lastName))
                {
                    errorsList.Add("Last name accepts letters, hyphens, apostrophes, and periods ");
                }
            }
        }
        /// <summary>
        /// This method verifies the ssn is in a 123-45-6789 format
        /// </summary>
        /// <param name="ssn"></param>
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
                if (!ssnCheck.IsMatch(ssn))
                {
                    errorsList.Add("Social Security Number must be in this format: 012-34-5678 ");
                }
            }
        }
        /// <summary>
        /// This method verifies the email address is not blank and is in a valid format
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
                if (!emailCheck.IsMatch(email))
                {
                    errorsList.Add("Email Address must be in this format: x@x.x ");
                }
            }
        }
        /// <summary>
        /// This method verifies the street is not blank and takes only letters and numbers 
        /// </summary>
        /// <param name="street"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidStreet(string street, List<string> errorsList)
        {
            if ((street == null) || street.Length < 1 || street == "")
            {
                errorsList.Add("Street Address is required ");
            }
            else if
               (street.Length > 0)
            {
                var streetCheck = new Regex(@"^[a-zA-z\s\d]+\w$");
                if (!streetCheck.IsMatch(street))
                {
                    errorsList.Add("Street accepts letters, numbers and spaces ");
                }
            }
        }
        /// <summary>
        /// This method verifies the city is not blank ant it only takes letters, spaces, hyphen, periods and apostrophes
        /// </summary>
        /// <param name="city"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidCity(string city, List<string> errorsList)
        {
            if ((city == null) || city.Length < 1 || city == "")
            {
                errorsList.Add("A City is required for Address ");
            }
            else if
             (city.Length > 0)
            {
                var cityCheck = new Regex(@"^[s\a-zA-z'.-]+\w$");
                if (!cityCheck.IsMatch(city))
                {
                    errorsList.Add("City accepts letters, hyphen, periods, and aposthrophes ");
                }
            }

        }
        /// <summary>
        /// This method verifies the state is required and can only be in 2 letter abbreviation
        /// </summary>
        /// <param name="state"></param>
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
                if (!stateCheck.IsMatch(state))
                {
                    errorsList.Add("Must be a valid State Abbreviation, ex. SC");
                }
            }
        }
        /// <summary>
        /// This method verifies the postal code is required and must be in one of the following formats:
        /// 12345 or 12345-6789
        /// </summary>
        /// <param name="pCode"></param>
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
                if (!pCodeCheck.IsMatch(pCode))
                {
                    errorsList.Add("Zip Code must be in one of the following formats : 12345 or 12345-1234");
                }
            }
        }
        /// <summary>
        /// This method verifies that age is required and takes no decimals
        /// </summary>
        /// <param name="Age"></param>
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
                var ageCheck = new Regex(@"^\d{1,8}$");
                var ageString = age.ToString();
                if (!ageCheck.IsMatch(ageString))
                {
                    errorsList.Add("Age must be a number greater than zero");
                }
            }
            else
            {
                errorsList.Add("Age must be greater than 0");
            }
        }
        /// <summary>
        /// This method verifies that height is required and takes no decimals
        /// </summary>
        /// <param name="height"></param>
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
                var heightCheck = new Regex(@"^\d{1,8}$");
                var heightString = height.ToString();
                if (!heightCheck.IsMatch(heightString))
                {
                    errorsList.Add("Height must be greater than zero");
                }
            }
        }
        /// <summary>
        /// This method verifies that weight is required and takes no decimals
        /// </summary>
        /// <param name="weight"></param>
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
                var weightCheck = new Regex(@"^\d{1,8}$");
                var weightString = weight.ToString();
                if (!weightCheck.IsMatch(weightString))
                {
                    errorsList.Add("Weight must be geater than zero");
                }
            }
        }
        /// <summary>
        /// This method verifies insurance os required and takes letters spaces and one number
        /// </summary>
        /// <param name="insurance"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidInsurance(string ins, List<string> errorsList)
        {
            if ((ins == null) || ins.Length < 1)
            {
                errorsList.Add("Insurance is required ");
            }
            else if (ins.Length > 0)
            {
                var insCheck = new Regex(@"^[A-Za-z][A-Za-z'-]+([ A-Za-z][A-Za-z'-]+)*$");
                if (!insCheck.IsMatch(ins))
                {
                    errorsList.Add("Insurance takes letters, spaces, periods, and aposthrophes");
                }
            }
        }
        /// <summary>
        /// This method verifies gender cannot be left blank and must be spell Male, Female, or Other specifically
        /// </summary>
        /// <param name="gender"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidGender(string gender, List<string> errorsList)
        {
            if ((gender == null) || gender.Length < 1)
            {
                errorsList.Add("Gender is required ");
            }
            else if (gender.Length > 0)
            {
                var genderCheck = new Regex(@"^\A[M][a][l][e]\z|\A[F][e][m][a][l][e]\z|\A[O][t][h][e][r]\z$");
                if (!genderCheck.IsMatch(gender))
                {
                    errorsList.Add("Gender accepts Male, Female, or Other");
                }
        }   }
    }
}