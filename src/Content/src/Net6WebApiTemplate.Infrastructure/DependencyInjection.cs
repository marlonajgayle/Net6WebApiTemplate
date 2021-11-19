using FluentEmail.MailKitSmtp;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Net6WebApiTemplate.Application.Common.Interfaces;
using Net6WebApiTemplate.Infrastructure.DataProtection;
using Net6WebApiTemplate.Infrastructure.Notifications.Email;

namespace Net6WebApiTemplate.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastrucutre(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {

            // Register Data Protection Services
            services.AddDataProtection()
                .SetDefaultKeyLifetime(TimeSpan.FromDays(30));
            services.AddSingleton<IDataEncryption, RouteDataProtection>();

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