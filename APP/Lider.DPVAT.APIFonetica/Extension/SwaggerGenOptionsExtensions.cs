using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Examples;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lider.DPVAT.APIFonetica.UI.Extension
{
    public static class SwaggerGenOptionsExtensions
    {
        /// <summary>
        /// Método que faz a configuração para do Swagger.
        /// </summary>
        /// <param name="options">Objeto que contém as configurações para o Swagger.</param>
        /// <param name="configuration"></param>
        public static void ConfigureSwaggerGenOptions(this SwaggerGenOptions options, IConfiguration configuration)
        {
            options.SwaggerDoc("v1", new Info()
            {
                Title = $"API Fonética Líder DPVAT - { configuration.GetSection("AmbienteConfiguration:Nome").Value }",
                //Version = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString(),
                Version = configuration.GetSection("Versao").Value,
                Description = "API que controlará o Fonetica.",
                Contact = new Contact()
                {
                    Name = "Seguradora Líder DPVAT",
                    Url = "https://www.seguradoralider.com.br"
                }
            });

            options.AddSecurityDefinition("Bearer", new ApiKeyScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = "header",
                Type = "apiKey"
            });
            options.DescribeAllEnumsAsStrings();
            options.OperationFilter<AddFileParamTypesOperationFilter>();
            options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                { "Bearer", Enumerable.Empty<string>() },
                });

            string caminhoAplicacao = PlatformServices.Default.Application.ApplicationBasePath;
            string nomeAplicacao = PlatformServices.Default.Application.ApplicationName;
            string caminhoXmlDoc = Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");
            options.IncludeXmlComments(caminhoXmlDoc);

            options.IgnoreObsoleteActions();
            options.IgnoreObsoleteProperties();
        }
    }
}
