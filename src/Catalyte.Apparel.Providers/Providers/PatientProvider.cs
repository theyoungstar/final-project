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
    public class PatientProvider : IPatientProvider
    {
        private readonly ILogger<PatientProvider> _logger;
        private readonly IPatientRepository _patientRepository;
        private readonly PatientValidation _patientValidation;
       /* private readonly IProductRepository _productRepository;*/



        public PatientProvider(IPatientRepository patientRepository,  ILogger<PatientProvider> logger, PatientValidation patientValidation)
        {
            _logger = logger;
            _patientRepository = patientRepository;
            _patientValidation = patientValidation;
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
        public async Task<Patient> UpdatePatientAsync(string email, int id, Patient updatedPatient)
        {
            // VALIDATE MOVIE TO UPDATE EXISTS
            Patient existingPatient;

            List<string> errorsList = _patientValidation.ValidationForPatient(updatedPatient);
            if (errorsList.Count > 0)
            {

                var result = string.Join(",", errorsList);
                throw new BadRequestException(result);
            }

            try
            {
                existingPatient = await _patientRepository.GetPatientByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            if (existingPatient == default || existingPatient == null)
            {
                _logger.LogInformation($"Patient with id: {id} does not exist.");
                throw new NotFoundException($"Patient with id: {id} not found.");
            }
            try
            {
                var existingPatients = await _patientRepository.GetAllPatientsAsync();
                foreach (var patient in existingPatients)
                    if (patient.Email == updatedPatient.Email)
                    {
                        _logger.LogInformation($"Patient Email already exists.");
                        throw new ConflictException($"This Email is already in use.");
                    }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            // GIVE THE MOVIE ID IF NOT SPECIFIED IN BODY TO AVOID DUPLICATE PRODUCTS
            if (updatedPatient.Id == default)
                updatedPatient.Id = id;


            // TIMESTAMP THE UPDATE
            updatedPatient.DateModified = DateTime.UtcNow;

            try
            {
                await _patientRepository.UpdatePatientAsync(updatedPatient);
                _logger.LogInformation("User updated.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return updatedPatient;
        }

    }
}




