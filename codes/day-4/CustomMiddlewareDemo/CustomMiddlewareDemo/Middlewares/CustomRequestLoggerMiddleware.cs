namespace CustomMiddlewareDemo.Middlewares
{
    public class CustomRequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomRequestLoggerMiddleware> _logger;

        public CustomRequestLoggerMiddleware(RequestDelegate next, ILogger<CustomRequestLoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation($"Handling request: {context.Request.Method}, {context.Request.Path} at {DateTime.Now.ToString()}");
            await _next(context);
        }
    }
}
