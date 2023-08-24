
using System.Text.Json.Serialization;


namespace net.postman.model
{

    public class Collection
    {
        [JsonPropertyName("info")] public Info Info { get; set; }


        [JsonPropertyName("item")] public List<Item> Item { get; set; } = new List<Item>();

        [JsonPropertyName("event")] public List<Event> Event { get; set; }

        [JsonPropertyName("protocolProfileBehavior")]
        public ProtocolProfileBehavior ProtocolProfileBehavior { get; set; }

        [JsonPropertyName("variable")] public List<Variable> Variable { get; set; }


        public List<Item> GetTestCases()
        {
            List<Item> testCases = new List<Item>();
            for (int i = 0; i < Item.Count; i++)
            {
                var item = Item[i];
                testCases.AddRange(item.GetTestCases());
            }

            return testCases;
        }
        public virtual int GetTestCaseCount()
        {
            int sum = 0;
            for (int i = 0; i < Item.Count; i++)
            {
                var item = Item[i];
                sum += item.GetTestCaseCount();
            }

            return sum;
        }

        public void SetParents()
        {
            foreach (var item in Item)
            {
                item.SetParent(null);
            }
        }
    }
}