using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Net6WebApiTemplate.Api.Filters;
using Net6WebApiTemplate.Application;
using Net6WebApiTemplate.Application.HealthChecks;
using Net6WebApiTemplate.Infrastructure;
using Net6WebApiTemplate.Persistence;
using Newtonsoft.Json;
using NLog.Web;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure NLog
var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.SetMinimumLevel(LogLevel.Trace);
})
.UseNLog();  // NLog: Setup NLog for Dependency injection

// loading appsettings.json based on environment configurations
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    var env = hostingContext.HostingEnvironment;

    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.Local.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

    if (env.EnvironmentName == "Local")
    {
        var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
        if (appAssembly != null)
        {
            config.AddUserSecrets(appAssembly, optional: true);
        }
    }

    config.AddEnvironmentVariables();

    if (args != null)
    {
        config.AddCommandLine(args);
    }
});

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastrucutre(builder.Configuration, builder.Environment);
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews(options =>
    options.Filters.Add<ApiExceptionFilterAttribute>());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable NWebSec Security Headers
app.UseXContentTypeOptions();
app.UseXXssProtection(options => options.EnabledWithBlockMode());
app.UseXfo(options => options.SameOrigin());
app.UseReferrerPolicy(options => options.NoReferrerWhenDowngrade());

// Feature-Policy security header
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Feature-Policy", "geolocation 'none'; midi 'none';");
    await next.Invoke();
});

app.UseHttpsRedirection();

app.UseAuthorization();

// Enable Health Check Middleware
app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";

        var response = new HealthCheckResponse()
        {
            Status = report.Status.ToString(),
            Checks = report.Entries.Select(x => new HealthCheck
            {
                Status = x.Value.Status.ToString(),
                Component = x.Key,
                Description = x.Value.Description == null && x.Key.Contains("DbContext") ? app.Environment.EnvironmentName + "-db" : x.Value.Description
            }),
            Duration = report.TotalDuration
        };

        await context.Response.WriteAsync(text: JsonConvert.SerializeObject(response, Formatting.Indented));
    }
});

app.MapControllers();

app.Run();