using AutoMapper;
using CapitalPlacementTask.API.Data.Repository.Interface;
using CapitalPlacementTask.API.Models.DTOs;
using CapitalPlacementTask.API.Models.Entities;
using CapitalPlacementTask.API.Services.Interface;
using System.Net;

namespace CapitalPlacementTask.API.Services.Implementation
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _repo;
        private readonly IMapper _mapper;

        public CandidateService(ICandidateRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<ResponseDto<CreateDto>> Create(CandidateDto candidateDto)
        {
            var candidate = _mapper.Map<Candidate>(candidateDto);

            await _repo.Add(candidate);

            if (await _repo.SaveChangesAsync())
            {
                return new ResponseDto<CreateDto>(new CreateDto { Id = candidate.CandidateId });
            }

            return new ResponseDto<CreateDto>(HttpStatusCode.BadRequest);
        }

        public async Task<ResponseDto<string>> Delete(Guid Id)
        {
            var result = await _repo.DeleteById(Id);

            if (!result)
            {
                return new ResponseDto<string>(HttpStatusCode.NotFound);
            }

            if (await _repo.SaveChangesAsync())
            {
                return new ResponseDto<string>(HttpStatusCode.NoContent);
            }

            return new ResponseDto<string>(HttpStatusCode.BadRequest, "Delete operation failed");
        }

        public async Task<ResponseDto<List<CandidateToReturnDto>>> GetAllCandidates()
        {
            var result = await _repo.GetAll();

            return new ResponseDto<List<CandidateToReturnDto>>(_mapper.Map<List<CandidateToReturnDto>>(result));
        }
    }
}
