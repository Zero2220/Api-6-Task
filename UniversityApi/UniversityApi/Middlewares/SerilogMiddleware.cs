using Serilog;
using System.Text;

public class SerilogMiddleware
{
    private readonly RequestDelegate _next;

    public SerilogMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        
        Log.Information($"Request {context.Request?.Method}: {context.Request?.Path.Value}");

       
        string requestBodyPayload = await ReadRequestBody(context);
        Log.Information($"Request Payload: {requestBodyPayload}");

        
        var originalBodyStream = context.Response.Body;

        using (var responseBody = new MemoryStream())
        {
            
            context.Response.Body = responseBody;

            await _next(context);

           
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            string responseBodyPayload = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            Log.Information($"Response {context.Response?.StatusCode}: {responseBodyPayload}");

           
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }

    private async Task<string> ReadRequestBody(HttpContext context)
    {
        context.Request.EnableBuffering();

        var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
        await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
        string bodyAsText = Encoding.UTF8.GetString(buffer);
        context.Request.Body.Seek(0, SeekOrigin.Begin);

        return bodyAsText;
    }
}