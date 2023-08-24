

using System.Text.Json.Serialization;

namespace net.postman.model;

public  class Url
{
    [JsonPropertyName("raw")]
    public string Raw { get; set; } 

    [JsonPropertyName("protocol")] public string Protocol { get; set; } 

    [JsonPropertyName("host")] public List<string> Host { get; set; }

    [JsonPropertyName("path")]
    public List<string> Path { get; set; }
}