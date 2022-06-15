using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.Providers.Interfaces;
using Catalyte.Apparel.Utilities.HttpResponseExceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Catalyte.Apparel.Providers.Providers
{
    /// <summary>
    /// This class provides the implementation of the IPurchaseProvider interface, providing service methods for purchases.
    /// </summary>
    public class EncounterProvider : IEncounterProvider
    {
        private readonly ILogger<EncounterProvider> _logger;
        private readonly IEncounterRepository _encounterRepository;
        private readonly EncounterValidation _encounterValidation;
        private readonly IPatientRepository _patientRepository;



        public EncounterProvider(IEncounterRepository encounterRepository,  ILogger<EncounterProvider> logger, EncounterValidation encounterValidation, IPatientRepository patientRepository)
        {
            _logger = logger;
            _encounterRepository = encounterRepository;
            _encounterValidation = encounterValidation;
            _patientRepository = patientRepository;
        }

        /// <summary>
        /// Persists a purchase to the database.
        /// </summary>
        /// <param name="model">PurchaseDTO used to build the purchase.</param>
        /// <returns>The persisted purchase with IDs.</returns>
        public async Task<IEnumerable<Encounter>> GetAllEncountersAsync()
        {
            IEnumerable<Encounter> encounters;

            try
            {
                encounters = await _encounterRepository.GetAllEncountersAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return encounters;
        }
        /// <summary>
        /// Asynchronously retrieves the product with the provided id from the database.
        /// </summary>
        /// <param name="productId">The id of the product to retrieve.</param>
        /// <returns>The product.</returns>
        /// <exception cref="ServiceUnavailableException"></exception>
        /// <exception cref="NotFoundException"></exception>
        public async Task<IEnumerable<Encounter>> GetEncountersByPatientIdAsync(int patientId)
        {
            IEnumerable<Encounter> encounters;

            try
            {
                encounters = await _encounterRepository.GetAllEncountersByPatientIdAsync(patientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            if (encounters == null || encounters == default)
            {
                _logger.LogInformation($"Encounter with id: {patientId} could not be found.");
                throw new NotFoundException($"Encounter with id: {patientId} could not be found.");
            }

            return encounters;
        }
        /// <summary>
        /// Asynchronously retrieves the product with the provided id from the database.
        /// </summary>
        /// <param name="productId">The id of the product to retrieve.</param>
        /// <returns>The product.</returns>
        /// <exception cref="ServiceUnavailableException"></exception>
        /// <exception cref="NotFoundException"></exception>
        public async Task<IEnumerable<Encounter>> CreateEncounterAsync(Encounter newEncounter)
        {
            IEnumerable<Encounter> savedEncounters;
            Patient patient;
 
            List<string> errorsList = _encounterValidation.ValidationForEncounter(newEncounter);
            if (errorsList.Count > 0)
            {
                var result = string.Join(",", errorsList);
                throw new BadRequestException(result);
            }
            try
            {
                patient = await _patientRepository.GetPatientByIdAsync(newEncounter.PatientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem getting the patient.");
            }
            if (patient.Id.ToString() == null || patient.Id == default)
            {
                throw new NotFoundException("Patient with this Id does not exist");
            }
            try
            {
                savedEncounters = await _encounterRepository.GetAllEncountersByPatientIdAsync(patient.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem getting the encounter.");
            }
            if (savedEncounters == null)
            {
                throw new NotFoundException("Encounter with this Patient Id does not exist");
            }

            return savedEncounters;
        }
        /// <summary>
        /// Asynchronously retrieves the product with the provided id from the database.
        /// </summary>
        /// <param name="productId">The id of the product to retrieve.</param>
        /// <returns>The product.</returns>
        /// <exception cref="ServiceUnavailableException"></exception>
        /// <exception cref="NotFoundException"></exception>
        public async Task<Encounter> UpdateEncounterAsync(string email, int id, Encounter updatedEncounter)
        {
            // VALIDATE MOVIE TO UPDATE EXISTS
            Encounter existingEncounter;

            List<string> errorsList = _encounterValidation.ValidationForEncounter(updatedEncounter);
            if (errorsList.Count > 0)
            {

                var result = string.Join(",", errorsList);
                throw new BadRequestException(result);
            }

            try
            {
                existingEncounter = await _encounterRepository.GetEncounterByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            if (existingEncounter == default || existingEncounter == null)
            {
                _logger.LogInformation($"Encounter with id: {id} does not exist.");
                throw new NotFoundException($"Encounter with id: {id} not found.");
            }
            try
            {
                var existingEncounters = await _encounterRepository.GetAllEncountersAsync();
                foreach (var encounter in existingEncounters)
                    if (encounter == updatedEncounter)
                    {
                        _logger.LogInformation($"Encounter Email already exists.");
                        throw new ConflictException($"This Email is already in use.");
                    }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            // GIVE THE MOVIE ID IF NOT SPECIFIED IN BODY TO AVOID DUPLICATE PRODUCTS
            if (updatedEncounter.Id == default)
                updatedEncounter.Id = id;


            // TIMESTAMP THE UPDATE
            updatedEncounter.DateModified = DateTime.UtcNow;

            try
            {
                await _encounterRepository.UpdateEncounterAsync(updatedEncounter);
                _logger.LogInformation("User updated.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return updatedEncounter;
        }
        public async Task<Encounter> GetEncounterByIdAsync(int id)
        {
            Encounter encounter;

            try
            {
                encounter = await _encounterRepository.GetEncounterByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return encounter;
        }
    

    }
}




