

using System.Text.Json.Serialization;

namespace net.postman.model;

public class Variable
{
    public Variable()
    {
    }

    public Variable(string key, string value)
    {
        Key = key;
        Value = value;
        Id = new Guid();
    }

    [JsonPropertyName("id")] public Guid Id { get; set; }

    [JsonPropertyName("key")] public string Key { get; set; }

    [JsonPropertyName("value")] public string Value { get; set; }
}