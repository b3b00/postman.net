

using System.Text.Json.Serialization;

namespace net.postman.model;

public  class Request
{
    [JsonPropertyName("method")]
    public string Method { get; set; }

    [JsonPropertyName("header")]
    public List<Header> Header { get; set; }

    [JsonPropertyName("body")]
    public Body Body { get; set; }

    [JsonPropertyName("url")]
    public Url Url { get; set; }
}