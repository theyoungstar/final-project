using Catalyte.Apparel.Data.Model;
using System.Linq;
using System.Collections.Generic;

namespace Catalyte.Apparel.Data.Filters
{
    /// <summary>
    /// Filter collection for encounter context queries.
    /// </summary>
    public static class EncounterFilters
    {
        public static IQueryable<Encounter> WhereEncounterIdEquals(this IQueryable<Encounter> encounters, int encounterId)
        {
            return encounters.Where(p => p.Id == encounterId).AsQueryable();
        }
     
        public static IQueryable<Encounter> WhereEncountersPatientIdEquals(this IQueryable<Encounter> encounters, int patientId)
        {
            return encounters.Where(p => p.PatientId == patientId).AsQueryable();
        }

    }

}