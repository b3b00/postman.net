// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using Jint;
using net.postman.model;

namespace net.postman.newman
{

    public class Pm
    {
        public void test(string testName, Action test)
        {
            Console.WriteLine($"testing [{testName}");
            test();
        }
    }

    public class JConsole
    {
        public void log(string message)
        {
            Console.WriteLine(message);
        }
    }
    
    
    public class Program
    {
        public static Collection LoadCollection()
        {
            Console.WriteLine("Hello, World!");
            var content = File.ReadAllText(@"C:\Users\olduh\dev\PostMan.Net\collection.json");
            var collection = JsonSerializer.Deserialize<Collection>(content);
            Console.WriteLine($"collection contains {collection.GetTestCaseCount()}");
            return collection;
        }

        public static void jint()
        {
            var engine = new Engine()
                .SetValue("pm", new Pm())
                .SetValue("console",new JConsole());
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
        
        public static void Main(string[] args)
        {
            // var collection = LoadCollection();
            jint();
        }
    }
}