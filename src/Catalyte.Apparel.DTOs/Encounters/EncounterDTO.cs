using Catalyte.Apparel.DTOs.Patients;
using System;

namespace Catalyte.Apparel.DTOs.Encounters
{
    /// <summary>
    /// Describes a data transfer object for a patient.
    /// </summary>
    public class EncounterDTO
    {

        public int Id { get; set; }
        public int PatientId  { get; set; }
        public string Notes { get; set; }
        public string VisitCode { get; set; }
        public string Provider { get; set; }
        public string BillingCode { get; set; }
        public string ICD10 { get; set; }
        public double TotalCost { get; set; }
        public double Copay { get; set; }
        public string ChiefComplaint { get; set; }
        public int Pulse { get; set; }
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
        public string Date { get; set; }
    }
}