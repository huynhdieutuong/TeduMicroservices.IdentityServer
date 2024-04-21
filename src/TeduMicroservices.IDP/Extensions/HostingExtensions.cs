using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using TeduMicroservices.IDP.Infrastructure.Domains;
using TeduMicroservices.IDP.Infrastructure.Domains.Interfaces;
using TeduMicroservices.IDP.Infrastructure.Repositories;
using TeduMicroservices.IDP.Infrastructure.Repositories.Interfaces;
using TeduMicroservices.IDP.Services.EmailService;

namespace TeduMicroservices.IDP.Extensions;

internal static class HostingExtensions
{
    public static void AddAppConfigurations(this WebApplicationBuilder builder)
    {
        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true,
                reloadOnChange: true)
            .AddEnvironmentVariables();
    }

    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddConfigurationSettings(builder.Configuration);

        // uncomment if you want to add a UI
        builder.Services.AddRazorPages();

        builder.Services.ConfigureIdentity(builder.Configuration);
        builder.Services.ConfigureIdentityServer(builder.Configuration);
        builder.Services.ConfigureCookiePolicy();
        builder.Services.ConfigureCors();
        builder.Services.ConfigureSwagger(builder.Configuration);
        builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        builder.Services.AddControllers(config =>
        {
            config.RespectBrowserAcceptHeader = true;
            config.ReturnHttpNotAcceptable = true;
            config.Filters.Add(new ProducesAttribute("application/json", "text/plain", "text/json"));
        });

        // Add services to the container
        builder.Services.AddScoped<IEmailSender, SmtpMailService>();
        builder.Services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
        builder.Services.AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
        builder.Services.AddScoped(typeof(IPermissionRepository), typeof(PermissionRepository));
        builder.Services.AddScoped(typeof(IRepositoryManager), typeof(RepositoryManager));

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // uncomment if you want to add a UI
        app.UseStaticFiles();

        app.UseCors("CorsPolicy");

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tedu Identity API");
        });

        app.UseRouting();

        app.UseCookiePolicy();
        app.UseIdentityServer();

        // uncomment if you want to add a UI
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapRazorPages().RequireAuthorization();
        });

        return app;
    }

    public static void ConfigureSerilog(this ConfigureHostBuilder host)
    {
        host.UseSerilog((context, configuration) =>
        {
            var elasticUri = context.Configuration.GetValue<string>("ElasticConfiguration:Uri");
            var username = context.Configuration.GetValue<string>("ElasticConfiguration:Username");
            var password = context.Configuration.GetValue<string>("ElasticConfiguration:Password");
            var applicationName = context.HostingEnvironment.ApplicationName?.ToLower().Replace(".", "-");

            if (string.IsNullOrEmpty(elasticUri))
                throw new Exception("ElasticConfiguration Uri is not configured.");

            configuration
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Debug()
                .WriteTo.Console().ReadFrom.Configuration(context.Configuration)
                .WriteTo.Elasticsearch(
                    new ElasticsearchSinkOptions(new Uri(elasticUri))
                    {
                        IndexFormat =
                            $"{applicationName}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                        AutoRegisterTemplate = true,
                        NumberOfShards = 2,
                        NumberOfReplicas = 1,
                        ModifyConnectionSettings = x => x.BasicAuthentication(username, password)
                    })
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                .Enrich.WithProperty("Application", applicationName)
                .ReadFrom.Configuration(context.Configuration);
        });
    }
}
