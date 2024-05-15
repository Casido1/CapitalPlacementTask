using CapitalPlacementTask.API.Models.Entities;

namespace CapitalPlacementTask.API.Data.Repository.Interface
{
    public interface ICandidateRepository
    {
        Task Add(Candidate program);

        Task<List<Candidate>> GetAll();

        Task<bool> DeleteById(Guid Id);

        Task<bool> SaveChangesAsync();
    }
}
