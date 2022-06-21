using Catalyte.Apparel.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Providers.Interfaces
{
    /// <summary>
    /// This interface provides an abstraction layer for product related service methods.
    /// </summary>
    public interface IPatientProvider
    {
        Task<Patient> GetPatientByIdAsync(int patientId);

        Task<IEnumerable<Patient>> GetAllPatientsAsync();

        Task<Patient> UpdatePatientAsync(int patientId, Patient updatedPatient);

        Task<Patient> CreatePatientAsync(Patient patient);

        Task<Patient> DeletePatientAsync(int patientId);

        Task<Patient> GetPatientByEmailAsync(string patientEmail);

    }
}
