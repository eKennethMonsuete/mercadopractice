using API.ModelErrors;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
       private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
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
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";

                // Definir um status code com base no tipo da exceção
                context.Response.StatusCode = ex switch
                {
                    ArgumentNullException => (int)HttpStatusCode.BadRequest,  
                    ArgumentOutOfRangeException => (int)HttpStatusCode.BadRequest, 
                    KeyNotFoundException => (int)HttpStatusCode.NotFound, 
                    UnauthorizedAccessException => (int)HttpStatusCode.Forbidden, 
                    _ => (int)HttpStatusCode.InternalServerError 
                };

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = _env.IsDevelopment()
                    ? new ApiResponse(context.Response.StatusCode.ToString(), ex.Message, ex.StackTrace?.ToString())
                    : new ApiResponse(context.Response.StatusCode.ToString(), ex.Message, "Internal Server Error");
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }

    }
}
