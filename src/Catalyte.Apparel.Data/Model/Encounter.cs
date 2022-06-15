using Newtonsoft.Json;
using System;

namespace Catalyte.Apparel.Data.Model
{
    /// <summary>
    /// This class is a representation of a hospital patient.
    /// </summary>
    public class Encounter : BaseEntity
    {
        public int PatientId { get; set; }
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

        public Patient Patient { get; set; }

    }

}