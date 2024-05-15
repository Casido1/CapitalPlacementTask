using CapitalPlacementTask.API.Data.Repository.Interface;
using CapitalPlacementTask.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CapitalPlacementTask.API.Data.Repository.Implementation
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly CosmosDbContext _context;

        public CandidateRepository(CosmosDbContext context)
        {
            _context = context;
        }

        public async Task Add(Candidate candidate)
        {
            await _context.AddAsync(candidate);
        }

        public async Task<bool> DeleteById(Guid id)
        {
            //Load program with related data so we can delete them together
            var candidate = await _context.Candidates.FindAsync(id);

            if (candidate == null) return false;

            _context.Candidates.Remove(candidate);

            return true;
        }

        public async Task<List<Candidate>> GetAll()
        {
            return await _context.Candidates.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }


    }
}
