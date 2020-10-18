namespace Youtube.Analyzer.Shared.Models
{
    public class Comment
    {
        public string VideoId { get; set; }
        public string Description { get; set; }
        public long LikeCount { get; set; }
    }
}
