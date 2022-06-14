/*using System.Collections.Generic;
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
    [Route("/ecounters")]
    public class EncountersController : ControllerBase
    {
        private readonly ILogger<EncountersController> _logger;
        private readonly IEncounterProvider _encounterProvider;
        private readonly IMapper _mapper;

        public EncountersController(
            ILogger<EncountersController> logger,
            IEncounterProvider encounterProvider,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _encounterProvider = encounterProvider;
        }

        [HttpGet("/encounters/{id}")]

        public async Task<ActionResult<EncounterDTO>> GetEncounterByIdAsync(int id)
        {
            _logger.LogInformation($"Request received for GetEncounterByIdAsync for id: {id}");

            var encounter = await _encounterProvider.GetEncounterByIdAsync(id);
            var encounterDTO = _mapper.Map<EncounterDTO>(encounter);

            return Ok(encounterDTO);
        }

        [HttpPut("")]
        public async Task<ActionResult<EncounterDTO>> UpdateEncounterAsync(string email,
            int id,
            [FromBody] EncounterDTO encounterToUpdate)
        {
            _logger.LogInformation("Request received for UpdateEncounterAsync");

            var encounter = _mapper.Map<Encounter>(encounterToUpdate);
            var updatedEncounter = await _encounterProvider.UpdateEncounterAsync(email, id, encounter);
            var encounterDTO = _mapper.Map<EncounterDTO>(updatedEncounter);

            return Ok(encounterDTO);
        }


        [HttpPost("")]
        public async Task<ActionResult<PatientDTO>> CreateEncounterAsync([FromBody] EncounterDTO encounter)
        {
            _logger.LogInformation("Request received for CreatePatient");

            var newEncounter = _mapper.Map<Encounter>(encounter);
            var savedEncounter = await _encounterProvider.CreateEncounterAsync(newEncounter);
            var encounterDTO = _mapper.Map<EncounterDTO>(savedEncounter);

            if (encounterDTO != null)
            {
                return Created($"/encounters", encounterDTO);
            }

            return NoContent();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EncounterDTO>>> GetAllEncountersAsync()
        {
            _logger.LogInformation("Request received for GetEncountersAsync");

            var encounterss = await _encounterProvider.GetAllEncountersAsync();
            var encounterDTOs = _mapper.Map<IEnumerable<EncounterDTO>>(encounterss);

            return Ok(encounterDTOs);
        }
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<EncounterDTO>>> GetEncountersByPatientIdAsync(int id)
        {
            _logger.LogInformation("Request received for GetEncountersAsync");

            var encounters = await _encounterProvider.GetEncountersByPatientIdAsync(id);
            var encounterDTOs = _mapper.Map<IEnumerable<EncounterDTO>>(encounters);

            return Ok(encounterDTOs);
        }

    }
}
*/
