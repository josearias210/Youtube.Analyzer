using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Youtube.Analyzer.Application.UseCases.AnalyzerVideo;

namespace Youtube.Analyzer.Api.Controllers
{
    [Route("api/videos/{videoId}")]
    [ApiController]
    public class VideosAnalyzeController : ControllerBase
    {
        private readonly VideoAnalyzeUseCase useCase;

        public VideosAnalyzeController(VideoAnalyzeUseCase useCase)
        {
            this.useCase = useCase;
        }


        [HttpGet("analyze")]
        public async Task<IActionResult> Get(string videoId)
        {
            var result = await useCase.ExecuteAsync(videoId);
            return Ok(result);
        }
    }
}
