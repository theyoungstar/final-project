using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;


namespace Catalyte.Apparel.Data.Model
{

    public class EncounterValidation
    {
        /// <summary>
        /// This is the Credit Card validation. It validates that the card fields meet required specifications before
        /// the purchase goes to the Database
        /// </summary>
        /// <param name="purchase"></param>
        /// <returns>errorsList</returns>
        public virtual List<string> ValidationForEncounter(Encounter encounter)
        {
            List<string> errorsList = new List<string>();
            HasValidPatientId(encounter.PatientId, errorsList);
            HasValidVisitCode(encounter.VisitCode, errorsList);
            HasValidProvider(encounter.Provider, errorsList);
            HasValidBillingCode(encounter.BillingCode, errorsList);
            HasValidICD10(encounter.ICD10, errorsList);
            HasValidTotalCost(encounter.TotalCost, errorsList);
            HasValidCopay(encounter.Copay, errorsList);
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
        public void HasValidPatientId(int patientId, List<string> errorsList)
        {
            if ((patientId == default) || patientId.ToString().Length < 1 || patientId == 0 || patientId.ToString() == "")
            {
                errorsList.Add("Patient Id is required ");
            }
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidVisitCode(string visitCode, List<string> errorsList)
        {
            if ((visitCode == null) || visitCode.Length < 1 || visitCode == "")
            {
                errorsList.Add("A Visit Code is required ");
            }
            else if (visitCode.Length > 0)
            {
                var vCodeCheck = new Regex(@"/^([A-Z][\d][A-Z]\s[\d][A-Z][\d])$/");
                if (!vCodeCheck.IsMatch(visitCode.Trim().ToUpper()))
                    errorsList.Add("Visit Code must be in this format: Q4W 5T3");
            }
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidProvider(string provider, List<string> errorsList)
        {
            if ((provider == null) || provider.Length < 1 || provider == "")
            {
                errorsList.Add("A Provider is required ");
            }
           
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidBillingCode(string billingCode , List<string> errorsList)
        {
            if ((billingCode == null) || billingCode.Length < 1 || billingCode == "")
            {
                errorsList.Add("Billing Code is required ");
            }
            else if (billingCode.Length > 0)
            {
                var bCodeCheck = new Regex(@"/^([\d]{3,3})\.([\d]{3,3})\.([\d]{3,3})\-([\d]{2,2})$/");
                if (!bCodeCheck.IsMatch(billingCode.Trim()))
                {
                    errorsList.Add("Billing Code must be entered in the following format: 123.456.789-12");
                }
            }
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidICD10(string icd10, List<string> errorsList)
        {
            if ((icd10 == null) || icd10.Length < 1 || icd10 == "")
            {
                errorsList.Add("ICD10 Code is required ");
            }
            else if (icd10.Length > 0)
            {
                var icd10Check = new Regex(@"/^[A-Z][0-9][0-9]$/");
                if (!icd10Check.IsMatch(icd10.Trim().ToUpper()))
                {
                    errorsList.Add("ICD10 Code must be entered in the following format: A23 ");
                }
            }
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidTotalCost(double totalCost, List<string> errorsList)
        {
            if ((totalCost == default) || totalCost.ToString().Length < 1 || totalCost.ToString() == "" || totalCost.ToString() == null)
            {
                errorsList.Add("A total cost is required ");
            }
            else if (totalCost.ToString().Length > 0)
            {
                var totalCostCheck = new Regex(@"/^([1-9])([0-9]{4,4})\.[0-9]{2,2}$/");
                var totalCostString = totalCost.ToString();
                if (!totalCostCheck.IsMatch(totalCostString.Trim()))
                {
                    errorsList.Add("Total cost must be a dollar amount ");
                }
            }
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidCopay(double copay, List<string> errorsList)
        {
            if ((copay == default) || copay.ToString().Length < 1 || copay.ToString() == "" || copay.ToString() == null)
            {
                errorsList.Add("Copay is required ");
            }
            else if (copay.ToString().Length > 0)
            {
                var copayCheck = new Regex(@"/^([1-9])([0-9]{2,2})\.[0-9]{2,2}$/");
                var copayString = copay.ToString();
                if (!copayCheck.IsMatch(copayString.Trim()))
                {
                    errorsList.Add("Copayt must be a dollar amount ");
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