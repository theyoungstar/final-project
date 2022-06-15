using Catalyte.Apparel.Data.Context;
using Catalyte.Apparel.Data.Filters;
using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Catalyte.Apparel.Data.Repositories
{
    /// <summary>
    /// This class handles methods for making requests to the product repository.
    /// </summary>

    public class EncounterRepository : IEncounterRepository
    {
        private readonly ILogger<EncounterRepository> _logger;
        private readonly IApparelCtx _ctx;

        public EncounterRepository(IApparelCtx ctx, ILogger<EncounterRepository> logger)
        {
            _logger = logger;
            _ctx = ctx;
        }

        public async Task<Encounter> AddEncounterAsync(Encounter encounter)
        {
            _ctx.Encounters.Add(encounter);
            await _ctx.SaveChangesAsync();
            return encounter;
        }
        public async Task<IEnumerable<Encounter>> GetAllEncountersAsync()
        {
            return await _ctx.Encounters
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .ToListAsync();
        }
        public async Task<Encounter> UpdateEncounterAsync(Encounter encounter)
        {
            _ctx.Encounters.Update(encounter);
            await _ctx.SaveChangesAsync();
            return encounter;

        }

        public async Task<Encounter> GetEncounterByIdAsync(int Id)
        {
            return await _ctx.Encounters
                .AsNoTracking()
                .WhereEncounterIdEquals(Id)
                .SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<Encounter>> GetAllEncountersByPatientIdAsync(int patientId)
        {
            return await _ctx.Encounters
                .AsNoTracking()
                .Where(p => p.PatientId == patientId)
                .AsQueryable()
                .ToListAsync();
        }
        public async Task<Encounter> GetPatientEncounterByIdAsync(int patientId, int encounterId)
        {
            return await _ctx.Encounters
                .AsNoTracking()
                .WhereEncounterIdEquals(patientId)
                .SingleOrDefaultAsync();
        }

    }
}