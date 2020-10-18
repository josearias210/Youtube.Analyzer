namespace Youtube.Analyzer.Shared.Models
{
    public class Sentiment
    {
        public string Message { get; set; }
        public double Positive { get; set; }
        public double Negative { get; set; }
        public double Neutral { get; set; }
    }
}
