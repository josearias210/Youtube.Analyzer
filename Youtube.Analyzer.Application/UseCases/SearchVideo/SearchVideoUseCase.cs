using System.Collections.Generic;
using System.Threading.Tasks;
using Youtube.Analyzer.Services;
using Youtube.Analyzer.Shared.Models;

namespace Youtube.Analyzer.Application.UseCases.AnalyzerVideo
{
    public class SearchVideoUseCase
    {
        private readonly IYoutubeService youtubeService;

        public SearchVideoUseCase(IYoutubeService youtubeService)
        {
            this.youtubeService = youtubeService;
        }

        public async Task<IEnumerable<Video>> ExecuteAsync(string keyword)
        {
            return await this.youtubeService.SearchVideosAsync(keyword);
        }
    }
}
