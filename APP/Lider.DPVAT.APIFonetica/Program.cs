using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.AspNetCore.Health;
using App.Metrics.Formatters.Prometheus;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.LayoutRenderers;
using NLog.Web;


namespace Lider.DPVAT.APIFonetica
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LayoutRenderer.Register("user-domain", (logEvent) => Environment.UserDomainName);
            LayoutRenderer.Register("user-name", (logEvent) => Environment.UserName);
            LayoutRenderer.Register("os-version", (logEvent) => Environment.OSVersion);
            LayoutRenderer.Register("current-directory", (logEvent) => Environment.CurrentDirectory);
            LayoutRenderer.Register("custom-shortdate", (logEvent) => DateTime.Now.ToString("yyyyMMdd"));
            LayoutRenderer.Register("application-name", (logEvent) => Assembly.GetExecutingAssembly().FullName);

            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {


                //CreateWebHostBuilder(args).Build().Run();

                var host = BuildWebHost(args);
                host.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Programa parado por causa de uma exception.");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IMetricsRoot Metrics { get; set; }
        public static IWebHost BuildWebHost(string[] args)
        {
            //Metrics = AppMetrics.CreateDefaultBuilder()
            //        .OutputMetrics.AsPrometheusPlainText()
            //        .Build();


            return WebHost.CreateDefaultBuilder(args)
                   .UseStartup<Startup>()
                   //.UseMetrics(
                   //         options =>
                   //         {
                   //             options.EndpointOptions = endpointsOptions =>
                   //             {
                   //                 endpointsOptions.MetricsTextEndpointOutputFormatter = Metrics.OutputMetricsFormatters.OfType<MetricsPrometheusTextOutputFormatter>().First();
                   //                 //  endpointsOptions.MetricsEndpointOutputFormatter = Metrics.OutputMetricsFormatters.OfType<MetricsPrometheusProtobufOutputFormatter>().First();
                   //             };
                   //         }
                   //         )
                   //.UseMetricsWebTracking()
                   //.UseMetricsEndpoints()
                   //.UseHealth()
                   //.UseHealthEndpoints()
                   .UseConfiguration(Configuration())
                   .UseNLog()
                   .Build();

        }
        public static IConfigurationRoot Configuration()
        {
            var config = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("hosting.json", optional: true)
                        .Build();

            return config;
        }

    }
}
