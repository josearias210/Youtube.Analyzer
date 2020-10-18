using System.Threading.Tasks;
using Youtube.Analyzer.Services;
using Youtube.Analyzer.Shared.Models;

namespace Youtube.Analyzer.Application.UseCases.AnalyzerVideo
{
    public class VideoAnalyzeUseCase
    {
        private readonly IYoutubeService youtubeService;
        private readonly IAnalyticsSentimentService analyticsSentimentService;

        public VideoAnalyzeUseCase(IYoutubeService youtubeService, IAnalyticsSentimentService analyticsSentimentService)
        {
            this.youtubeService = youtubeService;
            this.analyticsSentimentService = analyticsSentimentService;
        }

        public async Task<AnalyzeComment> ExecuteAsync(string videoId)
        {
            var analyzeComment = new AnalyzeComment();

            var comments = await this.youtubeService.GetCommentsAsync(videoId);
            if (comments != null)
            {
                foreach (var comment in comments)
                {
                    var result = await this.analyticsSentimentService.AnalysisAsync(comment.Description);
                    if (result != null)
                    {
                        analyzeComment.Total++;
                        analyzeComment.Positive += result.Positive;
                        analyzeComment.Neutral += result.Neutral;
                        analyzeComment.Negative += result.Negative;
                    }
                }

                analyzeComment.Positive /= analyzeComment.Total;
                analyzeComment.Neutral /= analyzeComment.Total;
                analyzeComment.Negative /= analyzeComment.Total;
            }

            return analyzeComment;
        }
    }
}
