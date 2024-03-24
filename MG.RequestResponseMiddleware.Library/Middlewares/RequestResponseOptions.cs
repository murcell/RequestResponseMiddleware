using MG.RequestResponseMiddleware.Library.Middlewares;
using Microsoft.Extensions.Logging;

namespace MG.RequestResponseMiddleware.Library;

public class RequestResponseOptions
{
    internal ILoggerFactory LoggerFactory;
    internal Func<RequestResponseContext,Task> ReqResHandler {  get; set; }
    internal LoggingOptions LoggingOptions;

    public void UseHandler(Func<RequestResponseContext,Task> resResHandler)
    {
        ReqResHandler = resResHandler;
    }

    public void UseLogger(ILoggerFactory loggerFactory, Action<LoggingOptions> loggingAction)
    {
        LoggingOptions = new LoggingOptions();
        loggingAction(LoggingOptions);
        this.LoggerFactory = loggerFactory;
    }

}
