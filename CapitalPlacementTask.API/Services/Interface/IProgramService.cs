using CapitalPlacementTask.API.Models.DTOs;

namespace CapitalPlacementTask.API.Services.Interface
{
    public interface IProgramService
    {
        Task<ResponseDto<CreateDto>> Create(ProgramInfoDto programDto);

        Task<ResponseDto<List<ProgramInfoDto>>> GetAllPrograms();

        Task<ResponseDto<ProgramInfoDto>> GetProgramById(Guid Id);

        Task<ResponseDto<string>> Update(ProgramInfoDto programDto);

        Task<ResponseDto<string>> Delete(Guid Id);
    }
}
