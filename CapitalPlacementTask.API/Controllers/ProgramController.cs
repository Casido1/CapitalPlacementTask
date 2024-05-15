using CapitalPlacementTask.API.Models.DTOs;
using CapitalPlacementTask.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacementTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        private readonly IProgramService _programService;

        public ProgramController(IProgramService programService)
        {
            _programService = programService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProgramAsync([FromBody] ProgramInfoDto programDto)
        {
            var result = await _programService.Create(programDto);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCandidatesAsync()
        {
            var result = await _programService.GetAllPrograms();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProgramByIdAsync([FromRoute] Guid id)
        {
            var result = await _programService.GetProgramById(id);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProgramAsync([FromBody] ProgramInfoDto programDto)
        {
            var result = await _programService.Update(programDto);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgramByIdAsync([FromRoute] Guid id)
        {
            var result = await _programService.Delete(id);

            return Ok(result);
        }
    }
}
