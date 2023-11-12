using System.Text.Json;
using System.Text.RegularExpressions;
using RestSharp;

namespace PostMan.Net.Model.net.jsTest;

public class Pm
{
    public Pm(string name, RestResponse<object> restResponse)
    {
        response = new Response(restResponse);
        _results = new List<PmTestResult>();
        _status = PmStatus.None;
    }

    private string _name; 
    
    public int Value { get; set; } = 1789;

    public Response response { get; set; }

    private PmStatus _status;

    public PmStatus Status => _status;
   
    
    private IList<PmTestResult> _results;

    public IList<PmTestResult> Results => _results;

    private PmTestResult _currentTestResult;

    private object _subject;

    
    

    private bool _typeCheck;
    
    private bool _negation;


    public void Reset()
    {
        _subject = null;
        _typeCheck = false;
        _negation = false;
    }
    
    public void test(string testName, Action test)
    {
        _currentTestResult = new PmTestResult(testName);
        test();
        if (_status != PmStatus.Failure)
        {
            _status = _currentTestResult.Status;
        }
        _results.Add(_currentTestResult);
    }

    
    
    public Pm Expect(object subject)
    {
        _subject = subject;
        return this;
    }

    public Pm To => this;

    public Pm Not
    {
        get
        {
            _negation = true;
            return this;
        }
    }

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
                if (!ok && _subject is JsonElement json && json.ValueKind == JsonValueKind.String)
                {
                    ok = true;
                }
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
        
        if (!ok || (_negation && ok))
        {
            _currentTestResult.SetStatus(PmStatus.Failure);
            _currentTestResult.AddMessage($"type error. expecting {type}. found {_subject.GetType().Name}");
            return this;
        }
        _currentTestResult.SetStatus(PmStatus.Success);
        return this;
    }

    public Pm A(string type) => An(type);
    
    public Pm Equal(object expectation)
    {
        var equals = _subject.Equals(expectation); 
        if (!equals || (_negation && equals))
        {
            _currentTestResult.SetStatus(PmStatus.Failure);
            _currentTestResult.AddMessage($"Expecting {expectation} but found {_subject}");
            return this;
        }
        _currentTestResult.SetStatus(PmStatus.Success);
        return this;
    }

    public Pm Eq(object expectation) => Equal(expectation);
    
    public Pm Empty {
        get
        {
            bool empty = string.IsNullOrEmpty(_subject?.ToString()); 
            if ((!_negation && !empty) || (_negation && empty))
            {
                _currentTestResult.SetStatus(PmStatus.Failure);
                _currentTestResult.AddMessage($"was expected to be ({(_negation ? "not":"")} empty");
                return this;
            }
            _currentTestResult.SetStatus(PmStatus.Success);
            return this;
        }
    }

    public Pm Match(string regex, string message)
    {
        Regex r = new Regex(regex.Substring(1,regex.Length-2).Replace("\\\\","\\"));
        var match = r.Match(_subject.ToString());
        if (!match.Success || (_negation && match.Success))
        {
            _currentTestResult.SetStatus(PmStatus.Failure);
            _currentTestResult.AddMessage($"{message} - {_subject.ToString()}");
            return this;
        }
        _currentTestResult.SetStatus(PmStatus.Success);
        return this;
    }
        

    
}