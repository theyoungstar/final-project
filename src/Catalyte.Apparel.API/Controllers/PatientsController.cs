using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Catalyte.Apparel.DTOs.Patients;
using Catalyte.Apparel.API.DTOMappings;
using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.Providers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Catalyte.Apparel.DTOs.Encounters;

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
        private readonly IEncounterProvider _encounterProvider;

        public PatientsController(
            ILogger<PatientsController> logger,
            IPatientProvider patientProvider,
            IMapper mapper, IEncounterProvider encounterProvider)
        {
            _logger = logger;
            _mapper = mapper;
            _patientProvider = patientProvider;
            _encounterProvider = encounterProvider;
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

        [HttpPost]
        [Route("/patients/{patientId}/encounters")]
        public async Task<IActionResult> CreatePatientEncounter(int patientid, [FromBody] EncounterDTO newEncounter)
        {
            _logger.LogInformation("Request received for CreatePatient");

            var encounter = _mapper.Map<Encounter>(newEncounter);
            encounter.PatientId = patientid;
            var addedEncounter = await _encounterProvider.CreateEncounterAsync(encounter);


            return Created("/encounters", addedEncounter);
        }

        [HttpGet]
        [Route("/patients/{patientId}/encounters")]
        public async Task<ActionResult<EncounterDTO>> GetAllEncountersByPatientIdAsync(int patientId)
        {
            var encounters = await _encounterProvider.GetEncountersByPatientIdAsync(patientId);
            var mappedEncounters = _mapper.Map<IEnumerable<EncounterDTO>>(encounters);

            return Ok(mappedEncounters);

        }

        [HttpGet]
        [Route("/patients/{patientId}/encounters/{encounterId}")]
        public async Task<ActionResult<EncounterDTO>> GetAllEncounterByIdAsync(int encounterId)
        {
            var encounters = await _encounterProvider.GetEncounterByIdAsync(encounterId);
            var mappedEncounters = _mapper.Map<EncounterDTO>(encounters);

            return Ok(mappedEncounters);

        }


        [HttpDelete("/patients/{id}")]
        public async Task<IActionResult> DeletePatientAsync(int id)
        {
            _logger.LogInformation("Request received for DeletePatientAsync");
            await _patientProvider.DeletePatientAsync(id);

            return NoContent();
        }

        [HttpPut("/patients/{patientId}/encounters/{encounterId}")]
        public async Task<ActionResult<EncounterDTO>> UpdateEncounterByIdAsync(
           int encounterId,
           [FromBody] EncounterDTO encounterToUpdate)
        {
            _logger.LogInformation("Request received for UpdateEncounterAsync");

            var encounter = _mapper.Map<Encounter>(encounterToUpdate);
            var updatedEncounter = await _encounterProvider.UpdateEncounterAsync(encounterId, encounter);
            var encounterDTO = _mapper.Map<EncounterDTO>(updatedEncounter);

            return Ok(encounterDTO);
        }
    }
}
