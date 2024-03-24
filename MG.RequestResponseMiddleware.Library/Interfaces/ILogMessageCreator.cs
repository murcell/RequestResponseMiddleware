using MG.RequestResponseMiddleware.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG.RequestResponseMiddleware.Library.Interfaces;

public interface ILogMessageCreator
{
    string Create(RequestResponseContext requestResponseContext);
}
