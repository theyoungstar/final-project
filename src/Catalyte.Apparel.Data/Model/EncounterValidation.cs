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
            HasValidComplaint(encounter.ChiefComplaint, errorsList);
            HasValidPulse(encounter.Pulse, errorsList);
            HasValidSystolic(encounter.Systolic, errorsList);
            HasValidDiastolic(encounter.Diastolic, errorsList);
            HasValidDate(encounter.Date, errorsList);
            


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
                var vCodeCheck = new Regex(@"^([A-Z][\d][A-Z]\s[\d][A-Z][\d])$");
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
                var bCodeCheck = new Regex(@"^([\d]{3,3})\.([\d]{3,3})\.([\d]{3,3})\-([\d]{2,2})$");
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
                var icd10Check = new Regex(@"^[A-Z][0-9][0-9]$");
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
                var totalCostCheck = new Regex(@"^([1-9])([0-9]{1,5})\.[0-9]{2}$");
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
                var copayCheck = new Regex(@"^([1-9])([0-9]{2,2})\.[0-9]{2}$");
                var copayString = copay.ToString();
                if (copayCheck.IsMatch(copayString.Trim()))
                {
                    errorsList.Add("Copay must be a dollar amount ");
                }
            }
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidComplaint(string complaint, List<string> errorsList)
        {
            if ((complaint == null) || complaint.Length < 1 || complaint == "")
            {
                errorsList.Add("A Chief Complaint is required ");
            }
        }
        /// <summary>
        /// This method verifies patient's pulse is in beats per minute
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidPulse(int? pulse, List<string> errorsList)
        {
         /*   if (pulse == default || pulse.ToString() == null)
            {
                errorsList.Add("An age is required ");
            }*/
            if (pulse > 0)
            {
                var ageCheck = new Regex(@"^200|[1-9]?\d$");
                var ageString = pulse.ToString();
                if (!ageCheck.IsMatch(ageString.Trim()))
                {
                    errorsList.Add("Must use numbers for patient's pulse");
                }
            }
            
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidSystolic(int? systolic, List<string> errorsList)
        {
            /*if (systolic == default || systolic.ToString() == null)
            {
                errorsList.Add("A height is required ");
            }*/
            if (systolic > 0)
            {
                var systolicCheck = new Regex(@"^150|[1-9]?\d$");
                var systolicString = systolic.ToString();
                if (!systolicCheck.IsMatch(systolicString.Trim()))
                {
                    errorsList.Add("Systolic must be a number");
                }
            }
           
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidDiastolic(int? diastolic, List<string> errorsList)
        {
            /*if (weight == default || weight.ToString() == null)
            {
                errorsList.Add("A weight is required ");
            }*/
            if (diastolic > 0)
            {
                var diastolicCheck = new Regex(@"^99|[1-9]?\d$");
                var diastolicString = diastolic.ToString();
                if (!diastolicCheck.IsMatch(diastolicString.Trim()))
                {
                    errorsList.Add("Diastolic must be a number");
                }
            }
         
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidDate(string date, List<string> errorsList)
        {
            if ((date == null) || date.Length < 1)
            {
                errorsList.Add("Date is required ");
            }
            else if (date.Length > 0)
            {
                var dateCheck = new Regex(@"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$");
                if (!dateCheck.IsMatch(date.Trim()))
                {
                    errorsList.Add("Date must be in a YYYY-MM-DD format");
                }
            }
        }
      
    }
}