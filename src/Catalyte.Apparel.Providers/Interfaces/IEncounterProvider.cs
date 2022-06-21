using Catalyte.Apparel.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Providers.Interfaces
{
    /// <summary>
    /// This interface provides an abstraction layer for product related service methods.
    /// </summary>
    public interface IEncounterProvider
    {
        Task<Encounter> GetEncounterByIdAsync(int encounterId);

        Task<IEnumerable<Encounter>> GetAllEncountersAsync();

        Task<Encounter> UpdateEncounterAsync(int patientId, int encounterID, Encounter updatedEncounter);

        Task<IEnumerable<Encounter>> GetEncountersByPatientIdAsync(int patientId);

        Task<Encounter> CreateEncounterAsync(int patientId, Encounter newEncounter);

        Task<Encounter>GetEncounterByIdByPatientIdAsync(int patientId, int encounterId);

    }
}
