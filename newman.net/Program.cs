// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using Jint;
using net.postman.model;
using PostMan.Net.Runner;

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
            var content = File.ReadAllText(@"off.json");
            var collection = JsonSerializer.Deserialize<Collection>(content);
            Console.WriteLine($"collection contains {collection.GetTestCaseCount()} test cases");
            return collection;
        }

      

        public static void Main(string[] args)
        {
            var collection = LoadCollection();
            CollectionRunner runner = new CollectionRunner(collection);
            runner.Run();
            //jint();
        }
    }
}