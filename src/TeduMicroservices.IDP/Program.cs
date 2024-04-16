﻿using Serilog;
using TeduMicroservices.IDP.Extensions;
using TeduMicroservices.IDP.Infrastructure.Persistence;
using TeduMicroservices.IDP.Persistence;

Log.Information("Starting up");
var builder = WebApplication.CreateBuilder(args);
try
{
    builder.AddAppConfigurations();
    builder.Host.ConfigureSerilog();

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    await app.MigrateDatabaseAsync(builder.Configuration);
    await SeedUserData.EnsureSeedDataAsync(builder.Configuration.GetConnectionString("IdentitySqlConnection") ?? "");

    app.Run();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal)) throw;

    Log.Fatal(ex, $"Unhandled exception: {ex.Message}");
}
finally
{
    Log.Information($"Shutdown {builder.Environment.ApplicationName} complete");
    Log.CloseAndFlush();
}