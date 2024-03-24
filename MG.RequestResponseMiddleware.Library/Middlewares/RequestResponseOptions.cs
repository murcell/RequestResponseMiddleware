using MG.RequestResponseMiddleware.Library.Models;
using Microsoft.Extensions.Logging;

namespace MG.RequestResponseMiddleware.Library.Middlewares;

public class RequestResponseOptions
{
    internal ILoggerFactory LoggerFactory;
    internal Func<RequestResponseContext,Task> ResResHandler {  get; set; }
    internal LoggingOptions LoggingOptions;

    public void UseHandler(Func<RequestResponseContext,Task> resResHandler)
    {
        ResResHandler = resResHandler;
    }

    public void UseLogger(ILoggerFactory loggerFactory, Action<LoggingOptions> loggingAction)
    {
        LoggingOptions = new LoggingOptions();
        loggingAction(LoggingOptions);
        this.LoggerFactory = loggerFactory;
    }

}
