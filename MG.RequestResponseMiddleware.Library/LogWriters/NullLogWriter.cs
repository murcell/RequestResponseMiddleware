using MG.RequestResponseMiddleware.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG.RequestResponseMiddleware.Library.LogWriters
{
    internal class NullLogWriter : ILogWriter
    {
        public ILogMessageCreator MessageCreator { get; }

        public Task Write(RequestResponseContext context)
        {
           return Task.CompletedTask;
        }
    }
}
