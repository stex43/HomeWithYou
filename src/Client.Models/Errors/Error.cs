using System.Text.Json.Serialization;

namespace HomeWithYou.Client.Models.Errors
{
    public sealed class Error
    {
        [JsonPropertyName("code")]
        public int StatusCode { get; set; }
        
        [JsonPropertyName("message")]
        public string Message { get; set; }
        
        [JsonPropertyName("target")]
        public string Target { get; set; }
    }
}