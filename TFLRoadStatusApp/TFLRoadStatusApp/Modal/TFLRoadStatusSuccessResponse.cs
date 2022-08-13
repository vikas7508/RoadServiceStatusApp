using System;
using System.Text.Json.Serialization;

namespace TFLRoadStatusApp.Modal
{
    public class TflRoadStatusSuccessResponse
    {
        public TflRoadStatusSuccessResponse()
        {

        }
        [JsonPropertyName("$type")]
        public string Type { get; set; }
        [JsonPropertyName("bounds")]
        public string Bounds { get; set; }
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
        [JsonPropertyName("envelope")]
        public string Envelope { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("statusSeverity")]
        public string StatusSeverity { get; set; }
        [JsonPropertyName("statusSeverityDescription")]
        public string StatusSeverityDescription { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
