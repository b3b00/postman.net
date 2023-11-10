using Jint.Native;
using Jint.Runtime;
using RestSharp;

public class Response : JsValue
{

    private RestResponse<object> _response;

    public Response(RestResponse<object> response) : base(Types.Object)
    {
        _response = response;
    }

    public int code => (int)_response.StatusCode;

    public object json()
    {
        return _response.Data;
    }

    public string status => _response.StatusDescription;

    public override object? ToObject()
    {
        return this;
    }
}