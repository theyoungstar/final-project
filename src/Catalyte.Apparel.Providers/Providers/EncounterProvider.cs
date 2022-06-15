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
        public async Task<Encounter> CreateEncounterAsync(Encounter newEncounter)
        {
            Encounter savedEncounter;

 
            List<string> errorsList = _encounterValidation.ValidationForEncounter(newEncounter);
            if (errorsList.Count > 0)
            {
                var result = string.Join(",", errorsList);
                throw new BadRequestException(result);
            }
            try
            {
                savedEncounter =await _encounterRepository.AddEncounterAsync(newEncounter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem getting the encounter.");
            }
            if (savedEncounter == null)
            {
                throw new UnprocessableEntityException($"Encounter Id already exists in the database.");
            }
            return savedEncounter;
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
            IEnumerable<Encounter> existingEncounters;

            List<string> errorsList = _encounterValidation.ValidationForEncounter(updatedEncounter);
            if (errorsList.Count > 0)
            {

                var result = string.Join(",", errorsList);
                throw new BadRequestException(result);
            }

            try
            {
                existingEncounters = await _encounterRepository.GetAllEncountersByPatientIdAsync(patientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            if (existingEncounters == default || existingEncounters == null)
            {
                _logger.LogInformation($"There are no encounters attached to patient Id: {patientId}.");
                throw new NotFoundException($"There are no encounters attached to patient Id: {patientId}.");
            }
            foreach (var encounter in existingEncounters)
                if (encounter.Id != updatedEncounter.Id)
                {
                    _logger.LogInformation($"Encounter Id does not exist. Please create a new encounter");
                    throw new NotFoundException($"This is a new encounter Id. Are you trying to create a new encounter?");
                }
                else if (encounter.Id == default)
                    encounter.Id = updatedEncounter.Id;

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

            // GIVE THE MOVIE ID IF NOT SPECIFIED IN BODY TO AVOID DUPLICATE PRODUCTS
            if (updatedEncounter.Id == default)
                updatedEncounter.Id = encounterId;


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




