using System;
using System.Text.Json.Serialization;

namespace TFLRoadStatusApp.Modal
{
    public class TflRoadStatusErrorResponse
    {
        public TflRoadStatusErrorResponse()
        {

        }
        [JsonPropertyName("$type")]
        public string Type { get; set; }
        [JsonPropertyName("timestampUtc")]
        public string TimestampUtc { get; set; }
        [JsonPropertyName("exceptionType")]
        public string ExceptionType { get; set; }
        [JsonPropertyName("httpStatusCode")]
        public int HttpStatusCode { get; set; }
        [JsonPropertyName("httpStatus")]
        public string HttpStatus { get; set; }
        [JsonPropertyName("relativeUri")]
        public string RelativeUri { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}