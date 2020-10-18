using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Youtube.Analyzer.Application.UseCases.AnalyzerVideo;

namespace Youtube.Analyzer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly SearchVideoUseCase useCase;

        public VideosController(SearchVideoUseCase useCase)
        {
            this.useCase = useCase;
        }

        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] string q)
        {
            var result = await useCase.ExecuteAsync(q);
            return Ok(result);
        }
    }
}
