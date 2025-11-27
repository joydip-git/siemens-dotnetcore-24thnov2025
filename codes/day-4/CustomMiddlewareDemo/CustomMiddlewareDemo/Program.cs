using CustomMiddlewareDemo.Middlewares;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddMvcCore();
//builder.Services.AddAuthorization();
//builder.Services.AddRouting();
//builder.Services.AddEndpointsApiExplorer();

//RESTful ASP.NET Core Web API project with minimal setup
//registering services required for controllers based RESTful API
builder.Services.AddControllers();

// Alternatively, for MVC with views support
//builder.Services.AddControllersWithViews();

// Alternatively, for Razor Pages support
//builder.Services.AddRazorPages();

// Alternatively, for Blazor Server support
//builder.Services.AddServerSideBlazor();

//all middleware components are configured in the HTTP request pipeline here (instances are created by the DI container)
var app = builder.Build();

//InvokeAsync method of every middleware component in the pipeline is called for each HTTP request.
//app.UseRouting();
//app.UseAuthentication();
//app.UseAuthorization();
app.UseCustomRequestLogger();
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    IQueryCollection queryValues = context.Request.Query;
    if (queryValues["x"] != "100")
    {
        context.Response.StatusCode = 403; // Forbidden
        return;
    }
    //short-circuiting the pipeline, not calling the next middleware component
    await next(context);
});

app.MapControllers();
app.UseCustomResponseLogger();

app.Run();
