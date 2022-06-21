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
        /// This method verifies the patient Id is required and only .
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidPatientId(int patientId, List<string> errorsList)
        {

            if (patientId == default || patientId.ToString().Length < 1 || patientId.ToString() == "")
            {
                errorsList.Add("A Patient Id is required ");
            }
            else if (patientId.ToString().Length > 0)
            {
                var patientIdCheck = new Regex(@"^([1-9][0-9]{0,3})$");
                var patientIdString = patientId.ToString();
                if (!patientIdCheck.IsMatch(patientIdString))
                    errorsList.Add("Patient Id is rquired and must be a number greater than zero ");
            }
        }

        /// <summary>
        /// This method verifies the visitCode is not left blank and is formatted like this: LDL DLD.
        /// </summary>
        /// <param name="vistCode"></param>
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
        /// This method verifies the provider is required and takes only letters, hyphens, periods, and apostrophes
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="errorsList"></param>
        /// <returns>the boolean true</returns>
        public void HasValidProvider(string provider, List<string> errorsList)
        {
            if ((provider == null) || provider.Length < 1 || provider == "")
            {
                errorsList.Add("A Provider is required with no excess spaces ");
            }
            else if (provider.Length > 0)
            {
                var providerCheck = new Regex(@"^[A-Za-z][A-Za-z'-\.]+([ A-Za-z][A-Za-z'-]+)*$");
                if (!providerCheck.IsMatch(provider))
                    errorsList.Add("Provider takes only letters, hyphens, periods, and apostrophes ");
            }

        }
        /// <summary>
        /// This method verifies the billingCode is required and must be in this format: 123.456.789-12
        /// </summary>
        /// <param name="billingCode"></param>
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
        /// This method verifies the icd10 is required and must be in this format: A44
        /// </summary>
        /// <param name="icd10"></param>
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
        /// This method verifies the totalCost is required and must be in this format: DD.DD 
        /// </summary>
        /// <param name="totalCost"></param>
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
        /// This method verifies the copay is required and must be in this format: DD.DD 
        /// </summary>
        /// <param name="copay"></param>
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
        /// This method verifies the chiefComplaint is required
        /// </summary>
        /// <param name="complaint"></param>
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
        /// This method verifies patient's pulse is a number greater than zero if not null
        /// </summary>
        /// <param name="pulse"></param>
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
                var pulseCheck = new Regex(@"^([1-9][0-9]{0,2})$");
                if (!pulseCheck.IsMatch(pulse))
                {
                    errorsList.Add("Must be a number greater than zero with no excess spaces");
                }
            }    
        }

        /// <summary>
        /// This method verifies patient's systolic is a number greater than zero if not null
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
                var systolicCheck = new Regex(@"^([1-9][0-9]{0,2})$");
                if (!systolicCheck.IsMatch(systolic))
                {
                    errorsList.Add("Systolic must be a number greater than zero with no excess spaces");
                }
            }
        }
        /// <summary>
        /// This method verifies patient's diastolic is a number greater than zero if not null
        /// </summary>
        /// <param name="diastolic"></param>
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
                var diastolicCheck = new Regex(@"^([1-9][0-9]{0,2})$");
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
                var dateCheck = new Regex(@"^[0-9]{4}-(((0[13578]|(10|12))-(0[1-9]|[1-2][0-9]|3[0-1]))|(02-(0[1-9]|[1-2][0-9]))|((0[469]|11)-(0[1-9]|[1-2][0-9]|30)))$");
                if (!dateCheck.IsMatch(date))
                {
                    errorsList.Add("Date must be in a YYYY-MM-DD format with no excess spaces");
                }
            }
        }
      
    }
}