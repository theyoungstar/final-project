using Catalyte.Apparel.DTOs.Encounters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Catalyte.Apparel.DTOs.Patients
{
    /// <summary>
    /// Describes a data transfer object for a patient.
    /// </summary>
    public class PatientDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Ssn { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Postal { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Insurance { get; set; }
        public string Gender { get; set; }

    }
}