namespace MG.RequestResponseMiddleware.Library.Middlewares;

internal class HandlerRequestResponseLoggingMiddlaware : BaseRequestResponseMiddaware
{
    private readonly Func<RequestResponseContext, Task> reqResResHandler;
    public HandlerRequestResponseLoggingMiddlaware(RequestDelegate next, Func<RequestResponseContext, Task> reqResResHandler, ILogWriter logWriter) :base(next,logWriter)
    {
        this.reqResResHandler = reqResResHandler;
    }
    public async Task Invoke(HttpContext context)
    {
        var reqResContext = await BaseMiddlewareInvoke(context);
        await reqResResHandler.Invoke(reqResContext);
    }
}
