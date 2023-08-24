using System.Text.Json.Serialization;

namespace net.postman.model;

public  class Event
{
    [JsonPropertyName("listen")]
    public string Listen { get; set; }

    [JsonPropertyName("script")]
    public Script Script { get; set; }
}