using CapitalPlacementTask.API.Models.Entities;

namespace CapitalPlacementTask.API.Data.Repository.Interface
{
    public interface IProgramRepository
    {
        Task Add(ProgramInfo program);

        Task<List<ProgramInfo>> GetAll();

        Task<ProgramInfo> GetById(Guid Id);

        Task<bool> DeleteById(Guid Id);

        Task<bool> SaveChangesAsync();
    }
}
