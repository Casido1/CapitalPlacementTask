using CapitalPlacementTask.API.Data.Repository.Interface;
using CapitalPlacementTask.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CapitalPlacementTask.API.Data.Repository.Implementation
{
    public class ProgramRepository : IProgramRepository
    {
        private readonly CosmosDbContext _context;

        public ProgramRepository(CosmosDbContext context)
        {
            _context = context;
        }
        public async Task Add(ProgramInfo program)
        {
            await _context.AddAsync(program);
        }

        public async Task<bool> DeleteById(Guid Id)
        {
            //Load program with related data so we can delete them together
            var program = await LoadProgramWithReferences(Id);

            if (program == null) return false;

            _context.Programs.Remove(program);

            return true;
        }

        public async Task<List<ProgramInfo>> GetAll()
        {
            var programs = await LoadAllProgramsWithReferences();

            return programs;
        }

        public async Task<ProgramInfo> GetById(Guid Id)
        {
            //Load program with related data in other containers
            var program = await LoadProgramWithReferences(Id);

            return program;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<ProgramInfo?> LoadProgramWithReferences(Guid Id)
        {
            var program = await _context
                .Programs
                .FindAsync(Id);

            if (program == null) return null;

            var programEntry = _context.Programs.Entry(program);

            // Include the Employer (which comes from another container)
            await programEntry
                .Reference(program => program.Employer)
                .LoadAsync();

            // Include the Questions (which come from another container)
            await programEntry
                .Collection(program => program.Questions)
                .LoadAsync();

            return program;
        }

        private async Task<List<ProgramInfo>> LoadAllProgramsWithReferences()
        {
            var programs = await _context
                .Programs
                .ToListAsync();

            if (programs.Count() == 0) return programs;

            foreach (var program in programs)
            {
                var programEntry = _context.Entry(program);

                // Include the Employer (which comes from another container)
                await programEntry
                    .Reference(program => program.Employer)
                    .LoadAsync();

                // Include the Questions (which come from another container)
                await programEntry
                    .Collection(program => program.Questions)
                    .LoadAsync();
            }

            return programs;
        }
    }
}
