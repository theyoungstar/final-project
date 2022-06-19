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
                if (!vCodeCheck.IsMatch(visitCode))
                    errorsList.Add("Visit Code must be in this format: Q4W 5T3 with a space in between and no excess spaces ");
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
                errorsList.Add("A Provider is required with no excess spaces ");
            }
           
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidBillingCode(string billingCode, List<string> errorsList)
        {
            
            if ((billingCode == null) || billingCode.Length < 1 || billingCode == "")
            {
                errorsList.Add("Billing Code is required ");
            }
            else if (billingCode.Length > 0)
            {
                var bCodeCheck = new Regex(@"^([\d]{3,3})\.([\d]{3,3})\.([\d]{3,3})\-([\d]{2,2})$");
                if (!bCodeCheck.IsMatch(billingCode))
                {
                    errorsList.Add(" Billing Code must be entered in the following format: 123.456.789-12 with no excess spaces");
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
                errorsList.Add(" ICD10 Code is required");
            }
            else if (icd10.Length > 0)
            {
                var icd10Check = new Regex(@"^[A-Z][0-9][0-9]$");
                if (!icd10Check.IsMatch(icd10))
                {
                    errorsList.Add(" ICD10 Code must be entered in the following format: A23 with no excess spaces");
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
            if (totalCost.ToString().Length < 1 || totalCost.ToString() == "" || totalCost.ToString() == null)
            {
                errorsList.Add("A total cost is required ");
            }
            else if (totalCost.ToString().Length > 0)
            {
                var totalCostCheck = new Regex(@"^(\d{1,9}(\,\d{3})*|(\d+))(\.\d{2})?|(\.\d{2})?$");
                var totalCostString = totalCost.ToString();
                if (!totalCostCheck.IsMatch(totalCostString))
                {
                    errorsList.Add(" Total cost must be a dollar amount with no excess spaces");
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
            if (copay.ToString().Length < 1 || copay.ToString() == "" || copay.ToString() == null)
            {
                errorsList.Add("Copay is required ");
            }
            else if (copay.ToString().Length > 0)
            {
                var copayCheck = new Regex(@"^(\d{1,9}(\,\d{3})*|(\d+))(\.\d{2})?|(\.\d{2})?$");
                var copayString = copay.ToString();
                if (!copayCheck.IsMatch(copayString))
                {
                    errorsList.Add(" Copay must be a dollar amount with no excess spaces");
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
        public void HasValidPulse(string pulse, List<string> errorsList)
        {
            if (pulse == null || pulse == "")
            {
                pulse = "0";
            }
            else if(pulse.Length > 0)
            {
                var pulseCheck = new Regex(@"^[1-9]\d+$");
                if (!pulseCheck.IsMatch(pulse))
                {
                    errorsList.Add("Must use numbers for patient's pulse with no excess spaces");
                }
            }    
        }

        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidSystolic(string systolic, List<string> errorsList)
        {
            if (systolic == null || systolic == "")
            {
                systolic = "0";
            }
            else if (systolic.Length > 0)
            {
                var systolicCheck = new Regex(@"^[1-9]\d+$");
                if (!systolicCheck.IsMatch(systolic))
                {
                    errorsList.Add("Must use numbers for systolic with no excess spaces");
                }
            }
        }
        /// <summary>
        /// This method verifies the Credit card number is between 14-19 digits with no letters, special characters or spaces 
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidDiastolic(string diastolic, List<string> errorsList)
        {
            if (diastolic == null || diastolic == "")
            {
                diastolic = "0";
            }
            else if (diastolic.Length > 0)
            {
                var diastolicCheck = new Regex(@"^[1-9]\d+$");
                if (!diastolicCheck.IsMatch(diastolic))
                {
                    errorsList.Add("Must use numbers for diastolic with no excess spaces");
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
                var dateCheck = new Regex(@"^^[0-9]{4}-(((0[13578]|(10|12))-(0[1-9]|[1-2][0-9]|3[0-1]))|(02-(0[1-9]|[1-2][0-9]))|((0[469]|11)-(0[1-9]|[1-2][0-9]|30)))$");
                if (!dateCheck.IsMatch(date))
                {
                    errorsList.Add("Date must be in a YYYY-MM-DD format with no excess spaces");
                }
            }
        }
      
    }
}