public class Pm
{
    public void test(string testName, Action test)
    {
        Console.WriteLine($"testing [{testName}");
        test();
    }
}