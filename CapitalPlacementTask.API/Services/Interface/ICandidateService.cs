using CapitalPlacementTask.API.Models.DTOs;

namespace CapitalPlacementTask.API.Services.Interface
{
    public interface ICandidateService
    {
        Task<ResponseDto<CreateDto>> Create(CandidateDto candidateDto);

        Task<ResponseDto<List<CandidateToReturnDto>>> GetAllCandidates();

        Task<ResponseDto<string>> Delete(Guid Id);
    }
}
