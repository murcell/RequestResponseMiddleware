using MG.RequestResponseMiddleware.Library.Interfaces;
using MG.RequestResponseMiddleware.Library.LogWriters;
using MG.RequestResponseMiddleware.Library.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace MG.RequestResponseMiddleware.Library;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder AddMGRequestResponseMiddleware(this IApplicationBuilder appBuilder, Action<RequestResponseOptions> optionAction)
    {
        var opt=new RequestResponseOptions();
        optionAction(opt);

        ILogWriter logWriter = opt.LoggerFactory is null? new NullLogWriter(): new LoggerFactoryLogWriter(opt.LoggerFactory, opt.LoggingOptions);

        if (opt.ReqResHandler is not null)
            appBuilder.UseMiddleware<HandlerRequestResponseLoggingMiddlaware>(opt.ReqResHandler, logWriter);
        else
            appBuilder.UseMiddleware<RequestResponseLoggingMiddleware>(opt.ReqResHandler, logWriter);


        return appBuilder;
    }

}
