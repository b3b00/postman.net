using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
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

    private bool _typeCheck;

    public Pm Be
    {
        get
        {
            _typeCheck = true;
            return this;
        }
    }

    public Pm An(string type)
    {
        bool ok = false;
        
        switch (type)
        {
            case "int":
            {
                ok = _subject is double d && (Math.Round(d) - d == 0.0d);
                break;
            }
            case "string":
            {
                ok = _subject is string;
                break;
            }
            case "double":
            {
                ok = _subject is double || _subject is decimal || _subject is float;
                break;
            }
            case "object":
            {
                ok = !_subject.GetType().IsValueType;
                if (_subject is JsonElement json)
                {
                    ok = ok || json.ValueKind == JsonValueKind.Object;
                }

                break;
            }
        }
        
        if (ok)
        {
            Console.WriteLine("type is OK");
            return this;
        }

        Console.Error.WriteLine($"type error. expecting {type}. found {_subject.GetType().Name}");
        return this;
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

    public Pm Match(string regex, string message)
    {
        Regex r = new Regex(regex);
        var match = r.Match(_subject.ToString());
        if (match.Success)
        {
            Console.WriteLine("regew match OK");
        }
        else
        {
            Console.WriteLine(message+ "  -  "+_subject.ToString());
        }
        return this;
    }
        

    public int Value { get; set; } = 1789;

    public Response response { get; set; }
}