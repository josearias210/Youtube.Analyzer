using System.Threading.Tasks;
using Youtube.Analyzer.Shared.Models;

namespace Youtube.Analyzer.Services
{
    public interface IAnalyticsSentimentService
    {
        Task<Sentiment> AnalysisAsync(string message);
    }
}