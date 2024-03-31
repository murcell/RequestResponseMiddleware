using MG.RequestResponseMiddleware.FileLogger.Library.MessageCreator;
using MG.RequestResponseMiddleware.FileLogger.Library.Models;
using MG.RequestResponseMiddleware.Library;
using MG.RequestResponseMiddleware.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG.RequestResponseMiddleware.FileLogger.Library.LogWriters
{
    internal class FileLogWriter : ILogWriter
    {
        private readonly FileLoggingOptions _fileLoggingOptions;

        internal FileLogWriter(FileLoggingOptions fileLoggingOptions)
        {
            _fileLoggingOptions = fileLoggingOptions;
            MessageCreator = fileLoggingOptions.UseJsonFormat ? new FileLoggerJsonMessageCreator() : new FileLoggerMessageCreator();

            fileLoggingOptions.ValidatePath();
        }

        public ILogMessageCreator MessageCreator { get; }

        public async Task Write(RequestResponseContext context)
        {
            var message = MessageCreator.Create(context);
            var fullPath = _fileLoggingOptions.GetFullFilePath();
            await File.AppendAllTextAsync(fullPath, message);
        }
    }
}
