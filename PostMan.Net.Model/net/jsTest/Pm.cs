using RestSharp;
using Jint.Native;
using Jint.Runtime;

public class Pm : JsValue
{


    
   
    public Pm(RestResponse<object> restResponse) : base(Types.Object) 
    {
        response = new Response(restResponse);
    }
    public void test(string testName, Action test)
    {
        test();
    }
    
    public Response response { get; set; }


    public override object? ToObject()
    {
        return this ;
    }
}