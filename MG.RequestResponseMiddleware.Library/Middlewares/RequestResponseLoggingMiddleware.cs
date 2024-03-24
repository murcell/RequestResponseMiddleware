namespace MG.RequestResponseMiddleware.Library.Middlewares;

public class RequestResponseLoggingMiddleware : BaseRequestResponseMiddaware
{
    public RequestResponseLoggingMiddleware(RequestDelegate next, ILogWriter logWriter):base(next,logWriter)
    {
    }

    public async Task Invoke(HttpContext context)
    {
        var reqResContext = await BaseMiddlewareInvoke(context);

        //await logWriter?.Write(reqResContext);
    }
   
}
