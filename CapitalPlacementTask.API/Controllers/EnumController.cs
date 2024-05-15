using CapitalPlacementTask.API.Enums;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CapitalPlacementTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnumController : ControllerBase
    {
        [HttpGet("genders")]
        public IActionResult GetGenders()
        {
            var genders = Enum.GetNames(typeof(Gender)).ToList();

            return Ok(genders);
        }

        [HttpGet("questiontypes")]
        public IActionResult GetQuestionTypes()
        {
            var types = Enum.GetNames(typeof(QuestionType)).ToList();

            return Ok(types);
        }
    }
}
