namespace FirstCoreWebApp
{
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomLoggerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomLoggerMiddleware>();
        }
    }
}
