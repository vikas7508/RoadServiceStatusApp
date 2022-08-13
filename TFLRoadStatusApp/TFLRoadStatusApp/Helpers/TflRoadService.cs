using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TflRoadStatusApp.Helpers.Interfaces;

namespace TflRoadStatusApp.Helpers
{
    public class TflRoadService: ITflRoadService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public TflRoadService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        /// <summary>
        /// Function for calling TFLRoad API
        /// Takes the uri as a param and return the Task of HTTPResponse
        /// Fetches the Baseapi Uri from appsettings.json
        /// </summary>
        /// <param name="uri"></param>
        /// <returns>HttpResponseMessage</returns>
        public async Task<HttpResponseMessage> Get(string uri)
        {
            var apiKey = _config["app_key"];
            var appId = _config["app_id"];
            string APIURL = apiKey == ""? uri: $"{uri}?app_id={appId}&app_key={apiKey}";
            var response = await _httpClient.GetAsync(APIURL);
            return response;
        }
    }
}
