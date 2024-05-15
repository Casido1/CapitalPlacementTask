using AutoMapper;
using CapitalPlacementTask.API.Data.Repository.Interface;
using CapitalPlacementTask.API.Models.DTOs;
using CapitalPlacementTask.API.Models.Entities;
using CapitalPlacementTask.API.Services.Interface;
using System.Net;

namespace CapitalPlacementTask.API.Services.Implementation
{
    public class ProgramService : IProgramService
    {
        private readonly IProgramRepository _repo;
        private readonly IMapper _mapper;

        public ProgramService(IProgramRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<ResponseDto<CreateDto>> Create(ProgramInfoDto programDto)
        {
            var program = _mapper.Map<ProgramInfo>(programDto);

            await _repo.Add(program);

            if (await _repo.SaveChangesAsync())
            {
                return new ResponseDto<CreateDto>(new CreateDto { Id = program.ProgramInfoId });
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

        public async Task<ResponseDto<List<ProgramInfoDto>>> GetAllPrograms()
        {
            var result = await _repo.GetAll();

            return new ResponseDto<List<ProgramInfoDto>>(_mapper.Map<List<ProgramInfoDto>>(result));
        }

        public async Task<ResponseDto<ProgramInfoDto>> GetProgramById(Guid Id)
        {
            var program = await _repo.GetById(Id);

            if (program == null) return new ResponseDto<ProgramInfoDto>(HttpStatusCode.NotFound);

            return new ResponseDto<ProgramInfoDto>(_mapper.Map<ProgramInfoDto>(program));
        }

        public async Task<ResponseDto<string>> Update(ProgramInfoDto programDto)
        {
            var program = await _repo.GetById(programDto.ProgramInfoId);

            if (program == null) return new ResponseDto<string>(HttpStatusCode.NotFound);

            _mapper.Map(programDto, program);

            if (await _repo.SaveChangesAsync())
            {
                return new ResponseDto<string>(HttpStatusCode.NoContent);
            };

            return new ResponseDto<string>(HttpStatusCode.NotModified);
        }
    }
}
