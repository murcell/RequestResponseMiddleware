using MG.RequestResponseMiddleware.Library.Middlewares;
using MG.RequestResponseMiddleware.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG.RequestResponseMiddleware.Library.MessageCreators
{
    public abstract class BaseLogMessageCreator
    {
        protected string GetValueByField(RequestResponseContext _requestResponseContext,LogFileds field)
        {
            return field switch
            {
                LogFileds.Request => _requestResponseContext.RequestBody,
                LogFileds.Response => _requestResponseContext.ResponseBody,
                LogFileds.QueryString => _requestResponseContext.context?.Request?.QueryString.Value,
                LogFileds.Path => _requestResponseContext.context?.Request?.Path.Value,
                LogFileds.HostName => _requestResponseContext.context?.Request?.Host.Value,
                LogFileds.ResponseLength => _requestResponseContext.ResponseLength.ToString(),
                LogFileds.RequestLength => _requestResponseContext.RequestLength.ToString(),
                LogFileds.ResponseTiming => _requestResponseContext.FormattedCreationTime,
                _ => string.Empty
            };
        }
    }
}
