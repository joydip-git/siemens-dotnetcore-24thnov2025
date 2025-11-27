namespace FirstCoreWebApp
{
    public class CustomLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomLoggerMiddleware> _logger;
        public CustomLoggerMiddleware(RequestDelegate next, ILogger<CustomLoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _logger.LogInformation("miidleware created...");
        }
        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation("request entered Custom Logger Middleware at " + DateTime.Now.ToString());
            _logger.LogInformation($"Request Thread: {Environment.CurrentManagedThreadId}");

            await _next(context);

            _logger.LogInformation($"Response Thread: {Environment.CurrentManagedThreadId}");

            _logger.LogInformation("response entered Custom Logger Middleware at " + DateTime.Now.ToString());
        }
        //public async Task InvokeAsync(HttpContext context, RequestDelegate _next, ILogger<CustomLoggerMiddleware> _logger)
        //{
        //_logger.LogInformation("request entered Custom Logger Middleware at "+DateTime.Now.ToString());

        //    await _next(context);

        //_logger.LogInformation("response entered Custom Logger Middleware at " + DateTime.Now.ToString());
        //}
    }

}
