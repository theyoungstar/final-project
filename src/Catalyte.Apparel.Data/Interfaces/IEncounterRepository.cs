using Catalyte.Apparel.Data.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Data.Interfaces
{
    /// <summary>
    /// This interface provides an abstraction layer for product repository methods.
    /// </summary>
    public interface IEncounterRepository
    {
        Task<Encounter> AddEncounterAsync(Encounter encounter);

        Task<IEnumerable<Encounter>> GetAllEncountersAsync();

        Task<Encounter> UpdateEncounterAsync(Encounter encounter);

        Task<Encounter> GetEncounterByIdAsync(int Id);

        Task<Encounter> GetPatientEncounterByIdAsync(int patientId, int encounterId);

        Task<IEnumerable<Encounter>> GetAllEncountersByPatientIdAsync(int patientId);

        Task<Encounter> CreateEncounterAsync(Encounter newEncounter);



    }
}