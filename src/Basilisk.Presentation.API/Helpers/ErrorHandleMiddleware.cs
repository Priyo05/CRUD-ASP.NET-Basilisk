using System.Net;
using System.Text.Json;

namespace Basilisk.Presentation.API.Helpers;
public class ErrorHandleMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandleMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            switch (error)
            {
                case KeyNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }


            //custom exception
            var result = new
            {
                title = error.Message,
                status = (int)response.StatusCode
            };

            response.WriteAsync(JsonSerializer.Serialize(result))
                    .GetAwaiter().GetResult();

        }
    }   



}


