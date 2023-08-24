using System.Text.Json.Serialization;


namespace net.postman.model;

public class Info
{
    [JsonPropertyName("_postman_id")] public Guid PostmanId { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("schema")] public Uri Schema { get; set; }
}