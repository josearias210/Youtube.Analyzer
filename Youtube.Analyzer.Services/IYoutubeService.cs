using System.Collections.Generic;
using System.Threading.Tasks;
using Youtube.Analyzer.Shared.Models;

namespace Youtube.Analyzer.Services
{
    public interface IYoutubeService
    {
        Task<IEnumerable<Comment>> GetCommentsAsync(string videoId, int maxResults = 10);
        Task<IEnumerable<Video>> SearchVideosAsync(string keyword, int maxResult = 10);
    }
}