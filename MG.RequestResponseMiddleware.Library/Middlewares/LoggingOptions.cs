using Microsoft.Extensions.Logging;

namespace MG.RequestResponseMiddleware.Library.Middlewares;

public class LoggingOptions
{
    private List<LogFileds> loggingFields;
    public LogLevel LogLevel { get; set; } = LogLevel.Information;
    public string LoggerCategoryName { get; set; } = "RequestResponseLoggingMiddleware";
    public List<LogFileds> LoggingFields 
    {   
        get { return loggingFields ??= new List<LogFileds>(); }
        set { loggingFields = value; }
    }
    
}

public enum LogFileds
{
    Request,
    Response,
    HostName,
    Path,
    QueryString,
    ResponseTiming,
    RequestLength,
    ResponseLength
}
