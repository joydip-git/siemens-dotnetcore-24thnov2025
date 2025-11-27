namespace CustomMiddlewareDemo.Middlewares
{
    public class CustomResponseLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomResponseLoggerMiddleware> _logger;

        public CustomResponseLoggerMiddleware(RequestDelegate next, ILogger<CustomResponseLoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {           
            await _next(context);
            _logger.LogInformation($"Handling response: {context.Request.Method}, {context.Request.Path} at {DateTime.Now.ToString()}");
        }
    }
}
