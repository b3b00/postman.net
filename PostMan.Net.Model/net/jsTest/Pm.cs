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
            case "array":
            {
                ok = !_subject.GetType().IsValueType;
                if (_subject is JsonElement json)
                {
                    ok = ok || json.ValueKind == JsonValueKind.Array;
                }

                break;
            }
        }
        
        if (!ok)
        {
            Console.Error.WriteLine($"type error. expecting {type}. found {_subject.GetType().Name}");
        }

        return this;
    }

    public Pm A(string type) => An(type);
    
    public Pm Equal(object expectation)
    {
        if (!_subject.Equals(expectation))
        {
            Console.Error.WriteLine($"Expecting {expectation} but found {_subject}");
        }

        return this;
    }

    public Pm Eq(object expectation) => Equal(expectation);
    

    public Pm Match(string regex, string message)
    {
        Regex r = new Regex(regex.Substring(1,regex.Length-2).Replace("\\\\","\\"));
        var match = r.Match(_subject.ToString());
        if (!match.Success)
        {
            Console.WriteLine(message+ "  -  "+_subject.ToString());
        }
        return this;
    }
        

    public int Value { get; set; } = 1789;

    public Response response { get; set; }
}