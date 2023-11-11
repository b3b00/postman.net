// See https://aka.ms/new-console-template for more information

using Jint;
using Jint.Native;
using RestSharp;

public class Program
{

    static RestResponse<object> GetData()
    {
        RestClient client = new RestClient();
        var request = new RestRequest("https://calendrier.api.gouv.fr/jours-feries/metropole/2024.json");
        request.Method = Method.Get;
        
        var response = client.Execute<object>(request);

        return response;
    }
    
    

    public static void jintWithModule()
    {
        var data = GetData();

        var engine = new Engine();
        
        var pm= JsValue.FromObjectWithType(engine, new Pm(data), typeof(Pm));
        //engine.SetValue("pm", new Pm(data));
        // Create the module 'lib' with the class MyClass and the variable version
        engine.AddModule("lib", builder => builder
            .ExportType<JConsole>()
            .ExportType<Pm>()
            .ExportType<Response>()
            .ExportValue("pm", pm)
            //.ExportValue("mp",JsValue.FromObject(new Pm(data)))
            .ExportValue("version",15));
        
        
// Create a user-defined module and do something with 'lib'
        engine.AddModule("custom", @"
    import { pm, version } from 'lib';
    //const code = pm.response.code;
    //const json = pm.reponse.json();
const code = pm.Value;
    const x = new JConsole();
    x.log(`status code is >${code}<`);
    export const result = x.log('hello jint '+version);
");

// Import the user-defined module; this will execute the import chain
        var ns = engine.ImportModule("custom");

// The result contains "live" bindings to the module
        var id = ns.Get("result");
    }

    public static void pmJint()
    {
        var data = GetData();
        
        var p = new Person {
            Name = "Mickey Mouse",
            Age = 23,
            Response = new Response(data),
            Address = new Address()
            {
                Number = 1,
                Street = "souris"
            }
        };
        var script = File.ReadAllText("./test.js");
        var engine = new Engine()
            .SetValue("pm",new Pm(data))
            .SetValue("log", new Action<object>(Console.WriteLine))
            .Execute(script);
    }

    public static void Main(string[] args)
    {
        pmJint();
    }
    
}

public class Person 
{
    
    public string Name { get; set; }
    public int Age { get; set; }
    
    public Response Response { get; set; }
    
    public Address Address { get; set; }
}

public class Address
{
    public int Number { get; set; }
    
    public string Street { get; set; }
}