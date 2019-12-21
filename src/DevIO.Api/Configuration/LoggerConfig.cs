using System;
using DevIO.Api.Extensions;
using Elmah.Io.AspNetCore;
using Elmah.Io.AspNetCore.HealthChecks;
using Elmah.Io.Extensions.Logging;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DevIO.Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddElmahIo(o =>
            {
                o.ApiKey = "8f66bd67abe14f1ca6f3fc91496eaf3d";
                o.LogId = new Guid("90c4fa92-8dc6-4557-b5c8-4826e2b99c48");
            });

            //services.AddLogging(builder =>
            //{
            //    builder.AddElmahIo(o =>
            //    {
            //        o.ApiKey = "8f66bd67abe14f1ca6f3fc91496eaf3d";
            //        o.LogId = new Guid("90c4fa92-8dc6-4557-b5c8-4826e2b99c48");
            //    });
            //    builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);
            //});

            services.AddHealthChecks()
                .AddElmahIoPublisher("8f66bd67abe14f1ca6f3fc91496eaf3d", new Guid("90c4fa92-8dc6-4557-b5c8-4826e2b99c48"), "API Fornecedores")
                .AddCheck("Produtos", new SqlServerHealthCheck(configuration.GetConnectionString("DefaultConnection")))
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");

            services.AddHealthChecksUI();

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            app.UseHealthChecks("/api/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.UseHealthChecksUI(options => { options.UIPath = "/api/hc-ui"; });

            return app;
        }
    }
}