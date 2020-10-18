# Description

Example application to show the use of youtube API and azure's additive services to analyze text.


## Usage

1. In the project "Youtube.Analyzer.Api" add the following secrets (Change UrlWebApi if required)

```JSON
{
  "AppSettings": {
    "YoutubeApiKey": "YOUR_KEY_YOUTUBE",
    "YoutubeAppName": "YOUR_NAME_YOUTUBE",
    "TextAnalyticsEndpoint": "YOUR_ENDPOINT_TEXT_ANALYTICS",
    "TextAnalyticsApiKey": "YOUR_KEY_TEXT_ANALYTICS",
    "UrlWebApi": "https://localhost:5001/api/"
  }
}
```

2. Run the API and APP project simultaneously

## License
[MIT](https://choosealicense.com/licenses/mit/)
