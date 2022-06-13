using Catalyte.Apparel.Data.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Data.Interfaces
{
    /// <summary>
    /// This interface provides an abstraction layer for product repository methods.
    /// </summary>
    public interface IPatientRepository
    {
        Task<Patient> GetPatientByIdAsync(int patientId);

        Task<IEnumerable<Patient>> GetAllPatientsAsync();

        Task<Patient> UpdatePatientAsync(Patient patient);

        Task<Patient> CreatePatientAsync(Patient newPatient);

        Task<Patient> GetPatientByEmailAsync(string patientEmail);

        Task<Patient> DeleteMoviesAsync(Patient patient);


    }
}