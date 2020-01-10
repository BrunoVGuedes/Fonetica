using System;
using System.Linq;
using System.Threading.Tasks;
using App.Metrics;
using App.Metrics.Health;
using App.Metrics.Health.Checks.Sql;
using Lider.DPVAT.APIDesvincularSinistro.UI.Extension;
using Lider.DPVAT.APIFonetica.Infra.CrossCutting.ExceptionHandler;
using Lider.DPVAT.APIFonetica.Infra.CrossCutting.IOC;
using Lider.DPVAT.APIFonetica.UI.Extension;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using NLog.Extensions.Logging;
using NLog.Web;
using Prometheus;

namespace Lider.DPVAT.APIFonetica
{
    public class Startup
    {
        public Startup(IHostingEnvironment environment)
        {
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(environment.ContentRootPath)
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .AddJsonFile("versao.json", optional: false, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                 .Build();

            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IOC iOC = new IOC();
            iOC.InjecaoAppService(services);
            iOC.InjecaoDomain(services);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.ConfigureSwaggerGenOptions(Configuration);
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(option =>
                  option.Configure(Configuration)
        );

            services.AddAuthorization(options =>
            {
                options.AddPolicy("APIFonetica", policy => policy.RequireClaim("APIFoneticaNumero"));
            });

            services.AddMvcCore().AddMetricsCore();
            //  .AddApiExplorer()
            //   .AddFormatterMappings()
            //   .AddDataAnnotations()
            //    .AddJsonFormatters()
            //    .AddCors()
            ;

            services.AddResponseCompression();
            services.AddGlobalExceptionHandlerMiddleware();
            // services.AddDistributedMemoryCache();
            // services.AddRouting();

            services.AddHealthChecks()
                  .AddGCInfoCheck("MEMORIA");

            // var metrics = new MetricsBuilder()
            //                      .Configuration.Configure(
            //                          options =>
            //                          {
            //                              options.AddServerTag();
            //                              options.AddEnvTag();
            //                              options.AddAppTag();
            //                          })
            //                      .OutputMetrics.AsPrometheusPlainText()
            //                      .OutputMetrics.AsPrometheusProtobuf()
            //                      .Build();

            // services.AddMetrics(metrics);
            // services.AddMetricsEndpoints();
            // services.AddMetricsTrackingMiddleware();
            // services.AddMetricsReportingHostedService();

            // var health = AppMetricsHealth.CreateDefaultBuilder()
            //.HealthChecks.RegisterFromAssembly(services)
            //.BuildAndAddTo(services);

            // services.AddHealth(health);
            // services.AddHealthEndpoints();
            // services.AddHealthReportingHostedService();


            var health = AppMetricsHealth.CreateDefaultBuilder();//.Configuration.Configure(new HealthOptions() { Enabled = true, ApplicationName = "http://health.local.com", ReportingEnabled = true })
            //.Report.ToMetrics(metrics)
            //.HealthChecks.AddSqlCachedCheck("Teste de conexão com o banco.", () => new MySql.Data.MySqlClient.MySqlConnection("Server=mysql;Database=healthcheck;Uid=healthcheckuser;Pwd=healthcheckpws;SslMode=none;"),
            //                              TimeSpan.FromSeconds(10),
            //                              TimeSpan.FromMinutes(1))
            //.HealthChecks.AddPingCheck("Google Ping", "google.com", TimeSpan.FromSeconds(10))
            //.HealthChecks.AddHttpGetCheck("GitHub", new Uri("https://github.com"), TimeSpan.FromSeconds(10))
            //.HealthChecks.RegisterFromAssembly(services)
            //.BuildAndAddTo(services);

            services.AddHealth(health);

            var metrics = new MetricsBuilder()
                                .Configuration.Configure(
                                    options =>
                                    {
                                        options.AddServerTag();
                                        options.AddEnvTag();
                                        options.AddAppTag();
                                    })
                                .OutputMetrics.AsPrometheusPlainText()
                                .Build();

            services.AddMetrics(metrics);

            services.AddMetricsReportingHostedService();
            services.AddHealthReportingHostedService();

            services.AddMetricsEndpoints();
            services.AddHealthEndpoints();
            services.AddMetricsTrackingMiddleware();
            services.AddMvc().AddMetrics();


            services.AddMvc().AddMetrics();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();
            env.ConfigureNLog("NLog.config");

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }


            app.UseGlobalExceptionHandlerMiddleware();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIFonetica");
            });
            var rew = new RewriteOptions();
            rew.AddRedirect("^$", "swagger");
            app.UseRewriter(rew);
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseResponseCompression();
            app.UseHealthChecks("/check", new HealthCheckOptions()
            {
                ResponseWriter = WriteResponse,
            });

            app.UseMetricsAllMiddleware();
            //app.UseHealthAllEndpoints();
            app.UseMetricsAllEndpoints();
            app.UseMvc();
        }

        private static Task WriteResponse(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";
            var json = new JObject(
            new JProperty("status", result.Status.ToString()),
            new JProperty("results", new JObject(result.Entries.Select(pair =>
            new JProperty(pair.Key, new JObject(
            new JProperty("status", pair.Value.Status.ToString()),
            new JProperty("description", pair.Value.Description),
            new JProperty("data", new JObject(pair.Value.Data.Select(p => new JProperty(p.Key, p.Value))))))))));
            return httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
        }

    }
}
