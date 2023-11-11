using RestSharp;
using Jint.Native;
using Jint.Runtime;

public class Pm
{
    public Pm(RestResponse<object> restResponse)
    {
        response = new Response(restResponse);
    }

    public void test(string testName, Action test)
    {
        test();
    }


    private object _subject;

    private object _expectation;
    public Pm Expect(object subject)
    {
        _subject = subject;
        return this;
    }

    public Pm To
    {
        get { return this; }
    }

    public Pm Equal(object expectation)
    {
        if (_subject.Equals(expectation))
        {
            Console.WriteLine("youpiypoupi yeah !");
        }
        else
        {
            Console.Error.WriteLine("ERROR ! ");
        }
        return this;
    }

    public int Value { get; set; } = 1789;

    public Response response { get; set; }
}