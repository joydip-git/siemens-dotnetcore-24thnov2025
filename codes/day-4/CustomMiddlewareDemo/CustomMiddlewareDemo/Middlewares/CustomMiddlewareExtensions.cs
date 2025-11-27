namespace CustomMiddlewareDemo.Middlewares
{
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomRequestLogger(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomRequestLoggerMiddleware>();
        }
        public static IApplicationBuilder UseCustomResponseLogger(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomResponseLoggerMiddleware>();
        }
    }
}
