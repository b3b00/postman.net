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

    public int Value { get; set; }= 1789;
    
    public Response response { get; set; }


    
}