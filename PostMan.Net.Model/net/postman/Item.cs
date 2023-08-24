

using System.Diagnostics;
using System.Text.Json.Serialization;

namespace net.postman.model;


[DebuggerDisplay("{ItemName}")]
public class Item
{
    [JsonPropertyName("item")] public List<Item> Items { get; set; } = new List<Item>();

    [JsonPropertyName("protocolProfileBehavior")]
    public ProtocolProfileBehavior ProtocolProfileBehavior { get; set; }

    [JsonIgnore] public bool IsFolder => Items != null && Items.Any();

    [JsonIgnore] public bool IsTestCase => Items == null || !Items.Any();

    [JsonIgnore] public Item Parent { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonIgnore] public string ItemName => (IsFolder ? "/" : "") + Name;
    
    [JsonPropertyName("event")]
    public List<Event> Event { get; set; }

    [JsonPropertyName("request")]
    public Request Request { get; set; }

    [JsonPropertyName("response")]
    public List<object> Response { get; set; } = new List<object>();
        

    
    public List<Item> GetTestCases()
    {
        if (IsFolder)
        {
            List<Item> testCases = new List<Item>();
            for (int i = 0; i < Items.Count; i++)
            {
                var item = Items[i];
                testCases.AddRange(item.GetTestCases());
            }

            return testCases;
        }
        else
        {
            return new List<Item>() { this };
        }
    }
    
    public int GetTestCaseCount()
    {
        if (IsFolder)
        {
            int sum = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                var item = Items[i];
                sum += item.GetTestCaseCount();
            }

            return sum;
        }

        return 1;
    }

    public void SetParent(Item parent)
    {
        Parent = parent;
        foreach (var item in Items)
        {
            item.SetParent(this);
        }
    }

}