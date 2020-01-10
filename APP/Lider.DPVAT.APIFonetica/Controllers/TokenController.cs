using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lider.DPVAT.APIFonetica.UI.ProviderJWT;
using Lider.DPVAT.APIFonetica.UI.VewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Lider.DPVAT.APIFonetica.UI.Controllers
{
    /// <summary>
    /// Token controle de Acesso
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        // GET: api/Token
        private readonly IConfiguration _configuration;

        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Metodo para a solicitação do Token que é a chave para a autencitação da API.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Retorna o Token para autenticar</returns>
        [AllowAnonymous]
        [HttpPost]
        [Produces("application/json")]
        public IActionResult RequestToken([FromBody] UsuarioViewModel request)
        {

            if (request == null)
            {
                return NotFound("Paramentros Inválidos");
            }

            if (request.Nome != _configuration.GetSection("Usuario").Value || request.Senha != _configuration.GetSection("Senha").Value)
            {
                return Unauthorized();
            }

            var Expiry = Convert.ToInt32(_configuration.GetSection("Expiry").Value);

            var token = new TokenJWTBuilder()
             .AddSecurityKey(JwtSecurityKey.Create(_configuration.GetSection("SecurityKey").Value))
             .AddSubject(_configuration.GetSection("Subject").Value)
             .AddIssuer(_configuration.GetSection("Issuer").Value)
             .AddAudience(_configuration.GetSection("Audience").Value)
             .AddClaim(_configuration.GetSection("Claim").Value, _configuration.GetSection("ClaimNumber").Value)
             .AddExpiry(Expiry)
             .Builder();

            return Ok(token.value);
        }

    }
}
