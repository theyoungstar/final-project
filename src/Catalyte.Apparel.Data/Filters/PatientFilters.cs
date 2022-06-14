using Catalyte.Apparel.Data.Model;
using System.Linq;
using System.Collections.Generic;

namespace Catalyte.Apparel.Data.Filters
{
    /// <summary>
    /// Filter collection for patient context queries.
    /// </summary>
    public static class PatientFilters
    {
        public static IQueryable<Patient> WherePatientIdEquals(this IQueryable<Patient> patients, int patientId)
        {
            return patients.Where(p => p.Id == patientId).AsQueryable();
        }
        public static IQueryable<Patient> WherePatientEmailEquals(this IQueryable<Patient> patients, string email)
        {
            return patients.Where(p => p.Email == email).AsQueryable();
        }
        public static IQueryable<Patient> WhereAllPatientsEquals(this IQueryable<Patient> patients, string firstName)
        {
            return patients.Where(p => p.FirstName == firstName).AsQueryable();
        }
        public static IQueryable<Encounter> WherePatientHasEncounterEquals(this IQueryable<Encounter> encounters, int patientId, int encounterId)
        {
            return encounters.Where(e => e.PatientId == patientId && e.Id == encounterId);
        }

    }

}