using System.Net;
using Services.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace UniversityApp.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                switch (ex)
                {
                   
                    case DublicateEntityException e:
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsJsonAsync(new { error = ex.Message });
                        break;
                   
                    default:
                        await context.Response.WriteAsJsonAsync(new { error = "Bilinmedik xeta bas verdi!" });
                        break;
                }
                var message = ex.Message;
                var errors = new List<RestExceptionError>();
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                if (ex is RestException rex)
                {
                    message = rex.Message;
                    errors = rex.Errors;
                    context.Response.StatusCode = rex.Code;
                }

                context.Response.ContentType = "application/json";
                var response = new { message, errors };
                var jsonResponse = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
