using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Youtube.Analyzer.Models;
using Youtube.Analyzer.Shared.Models;

namespace Youtube.Analyzer.Services
{
    public class YoutubeService : IYoutubeService
    {
        private readonly AppSettings appSetting;

        public YoutubeService(AppSettings appSetting)
        {
            this.appSetting = appSetting;
        }

        public async Task<IEnumerable<Video>> SearchVideosAsync(string keyword, int maxResult = 10)
        {
            var service = GetService();
            var videos = service.Search.List("snippet");
            videos.Q = keyword;
            videos.MaxResults = maxResult;
            videos.Type = "video";
            videos.Order = SearchResource.ListRequest.OrderEnum.Date;

            var response = await videos.ExecuteAsync();
            if (response != null)
            {
                return response.Items.Select(x => new Video()
                {
                    VideoId = x.Id.VideoId,
                    Title = x.Snippet.Title,
                    Thumbnail = x.Snippet.Thumbnails.Medium.Url
                });

            }
            return Enumerable.Empty<Video>();
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync(string videoId, int maxResults = 10)
        {
            var service = GetService();

            var video = service.CommentThreads.List("snippet");
            video.MaxResults = maxResults;
            video.VideoId = videoId;

            var response = await video.ExecuteAsync();
            if (response != null)
            {
                return response.Items.Select(x => new Comment()
                {
                    VideoId = x.Snippet.VideoId,
                    Description = x.Snippet.TopLevelComment.Snippet.TextOriginal,
                    LikeCount = x.Snippet.TopLevelComment.Snippet.LikeCount ?? 0
                }).ToList();
            }

            return Enumerable.Empty<Comment>();
        }

        private YouTubeService GetService()
        {
            return new YouTubeService(new BaseClientService.Initializer()
            {
                ApplicationName = appSetting.YoutubeAppName,
                ApiKey = appSetting.YoutubeApiKey,
            });
        }
    }
}
