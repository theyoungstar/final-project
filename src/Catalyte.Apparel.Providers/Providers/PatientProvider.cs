using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.Providers.Interfaces;
using Catalyte.Apparel.Utilities.HttpResponseExceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;


namespace Catalyte.Apparel.Providers.Providers
{
    /// <summary>
    /// This class provides the implementation of the IPurchaseProvider interface, providing service methods for purchases.
    /// </summary>
    public class PatientProvider : IPatientProvider
    {
        private readonly ILogger<PatientProvider> _logger;
        private readonly IPatientRepository _patientRepository;
        private readonly PatientValidation _patientValidation;
        private readonly IEncounterRepository _encounterRepository;


        public PatientProvider(IPatientRepository patientRepository,  ILogger<PatientProvider> logger, PatientValidation patientValidation, IEncounterRepository encounterRepository)
        {
            _logger = logger;
            _patientRepository = patientRepository;
            _patientValidation = patientValidation;
            _encounterRepository = encounterRepository; 
        }

        /// <summary>
        /// Persists a purchase to the database.
        /// </summary>
        /// <param name="model">PurchaseDTO used to build the purchase.</param>
        /// <returns>The persisted purchase with IDs.</returns>
        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            IEnumerable<Patient> patients;

            try
            {
                patients = await _patientRepository.GetAllPatientsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return patients;
        }
        /// <summary>
        /// Asynchronously retrieves the product with the provided id from the database.
        /// </summary>
        /// <param name="productId">The id of the product to retrieve.</param>
        /// <returns>The product.</returns>
        /// <exception cref="ServiceUnavailableException"></exception>
        /// <exception cref="NotFoundException"></exception>
        public async Task<Patient> GetPatientByIdAsync(int patientId)
        {
            Patient patient;

            try
            {
                patient = await _patientRepository.GetPatientByIdAsync(patientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            if (patient == null || patient == default)
            {
                _logger.LogInformation($"Patient with id: {patientId} could not be found.");
                throw new NotFoundException($"Patient with id: {patientId} could not be found.");
            }

            return patient;
        }
        /// <summary>
        /// Asynchronously retrieves the product with the provided id from the database.
        /// </summary>
        /// <param name="productId">The id of the product to retrieve.</param>
        /// <returns>The product.</returns>
        /// <exception cref="ServiceUnavailableException"></exception>
        /// <exception cref="NotFoundException"></exception>
        public async Task<Patient> CreatePatientAsync(Patient newPatient)
        {
            Patient savedPatient;
            List<string> errorsList = _patientValidation.ValidationForPatient(newPatient);
            if (errorsList.Count > 0)
            {
                var result = string.Join(",", errorsList);
                throw new BadRequestException(result);
            }

            try
            {
                savedPatient = await _patientRepository.GetPatientByEmailAsync(newPatient.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            if (savedPatient != null)
            {
                throw new ConflictException($"Patient Email already exists in the database.");
            }

            try
            {
                savedPatient = await _patientRepository.CreatePatientAsync(newPatient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }
            return savedPatient;
        }
        /// <summary>
        /// Asynchronously retrieves the product with the provided id from the database.
        /// </summary>
        /// <param name="productId">The id of the product to retrieve.</param>
        /// <returns>The product.</returns>
        /// <exception cref="ServiceUnavailableException"></exception>
        /// <exception cref="NotFoundException"></exception>
        public async Task<Patient> UpdatePatientAsync(int patientId, Patient updatedPatient)
        {
            Patient existingPatient;
            List<string> errorsList = _patientValidation.ValidationForPatient(updatedPatient);
            if (errorsList.Count > 0)
            {

                var result = string.Join(",", errorsList);
                throw new BadRequestException(result);
            }

            try
            {
                existingPatient = await _patientRepository.GetPatientByEmailAsync(updatedPatient.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting getting existing patient.");
            }

            if (existingPatient == default || existingPatient == null )
            {
                _logger.LogInformation($"Patient with id: {patientId} does not exist.");
                throw new NotFoundException($"Patient with id: {patientId} not found.");
            }
            if(existingPatient.Email == updatedPatient.Email && existingPatient.Id != updatedPatient.Id )
            {
                throw new ConflictException("Email already exists in the database");
            }
           
            try
            {
                await _patientRepository.UpdatePatientAsync(updatedPatient);
                _logger.LogInformation("Patient updated.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return updatedPatient;
        }
        public async Task<IEnumerable<Encounter>> GetPatientEncounterByIdAsync(int patientId, int encounterId)
        {
            Patient patient;
            IEnumerable<Encounter> encounters;
            try
            {
               patient = await _patientRepository.GetPatientByIdAsync(patientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was aproblem connecting to the database");
            }
            if (patient == null)
            {
                throw new NotFoundException("There is currently no Patient with that ID in the database");
            }
            try
            {
                encounters = await _encounterRepository.GetAllEncountersAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database");
            }
            if (encounters == null)
            {
                throw new NotFoundException("There are no Encounters");
            }
            foreach (Encounter encounter in encounters)
            {
               
            }
            return encounters;

        }
        public async Task<Patient> DeletePatientAsync(int patientId)
        {
            Patient existingPatient;
            IEnumerable<Encounter> encounter;
            try
            {
                existingPatient = await _patientRepository.GetPatientByIdAsync(patientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }
            if (existingPatient == null)
            {
                _logger.LogInformation($"Patient with id: {patientId} does not exist.");
                throw new NotFoundException($"Patient with id: {patientId} not found.");
            }
            try
            {
                encounter = await _encounterRepository.GetAllEncountersByPatientIdAsync(patientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.Message);
                throw new ServiceUnavailableException("err");
            }
            if (encounter.Count() != 0)
            {
                _logger.LogInformation("Cannot delete a patient with an encounter");
                throw new ConflictException("Cannot delete a patient with an encounter");
            }

            try
            {
                await _patientRepository.DeletePatientAsync(existingPatient);
                _logger.LogInformation("User updated.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }
            return existingPatient;

        }
        /// <summary>
        /// Asynchronously retrieves the product with the provided id from the database.
        /// </summary>
        /// <param name="productId">The id of the product to retrieve.</param>
        /// <returns>The product.</returns>
        /// <exception cref="ServiceUnavailableException"></exception>
        /// <exception cref="NotFoundException"></exception>
        public async Task<Patient> GetPatientByEmailAsync(string patientEmail)
        {
            Patient patient;

            try
            {
                patient = await _patientRepository.GetPatientByEmailAsync(patientEmail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            if (patient == null || patient == default)
            {
                _logger.LogInformation($"Patient with id: {patientEmail} could not be found.");
                throw new NotFoundException($"Patient with id: {patientEmail} could not be found.");
            }

            return patient;
        }

    }
}




