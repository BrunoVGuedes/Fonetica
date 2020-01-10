using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Lider.DPVAT.APIFonetica.UI.Extension
{
    public static class KestrelServerOptionsExtensions
    {
        public static void ConfigureEndpoints(this KestrelServerOptions options)
        {
            var configuration = options.ApplicationServices.GetRequiredService<IConfiguration>();
            var environment = options.ApplicationServices.GetRequiredService<IHostingEnvironment>();

            var hostHttp = configuration.GetSection("HttpServer:Endpoints:Http:Host")?.Value;
            var portHostHttp = configuration.GetSection("HttpServer:Endpoints:Http:Port")?.Value;
            var hostHttps = configuration.GetSection("HttpServer:Endpoints:Https:Host")?.Value;
            var portHostHttps = configuration.GetSection("HttpServer:Endpoints:Https:Port")?.Value;
            var storeName = configuration.GetSection("HttpServer:Endpoints:Https:StoreName")?.Value;
            var storeLocation = configuration.GetSection("HttpServer:Endpoints:Https:StoreLocation")?.Value;
            var certificateName = configuration.GetSection("HttpServer:Endpoints:Https:Name")?.Value;


            if (!string.IsNullOrEmpty(portHostHttp))
                options.Listen(IPAddress.Any, Convert.ToInt32(portHostHttp));

            if (!string.IsNullOrEmpty(portHostHttps))
            {
                var endpoint = new EndpointConfiguration()
                {
                    StoreLocation = storeLocation,
                    StoreName = storeName,
                    Name = certificateName
                };
                var certificate = LoadCertificate(endpoint, environment);
                options.Listen(IPAddress.Any, Convert.ToInt32(portHostHttps), c => c.UseHttps(certificate));
            }
        }

        private static X509Certificate2 LoadCertificate(EndpointConfiguration config, IHostingEnvironment environment)
        {
            if (config.StoreName != null && config.StoreLocation != null)
            {
                using (var store = new X509Store(config.StoreName, config.StoreLocation == "CurrentUser" ? StoreLocation.CurrentUser : StoreLocation.LocalMachine))
                {
                    store.Open(OpenFlags.ReadOnly);
                    var certificate = store.Certificates.Find(
                        X509FindType.FindBySubjectName,
                        config.Name,
                        validOnly: !environment.IsDevelopment());

                    if (certificate.Count == 0)
                    {
                        throw new InvalidOperationException($"Certificate not found for {config.Name}.");
                    }

                    return certificate[0];
                }
            }

            if (config.FilePath != null && config.Password != null)
            {
                return new X509Certificate2(config.FilePath, config.Password);
            }

            throw new InvalidOperationException("No valid certificate configuration found for the current endpoint.");
        }
    }
}
