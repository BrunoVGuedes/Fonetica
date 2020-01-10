using Lider.DPVAT.APIFonetica.UI.ProviderJWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lider.DPVAT.APIFonetica.UI.Extension
{
    public static class JwtBearerExtensions
    {
        /// <summary>
        /// Método que faz configuração a autenticação baseada no JWT.
        /// </summary>
        /// <param name="option">Objeto com as configurações para o JWT.</param>
        /// <param name="Configuration">Objeto com as configurações para arquivos extrerno.</param>
        public static void Configure(this JwtBearerOptions option, IConfiguration Configuration)
        {
            option.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Configuration.GetSection("Issuer").Value,
                ValidAudience = Configuration.GetSection("Audience").Value,
                IssuerSigningKey = JwtSecurityKey.Create(Configuration.GetSection("SecurityKey").Value)
            };

            option.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                    return Task.CompletedTask;
                }
            };
        }
    }
}
