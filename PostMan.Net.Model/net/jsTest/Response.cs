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
        return _response.Data;
    }

    public string status => _response.StatusDescription;
    
}