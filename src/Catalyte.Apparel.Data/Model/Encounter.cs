using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
        public int Copay { get; set; }
        public string ChiefComplaint { get; set; }
        public int Pulse { get; set; }
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
        public string Date { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        public static IEqualityComparer<Encounter> EncounterComparer { get; } = new EncounterEqualityComparer();

        private sealed class EncounterEqualityComparer : IEqualityComparer<Encounter>
        {
            public bool Equals(Encounter x, Encounter y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.PatientId == y.PatientId &&
                    x.Notes == y.Notes &&
                    x.VisitCode == y.VisitCode &&
                    x.Provider == y.Provider &&
                    x.BillingCode == y.BillingCode &&
                    x.ICD10 == y.ICD10 &&
                    x.TotalCost == y.TotalCost &&
                    x.Copay == y.Copay &&
                    x.ChiefComplaint == y.ChiefComplaint &&
                    x.Pulse == y.Pulse &&
                    x.Systolic == y.Systolic &&
                    x.Diastolic == y.Diastolic &&
                    x.Date == y.Date;
            }

            public int GetHashCode(Encounter obj)
            {
                var hashCode = new HashCode();
                hashCode.Add(obj.PatientId);
                hashCode.Add(obj.Notes);
                hashCode.Add(obj.VisitCode);
                hashCode.Add(obj.Provider);
                hashCode.Add(obj.BillingCode);
                hashCode.Add(obj.ICD10);
                hashCode.Add(obj.TotalCost);
                hashCode.Add(obj.Copay);
                hashCode.Add(obj.ChiefComplaint);
                hashCode.Add(obj.Pulse);
                hashCode.Add(obj.Systolic);
                hashCode.Add(obj.Diastolic);
                hashCode.Add(obj.Date);


                return hashCode.ToHashCode();
            }
        }

    }

}