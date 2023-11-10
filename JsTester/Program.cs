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
    
    static void jint()
    {
        var data = GetData();
        
        var engine = new Engine()
            .SetValue("pm", new Pm(data))
            .SetValue("console", new JConsole());
        engine.AddModule("prescript", @"
export const testOuts(left, right) {
    console.log(`testing ${left} againt {right}`);
}
");

        var testme = $@"
import {{ testOuts }} from 'prescript';
    pm.test(
        'this is a test',
 () => {{
console.log('this is a log from a test');
testOuts('toto','tata');
}}
);
";
        var parse = new Esprima.JavaScriptParser().ParseScript(testme);

        engine.Execute(testme);
    }

    public static void jinty()
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
    const code = pm.response.code;
    const json = pm.reponse.json();
    const x = new JConsole();
    x.log(`status code is >${code}<`);
    export const result = x.log('hello jint '+version);
");

// Import the user-defined module; this will execute the import chain
        var ns = engine.ImportModule("custom");

// The result contains "live" bindings to the module
        var id = ns.Get("result");
    }

    public static void Main(string[] args)
    {
        jinty();
    }
    
}