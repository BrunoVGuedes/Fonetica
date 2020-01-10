using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace Lider.DPVAT.APIFonetica
{
    public class ExceptionHandler : ExceptionFilterAttribute
    {

        public override void OnException(ExceptionContext context)
        {
            Exception ex = context.Exception;
            Logger.AddLog(ex.Message);
        }

    }

}
