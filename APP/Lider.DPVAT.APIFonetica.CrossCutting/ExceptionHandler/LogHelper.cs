using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Lider.DPVAT.APIFonetica.Infra.CrossCutting.ExceptionHandler
{
    public class LogHelper
    {
        public void SetWebLogInfo(HttpContext httpContext)
        {
            httpContext.Items["ErrorStatusCode"] = httpContext.Response.StatusCode.ToString();
            httpContext.Items["ErrorRequestPath"] = httpContext.Request.Path.ToString();
            httpContext.Items["ErrorStatusDescription"] = ((HttpStatusCode)httpContext.Response.StatusCode).ToString();
            httpContext.Items["ErrorStatusDescription"] = ((HttpStatusCode)httpContext.Response.StatusCode).ToString();
        }
    }
}
