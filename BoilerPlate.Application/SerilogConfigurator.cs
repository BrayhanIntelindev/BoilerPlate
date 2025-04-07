using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using Serilog.Events;
using Serilog.Sinks.SQLite;
using Serilog.Sinks.Http;
using BoilerPlate.Application.Sections.Logger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BoilerPlate.Application
{
    public static class SerilogConfigurator
    {
        public static void ConfigureSerilog(this IHostBuilder host, IConfiguration configuration)
        {
            var csvFormatter = new CsvFormatter();
            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.ApplicationInsights(
                    telemetryConverter: new Serilog.Sinks.ApplicationInsights.TelemetryConverters.TraceTelemetryConverter(),
                    restrictedToMinimumLevel: LogEventLevel.Information
                )
                .WriteTo.File("logs/myapp-.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.File(
                    formatter: csvFormatter,
                    path: "logs/cdv/myapp-.csv",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: null
                )
                //.WriteTo.MSSqlServer( // Configura el sink para SQL Server.
                //    connectionString: configuration.GetConnectionString("ConnectionString"), 
                //    sinkOptions: new MSSqlServerSinkOptions { TableName = "SeriLogs", AutoCreateSqlTable = true },
                //    restrictedToMinimumLevel: LogEventLevel.Warning,
                //    columnOptions: new ColumnOptions
                //    {
                //        AdditionalColumns = new List<SqlColumn> 
                //        {
                //            new SqlColumn { ColumnName = "ApplicationName", DataType = SqlDbType.NVarChar, DataLength = 50, AllowNull = true }
                //        }
                //    }
                //)
                .WriteTo.SQLite(
                    sqliteDbPath: "seriLog.db",
                    tableName: "SeriLogs",
                    storeTimestampInUtc: true,
                    restrictedToMinimumLevel: LogEventLevel.Warning
                );

            var workspaceId = configuration["SerilogSettings:AzureLogAnalytics:WorkspaceId"];
            if (!string.IsNullOrEmpty(workspaceId))
                loggerConfiguration.WriteTo.AzureAnalytics(
                    workspaceId: workspaceId,
                    authenticationId: configuration["SerilogSettings:AzureLogAnalytics:AuthenticationId"],
                    logName: "MyAppLogs"
                );

            var webHookUrl = configuration["SerilogSettings:WebHookUrl"];
            if (!string.IsNullOrEmpty(webHookUrl))
                loggerConfiguration.WriteTo.Http(requestUri: webHookUrl,
                queueLimitBytes: 1000000,
                    period: TimeSpan.FromSeconds(5)
                );

            Log.Logger = loggerConfiguration.CreateLogger();

            host.UseSerilog();

            EventsBeforeLog(csvFormatter);
        }
        public static IServiceCollection ConfigureSerilog(this IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders(); // Limpia los proveedores de registro predeterminados.
                loggingBuilder.AddSerilog(CreateLogger());
            });

            return services;
        }
        private static Serilog.Core.Logger CreateLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.AzureBlobStorage(
                    connectionString: Environment.GetEnvironmentVariable("AzureWebJobsStorage"),
                    storageContainerName: "logs",
                    restrictedToMinimumLevel: LogEventLevel.Information,
                    storageFileName: "myapp-{Date}.log"
                )
                .CreateLogger();
        }
        private static void EventsBeforeLog(CsvFormatter csvFormatter)
        {
            csvFormatter.ResetNeedsHeaders();
        }
    }
}
