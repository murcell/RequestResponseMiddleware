using MG.RequestResponseMiddleware.Library.Interfaces;
using MG.RequestResponseMiddleware.Library.Middlewares;
using MG.RequestResponseMiddleware.Library.Models;

namespace MG.RequestResponseMiddleware.Library.MessageCreators;

internal class LoggerFactoryMessageCreator : BaseLogMessageCreator,ILogMessageCreator
{
    private readonly LoggingOptions _loggingOptions;

    public LoggerFactoryMessageCreator(LoggingOptions loggingOptions)
    {
        _loggingOptions = loggingOptions;
    }

    public string Create(RequestResponseContext requestResponseContext)
    {
        var sb = new StringBuilder();

        _loggingOptions.LoggingFields.ForEach(f => {
            var value = GetValueByField(requestResponseContext,f);
            sb.AppendFormat("{0}: {1}\n",f,value);
        });

        return sb.ToString();
    }

    

}
