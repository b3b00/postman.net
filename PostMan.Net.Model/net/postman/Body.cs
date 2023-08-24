
using System.Text.Json.Serialization;

namespace net.postman.model;

public  class Body
{
    [JsonPropertyName("mode")]
    public string Mode { get; set; }

    [JsonPropertyName("raw")]
    public string Raw { get; set; }

    [JsonPropertyName("options")]
    public Options Options { get; set; }

    [JsonPropertyName("urlencoded")] public List<UrlEncodedParameter> Parameters { get; set; }
}