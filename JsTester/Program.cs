// See https://aka.ms/new-console-template for more information

using Jint;

public class Program
{

    static void jint()
    {
        var engine = new Engine()
            .SetValue("pm", new Pm())
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

        var engine = new Engine();
        // Create the module 'lib' with the class MyClass and the variable version
        engine.AddModule("lib", builder => builder
            .ExportType<JConsole>()
            .ExportValue("version", 15)
        );

// Create a user-defined module and do something with 'lib'
        engine.AddModule("custom", @"
    import { JConsole, version } from 'lib';
    const x = new JConsole();
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