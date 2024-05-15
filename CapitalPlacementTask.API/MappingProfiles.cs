using AutoMapper;
using CapitalPlacementTask.API.Models.DTOs;
using CapitalPlacementTask.API.Models.Entities;

namespace CapitalPlacementTask.API
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ProgramInfoDto, ProgramInfo>().ReverseMap();

            CreateMap<CandidateDto, Candidate>();

            CreateMap<Candidate, CandidateToReturnDto>();
        }
    }
}
