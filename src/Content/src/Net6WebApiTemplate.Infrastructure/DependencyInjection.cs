using FluentEmail.MailKitSmtp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Net6WebApiTemplate.Application.Common.Interfaces;
using Net6WebApiTemplate.Infrastructure.Cache.InMemory;
using Net6WebApiTemplate.Infrastructure.DataProtection;
using Net6WebApiTemplate.Infrastructure.Identity;
using Net6WebApiTemplate.Infrastructure.Notifications.Email;
using Net6WebApiTemplate.Infrastructure.Oauth;
using System.Text;

namespace Net6WebApiTemplate.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastrucutre(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            // Register OAuth services
            services.AddTransient<IJwtTokenManager, JwtTokenManager>();
            services.AddScoped<ISignInManager, SignInManager>();

            // Configure JWT Authentication and Authorization
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = jwtSettings.ValidateIssuer,
                ValidIssuer = jwtSettings.Issuer,
                ValidateAudience = jwtSettings.ValidateAudience,
                ValidAudience = jwtSettings.Audience,
                RequireExpirationTime = jwtSettings.RequireExpirationTime,
                ValidateLifetime = jwtSettings.ValidateLifetime,
                ClockSkew = jwtSettings.Expiration
            };
            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = tokenValidationParameters;
            });

            // Register Data Protection Services
            services.AddDataProtection()
                .SetDefaultKeyLifetime(TimeSpan.FromDays(30));
            services.AddSingleton<IDataEncryption, RouteDataProtection>();

            // Register InMemory Cache services
            services.AddMemoryCache();
            services.AddSingleton<ICacheProvider, CacheProvider>();

            // Register Fluent Email Services
            var emailConfig = new EmailConfiguration();
            configuration.GetSection(nameof(EmailConfiguration)).Bind(emailConfig);

            services.AddFluentEmail(defaultFromEmail: emailConfig.Email)
                .AddRazorRenderer()
                .AddMailKitSender(new SmtpClientOptions()
                {
                    Server = emailConfig.Host,
                    Port = emailConfig.Port,
                    //User = emailConfig.Email,
                    //Password = emailConfig.Password,
                    //RequiresAuthentication = true,
                    PreferredEncoding = "utf-8",
                    UsePickupDirectory = true,
                    MailPickupDirectory = @"C:\Users\mgayle\email",
                    UseSsl = emailConfig.EnableSsl
                });

            // Register Email Notification Service
            services.AddScoped<IEmailNotification, EmailNotificationService>();

            return services;
        }
    }
}