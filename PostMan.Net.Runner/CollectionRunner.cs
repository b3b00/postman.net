using Jint;
using net.postman.model;
using PostMan.Net.Model.net.jsTest;
using RestSharp;

namespace PostMan.Net.Runner;

public static class Extensions
{
    public static string Capitalize(this string s)
    {
        var c = s.ToLower();
        return Char.ToUpper(c[0]) + c.Substring(1);
    }
}

public class CollectionRunner
{
    public Collection Collection { get; private set; }
    
    public IList<Pm> TestCaseResults { get; private set; }
    
    public CollectionRunner(Collection collection)
    {
        Collection = collection;
        TestCaseResults = new List<Pm>();
    }

    public void Run()
    {
        foreach (var item in Collection.Item)
        {
            Run(item);
        }
        
    }

    private void Run(Item item)
    {
        if (item.IsFolder)
        {
            RunFolder(item);
        }
        else if (item.IsTestCase)
        {
            RunTestCase(item);
        }
    }

    
    private void RunFolder(Item item)
    {
        foreach (var subItem in item.Items)
        {
            Run(subItem);
        }
    }
    private void RunTestCase(Item item)
    {
        Console.WriteLine($"Test case {item.Name}");
        RestClient client = new RestClient();
        var request = new RestRequest(item.Request.Url.Raw);
        if (Enum.TryParse<Method>(item.Request.Method.Capitalize(), out var method))
        {
            request.Method = method;
        }
        else
        {
            request.Method = Method.Get;
        }

        if (request.Method == Method.Post || request.Method == Method.Put)
        {
            request.AddBody(item.Request.Body.Raw);
        }

        var response = client.Execute<object>(request);
        Console.WriteLine($"status : {response.StatusCode}");
        Console.WriteLine($"response :{response.Data}");

        Pm pm = new Pm(item.Name,response);
        var engine = new Engine()
            .SetValue("pm",pm)
            .SetValue("log", new Action<object>(Console.WriteLine));

        foreach (var evt in item.Event)
        {
            var script = string.Join('\n', evt.Script.Exec);
            engine.Execute(script);
        }   
        
        TestCaseResults.Add(pm);
        
    }
}