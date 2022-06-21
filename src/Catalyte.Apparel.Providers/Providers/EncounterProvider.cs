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
        public async Task<Encounter> CreateEncounterAsync(int patientId, Encounter newEncounter)
        {
            
            
            Encounter savedEncounter;
            List<string> errorsList = _encounterValidation.ValidationForEncounter(newEncounter);
            if (errorsList.Count > 0)
            {
                var result = string.Join(",", errorsList);
                throw new BadRequestException(result);
            }
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
            if (patientId == default)
            {
                throw new ServiceUnavailableException($"Patient with ID: {patientId} does not exist ");
            }
            try
            {
                savedEncounter = await _encounterRepository.CreateEncounterAsync(newEncounter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("Unable to connect to the database.");
            }

            return newEncounter;





        }
        /// <summary>
        /// Asynchronously retrieves the product with the provided id from the database.
        /// </summary>
        /// <param name="productId">The id of the product to retrieve.</param>
        /// <returns>The product.</returns>
        /// <exception cref="ServiceUnavailableException"></exception>
        /// <exception cref="NotFoundException"></exception>
        public async Task<Encounter> UpdateEncounterAsync(int patientId, int encounterId, Encounter updatedEncounter)
        {
            // VALIDATE MOVIE TO UPDATE EXISTS
            Encounter existingEncounter;
            Patient patient; 
            List<string> errorsList = _encounterValidation.ValidationForEncounter(updatedEncounter);
            if (errorsList.Count > 0)
            {

                var result = string.Join(",", errorsList);
                throw new BadRequestException(result);
            }

            try
            {
                patient = await _patientRepository.GetPatientByIdAsync(patientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem accessing an encounter from the database.");
            }
            if (patient == null || patient == default)
            {
                _logger.LogInformation($"Patient with ID: {patientId} not found in the database.");
                throw new NotFoundException($"Patient with ID: {patientId} not found in the database.");
            }
            try
            {
                existingEncounter = await _encounterRepository.GetEncounterByIdAsync(encounterId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem accessing an encounter from the database.");
            }
            if (existingEncounter == null)
            {
                _logger.LogInformation($"Enounter ID cannot be null.");
                throw new NotFoundException("Encounter Id not found");
            }

            if (existingEncounter.PatientId != patientId)
            {
                _logger.LogInformation($"This encounter belongs to a different patient. Please verify.");
                throw new NotFoundException($"This encounter belongs to a differnt patient.");
            }
            if(updatedEncounter.PatientId == default)
                updatedEncounter.PatientId = patientId;
            // GIVE THE MOVIE ID IF NOT SPECIFIED IN BODY TO AVOID DUPLICATE PRODUCTS
            if (updatedEncounter.Id == default)
                updatedEncounter.Id = encounterId;
            try
            {
                await _encounterRepository.UpdateEncounterAsync(updatedEncounter);
                _logger.LogInformation("Encounter updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

       

            return updatedEncounter;
        }
        
        /// <summary>
        /// Asynchronously retrieves the product with the provided id from the database.
        /// </summary>
        /// <param name="productId">The id of the product to retrieve.</param>
        /// <returns>The product.</returns>
        /// <exception cref="ServiceUnavailableException"></exception>
        /// <exception cref="NotFoundException"></exception>
        public async Task<Encounter> GetEncounterByIdAsync(int encounterId)
        {
            Encounter encounter;
            Patient patient;
            try
            {
                encounter = await _encounterRepository.GetEncounterByIdAsync(encounterId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            if (encounter == null || encounter == default)
            {
                _logger.LogInformation($"Enounter with id: {encounterId} could not be found.");
                throw new NotFoundException($"Encounter with id: {encounterId} could not be found.");
            }

            return encounter;
        }
        /// <summary>
        /// Asynchronously retrieves the product with the provided id from the database.
        /// </summary>
        /// <param name="productId">The id of the product to retrieve.</param>
        /// <returns>The product.</returns>
        /// <exception cref="ServiceUnavailableException"></exception>
        /// <exception cref="NotFoundException"></exception>
        public async Task<Encounter> GetEncounterByIdByPatientIdAsync(int patientId, int encounterId)
        {
            Encounter encounter;
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
            try
            {
                encounter = await _encounterRepository.GetEncounterByIdAsync(encounterId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            if (encounter == null || encounter == default)
            {
                _logger.LogInformation($"Enounter with id: {encounterId} could not be found.");
                throw new NotFoundException($"Encounter with id: {encounterId} could not be found.");
            }
            if (encounter.PatientId == patientId && encounterId != default)
            {
                return encounter;
            }

            return encounter;
        }



    }
}




