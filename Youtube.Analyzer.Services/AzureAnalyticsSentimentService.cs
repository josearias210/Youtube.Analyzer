using Azure;
using System;
using Azure.AI.TextAnalytics;
using Youtube.Analyzer.Models;
using System.Threading.Tasks;
using Youtube.Analyzer.Shared.Models;

namespace Youtube.Analyzer.Services
{
    public class AzureAnalyticsSentimentService : IAnalyticsSentimentService
    {
        private readonly AppSettings appSetting;

        public AzureAnalyticsSentimentService(AppSettings appSetting)
        {
            this.appSetting = appSetting;
        }

        public async Task<Sentiment> AnalysisAsync(string message)
        {
            var client = new TextAnalyticsClient(new Uri(this.appSetting.TextAnalyticsEndpoint), new AzureKeyCredential(this.appSetting.TextAnalyticsApiKey));

            var documentSentiment = await client.AnalyzeSentimentAsync(message);

            return new Sentiment()
            {
                Positive = documentSentiment.Value.ConfidenceScores.Positive,
                Negative = documentSentiment.Value.ConfidenceScores.Negative,
                Neutral = documentSentiment.Value.ConfidenceScores.Neutral,
                Message = message
            };
        }
    }
}