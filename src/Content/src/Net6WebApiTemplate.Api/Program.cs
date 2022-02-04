using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.OpenApi.Models;
using Net6WebApiTemplate.Api.Filters;
using Net6WebApiTemplate.Api.Options;
using Net6WebApiTemplate.Api.Services;
using Net6WebApiTemplate.Application;
using Net6WebApiTemplate.Application.Common.Interfaces;
using Net6WebApiTemplate.Infrastructure;
using Net6WebApiTemplate.Persistence;
using Net6WebApiTemplate.Persistence.Seeders;
using NLog.Web;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Globalization;
using System.Reflection;
using System.Text.Json.Serialization;

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

//--- Add services to the container.
// needed to load configuration from appsettings.json
builder.Services.AddOptions();

// needed to store rate limit counters and ip rules
builder.Services.AddMemoryCache();

// load general configuration from appsettings.json
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));

// inject counter and rules stores
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

// configuration (resolvers, counter key builders)
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

// Register CurrentUserService
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

// Register and configure localization services
builder.Services.AddLocalization(options => options.ResourcesPath = "Localization");
builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

// Add library project references
builder.Services.AddApplication();
builder.Services.AddInfrastrucutre(builder.Configuration, builder.Environment);
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(options=>
options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Swagger OpenAPI Configuration
var swaggerDocOptions = new SwaggerDocOptions();
builder.Configuration.GetSection(nameof(SwaggerDocOptions)).Bind(swaggerDocOptions);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions<SwaggerGenOptions>()
    .Configure<IApiVersionDescriptionProvider>((swagger, service) =>
    {
        foreach (ApiVersionDescription description in service.ApiVersionDescriptions)
        {
            swagger.SwaggerDoc(description.GroupName, new OpenApiInfo
            {
                Title = swaggerDocOptions.Title,
                Version = description.ApiVersion.ToString(),
                Description = swaggerDocOptions.Description,
                TermsOfService = new Uri("https://github.com/marlonajgayle/Net6WebApiTemplate/blob/develop/LICENSE.md"),
                Contact = new OpenApiContact
                {
                    Name = swaggerDocOptions.Organization,
                    Email = swaggerDocOptions.Email
                },
                License = new OpenApiLicense
                {
                    Name = "MIT",
                    Url = new Uri("https://github.com/marlonajgayle/Net6WebApiTemplate")
                }
            });
        }

        var security = new Dictionary<string, IEnumerable<string>>
        {
            {"Bearer", new string[0]}
        };

        swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme.",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        swagger.OperationFilter<AuthorizeCheckOperationFilter>();

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        swagger.IncludeXmlComments(xmlPath);

    });

builder.Services.AddControllersWithViews(options =>
    options.Filters.Add<ApiExceptionFilterAttribute>());

// Configure HTTP Strict Transport Security Protocol (HSTS)
builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(1);
});

// Register and configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy",
        options =>
        {
            options.WithOrigins(builder.Configuration.GetSection("Origins").Value)
            .WithMethods("OPTIONS", "GET", "POST", "PUT", "DELETE")
            .AllowCredentials();

        });
});

// Register and Configure API versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

// Register and configure API versioning explorer
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;

});

//-- Configure the HTTP request pipeline.
var app = builder.Build();
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local") || app.Environment.IsEnvironment("Test"))
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Seed test data
    var context = app.Services.GetService<INet6WebApiTemplateDbContext>();
    ProductSeeder.Initialize(context).Wait();
    CategorySeeder.Initialize(context).Wait();    
}
else
{
    // Enable HTTP Strict Transport Security Protocol (HSTS)
    app.UseHsts();
}

// List of supported cultures for localization used in RequestLocalizationOptions
var supportedCultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("es")
};

// Configure RequestLocalizationOptions with supported culture
var requestLocalizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en"),

    // Formatting numbers, date etc.
    SupportedCultures = supportedCultures,

    // UI strings that are localized
    SupportedUICultures = supportedCultures
};

// Enable Request Localization
app.UseRequestLocalization(requestLocalizationOptions);

// Enable NWebSec Security Response Headers
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

// Enable IP Rate Limiting Middleware
app.UseIpRateLimiting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health", new HealthCheckOptions()
    {
        ResponseWriter = HealthCheckResponseWriter.WriteHealthCheckResponse,
        AllowCachingResponses = false,

    });
});

app.MapControllers();

app.Run();

public partial class Program { }