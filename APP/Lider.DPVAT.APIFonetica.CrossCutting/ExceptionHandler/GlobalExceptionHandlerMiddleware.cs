using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace Lider.DPVAT.APIFonetica.Infra.CrossCutting.ExceptionHandler
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                LogHelper logHelper = new LogHelper();
                logHelper.SetWebLogInfo(context);
                _logger.LogError(ex, ex.Message + ex.StackTrace);               
                await HandleExceptionAsync(context, ex);
             
            }
        }

        public static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var json = new
            {
                context.Response.StatusCode,
                CodigoRetorno = 99,
                Message = "Erro ocorrido no Processamento da Request",
            };
            GC.Collect();
            return context.Response.WriteAsync(JsonConvert.SerializeObject(json));
        }

    }
}
