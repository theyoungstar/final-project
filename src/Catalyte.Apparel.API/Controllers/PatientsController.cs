﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Catalyte.Apparel.DTOs.Patients;
using Catalyte.Apparel.API.DTOMappings;
using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.Providers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalyte.Apparel.API.Controllers
{
    /// <summary>
    /// The PatientsController exposes endpoints for patient related actions.
    /// </summary>
    [ApiController]
    [Route("/patients")]
    public class PatientsController : ControllerBase
    {
        private readonly ILogger<PatientsController> _logger;
        private readonly IPatientProvider _patientProvider;
        private readonly IMapper _mapper;

        public PatientsController(
            ILogger<PatientsController> logger,
            IPatientProvider patientProvider,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _patientProvider = patientProvider;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDTO>>> GetAllPatientsAsync()
        {
            _logger.LogInformation("Request received for GetPatientsAsync");

            var patients = await _patientProvider.GetAllPatientsAsync();
            var patientDTOs = _mapper.Map<IEnumerable<PatientDTO>>(patients);

            return Ok(patientDTOs);
        }

        [HttpGet("/patients/{id}")]
        public async Task<ActionResult<PatientDTO>> GetPatientByIdAsync(int id)
        {
            _logger.LogInformation($"Request received for GetPatientByIdAsync for id: {id}");

            var patient = await _patientProvider.GetPatientByIdAsync(id);
            var patientDTO = _mapper.Map<PatientDTO>(patient);

            return Ok(patientDTO);
        }

        [HttpPut("/patients/{id}")]
        public async Task<ActionResult<PatientDTO>> UpdateUserAsync(string email,
            int id,
            [FromBody] PatientDTO patientToUpdate)
        {
            _logger.LogInformation("Request received for UpdatePatientAsync");

            var patient = _mapper.Map<Patient>(patientToUpdate);
            var updatedPatient = await _patientProvider.UpdatePatientAsync(email, id, patient);
            var patientDTO = _mapper.Map<PatientDTO>(updatedPatient);

            return Ok(patientDTO);
        }


        [HttpPost]
        public async Task<ActionResult<PatientDTO>> CreatePatientAsync([FromBody] PatientDTO patient)
        {
            _logger.LogInformation("Request received for CreatePatient");

            var newPatient = _mapper.Map<Patient>(patient);
            var savedPatient = await _patientProvider.CreatePatientAsync(newPatient);
            var patientDTO = _mapper.Map<PatientDTO>(savedPatient);

            if (patientDTO != null)
            {
                return Created($"/patients", patientDTO);
            }

            return NoContent();
        }
    }
}