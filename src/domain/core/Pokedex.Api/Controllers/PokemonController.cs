using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Pokedex.Api.Controllers
{
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/")]
    public class PokemonController : ControllerBase
    {
        [HttpGet("pokemon/{name}")]
        public IActionResult GetInformation(string name)
        {
            return StatusCode(StatusCodes.Status200OK, null);
        }
    }
}
