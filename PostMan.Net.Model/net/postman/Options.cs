

using System.Text.Json.Serialization;

namespace net.postman.model;

public  class Options
{
    [JsonPropertyName("raw")] public Raw Raw { get; set; } = new Raw() {Language = "json"};
}