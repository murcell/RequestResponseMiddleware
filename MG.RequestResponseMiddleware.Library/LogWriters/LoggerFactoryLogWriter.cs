using MG.RequestResponseMiddleware.Library.Interfaces;
using MG.RequestResponseMiddleware.Library.MessageCreators;
using MG.RequestResponseMiddleware.Library.Middlewares;
using MG.RequestResponseMiddleware.Library.Models;
using Microsoft.Extensions.Logging;

namespace MG.RequestResponseMiddleware.Library.LogWriters;

internal class LoggerFactoryLogWriter : ILogWriter
{
    private readonly ILogger Logger;
    private readonly LoggingOptions _loggingOptions;
    public ILogMessageCreator MessageCreator { get; }

    internal LoggerFactoryLogWriter(ILoggerFactory loggerFactory, LoggingOptions loggingOptions)
    {
        Logger = loggerFactory.CreateLogger(loggingOptions.LoggerCategoryName);
        _loggingOptions = loggingOptions;
        MessageCreator = new LoggerFactoryMessageCreator(_loggingOptions);
    }
    
    public async Task Write(RequestResponseContext context)
    {
        var message = MessageCreator.Create(context);
        Logger.Log(_loggingOptions.LogLevel, message);
        await Task.CompletedTask;
    }
}
