// Namespace imports
using CurrencyConverterTest.Core.Entities; 
using CurrencyConverterTest.Repository.RequestLogRepository;
using System.Text;

namespace WebApi.Helpers
{
    // Middleware for logging requests and responses
    public class LoggingMiddleware
    {
        // Request delegate for next middleware in pipeline
        private readonly RequestDelegate _next;

        // Logger factory for creating logger
        private readonly ILogger _logger;

        // Constructor injection
        public LoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<LoggingMiddleware>();
        }

        // Method invoked in middleware pipeline
        public async Task Invoke(HttpContext context, IRequestLogRepository requestLogRepository)
        {
            try
            {
                // Log request details
                _logger.LogInformation("Request: {method} {url}",
                                        context.Request.Method, context.Request.Path);

                // Create and save request log
                RequestLog requestLog = await CreateRequestLog(context);
                await requestLogRepository.AddRequestLog(requestLog);

                // Pass request to next middleware
                await _next(context);

                // Log response status code
                _logger.LogInformation("Response: {statusCode}",
                                        context.Response.StatusCode);

            }
            catch (Exception ex)
            {
                // Log any errors
                _logger.LogError(ex, "Error in middleware");
                throw;
            }
        }

        // Helper method to create request log
        private async Task<RequestLog> CreateRequestLog(HttpContext context)
        {
            string method = context.Request.Method;
            string url = context.Request.Path;
            string body = string.Empty;
            int statusCode = context.Response.StatusCode;
            DateTime datestamp = DateTime.Now;

            if (context.Request.Method == HttpMethods.Post && context.Request.ContentLength > 0)
            {
                context.Request.EnableBuffering();
                body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                context.Request.Body.Position = 0;  //rewinding the stream to 0
            }

            var requestLog = new RequestLog
            {
                Method = context.Request.Method,
                Url = url,
                RequestBody = body,
                StatusCode = statusCode,
                Timestamp = datestamp
            };

            return requestLog;

        }

    }

}