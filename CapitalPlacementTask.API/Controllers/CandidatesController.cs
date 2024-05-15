using CapitalPlacementTask.API.Models.DTOs;
using CapitalPlacementTask.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacementTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCandidateAsync([FromBody] CandidateDto candidateDto)
        {
            var result = await _candidateService.Create(candidateDto);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCandidatesAsync()
        {
            var result = await _candidateService.GetAllCandidates();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidateByIdAsync([FromRoute] Guid id)
        {
            var result = await _candidateService.Delete(id);

            return Ok(result);
        }
    }
}

