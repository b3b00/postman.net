

using System.Text.Json.Serialization;

namespace net.postman.model;

public  class Raw
{
    [JsonPropertyName("language")] public string Language { get; set; } = "json";
}