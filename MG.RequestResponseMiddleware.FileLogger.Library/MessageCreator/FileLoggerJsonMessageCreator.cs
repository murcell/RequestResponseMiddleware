using MG.RequestResponseMiddleware.Library;
using MG.RequestResponseMiddleware.Library.Interfaces;
using System;
using System.Text.Json;

namespace MG.RequestResponseMiddleware.FileLogger.Library.MessageCreator
{
    internal class FileLoggerJsonMessageCreator : ILogMessageCreator
    {
        public string Create(RequestResponseContext context)
        {
            return $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} - {JsonSerializer.Serialize(context,new JsonSerializerOptions { WriteIndented=true })}";
        }
    }
}
