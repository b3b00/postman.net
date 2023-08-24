

using System.Text.Json.Serialization;

namespace net.postman.model;

public  class Script
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("exec")]
    public List<string> Exec { get; set; }

    [JsonPropertyName("type")] public string Type { get; set; } = "text/javascript";
}