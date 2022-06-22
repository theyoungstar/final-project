using Catalyte.Apparel.Data.Context;
using Catalyte.Apparel.Data.Filters;
using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Catalyte.Apparel.Data.Repositories
{
    /// <summary>
    /// This class handles methods for making requests to the patient repository.
    /// </summary>

    public class PatientRepository : IPatientRepository
    {
        private readonly IApparelCtx _ctx;

        public PatientRepository(IApparelCtx ctx)
        {
            _ctx = ctx;
        }

        public async Task<Patient> GetPatientByIdAsync(int patientId)
        {
            return await _ctx.Patients
                .AsNoTracking()
                .WherePatientIdEquals(patientId)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _ctx.Patients
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .ToListAsync();
        }
        public async Task<Patient> UpdatePatientAsync(Patient patient)
        {
            _ctx.Patients.Update(patient);
            await _ctx.SaveChangesAsync();
            return patient;

        }

        public async Task<Patient> CreatePatientAsync(Patient newPatient)
        {
            _ctx.Patients.Add(newPatient);
            await _ctx.SaveChangesAsync();

            return newPatient;
        }
        public async Task<Patient> GetPatientByEmailAsync(string patientEmail)
        {
            return await _ctx.Patients
                .AsNoTracking()
                .WherePatientEmailEquals(patientEmail)
                .SingleOrDefaultAsync();
        }
        public async Task<Patient> DeletePatientAsync(Patient patient)
        {
            _ctx.Patients.Remove(patient);
            await _ctx.SaveChangesAsync();
            return patient;

        }
        public async Task<IEnumerable<Encounter>> GetPatientEncounterByIdAsync(int patientId, int encounterId)
        {
            return await _ctx.Encounters
                .AsNoTracking()
                .WherePatientHasEncounterEquals(patientId, encounterId)
                .OrderBy(p => p.Id)
                .ToListAsync();
        }
        
    }
}