using MG.RequestResponseMiddleware.Library;
using MG.RequestResponseMiddleware.Library.Interfaces;
using System;

namespace MG.RequestResponseMiddleware.FileLogger.Library.MessageCreator
{
    internal class FileLoggerMessageCreator : ILogMessageCreator
    {
        public string Create(RequestResponseContext context)
        {
            // DateTime: {} - [Duration] [Path+QueryString] [ReqBody] [ResBody]
            string message = $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} - [{context.FormattedCreationTime}] [{context.Url.PathAndQuery}] [{context.RequestBody}] [{context.ResponseBody}]\n";

            return message;
        }
    }
}
