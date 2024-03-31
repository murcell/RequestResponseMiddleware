using MG.RequestResponseMiddleware.FileLogger.Library.LogWriters;
using MG.RequestResponseMiddleware.FileLogger.Library.Models;
using MG.RequestResponseMiddleware.Library.Interfaces;
using MG.RequestResponseMiddleware.Library.Middlewares;
using Microsoft.AspNetCore.Builder;
using System;

namespace MG.RequestResponseMiddleware.FileLogger.Library
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddMGRequestResponseFileLoggerMiddleware(this IApplicationBuilder appBuilder, Action<FileLoggingOptions> optionAction)
        {
            var opt=new FileLoggingOptions();
            optionAction(opt);

            ILogWriter logWriter = new FileLogWriter(opt);
            appBuilder.UseMiddleware<RequestResponseLoggingMiddleware>(logWriter);

            return appBuilder;
        }

    }
}
