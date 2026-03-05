using System.Text.Json.Serialization;

namespace backend.Models.Response
{
    public class GeneralResponse<T> where T : class
    {
        [JsonPropertyName("StatusCode")]
        public int StatusCode { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Object")]
        public T? Object { get; set; }
    }
}
