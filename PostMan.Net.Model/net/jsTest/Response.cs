using System.Text.Json;
using Jint.Native;
using Jint.Runtime;
using RestSharp;

public class Response 
{

    private RestResponse<object> _response;

    public Response(RestResponse<object> response) 
    {
        _response = response;
    }

    public int code => (int)_response.StatusCode;

    public object json()
    {
        var t = (_response.Data is JsonElement ? (JsonElement)_response.Data : default);
        var x =  t.Deserialize<Dictionary<string,object>>();
        return x;
    }

    public string status => _response.StatusDescription;
    
}