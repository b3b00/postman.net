using System.Text.Json.Serialization;

namespace net.postman.model
{
    public class Header
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }
        
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("type")] public string Type { get; set; } = "text";

    }
}