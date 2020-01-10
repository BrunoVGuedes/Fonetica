using App.Metrics;
using App.Metrics.Counter;
using App.Metrics.Timer;
using Lider.DPVAT.APIFonetica.Application.Interfaces;
using Lider.DPVAT.APIFonetica.UI.VewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.InteropServices.ComTypes;

namespace Lider.DPVAT.APIFonetica.UI.Controllers
{
    /// <summary>
    /// Api de Fonetica.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    //[Authorize(Policy = "APIFonetica")]
    public class FoneticaController : ControllerBase
    {
        //LogFoneticaAppService
        private readonly IFoneticaAppService _FoneticaAppService;
        private IMetrics _metrics;
        public FoneticaController(IFoneticaAppService foneticaAppService, IMetrics metrics)
        {
            _FoneticaAppService = foneticaAppService;
            _metrics = metrics;
        }

        // Post: api/Fonetica
        /// <summary>
        /// Metodo para realizar o processo de Fonetica.
        /// </summary>
        /// <param name="request"> Parametros de entrada para a desvinculação do sisnitro</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ParametroSaidaFonetica), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(ParametroSaidaFonetica), 500)]
        [HttpPost]
        public IActionResult Fonetica([FromBody] ParametrosEntradaFonetica request)
        {
            var metricas = PegarMetricasdasAction("Fonetica");
            ParametroSaidaFonetica parametroSaidaFonetica = new ParametroSaidaFonetica();

            using (_metrics.Measure.Timer.Time(metricas))
            {
                if (string.IsNullOrEmpty(request.palavra))
                {
                    parametroSaidaFonetica.CodigoRetorno = 5;

                    GC.Collect();
                    return BadRequest(parametroSaidaFonetica); //"Parametros obrigatorios Vazios, Por favor verificar"
                }

                var returno = _FoneticaAppService.GerarFonetica(request.palavra);
                parametroSaidaFonetica.CodigoRetorno = 200;
                parametroSaidaFonetica.Palavra = returno;
            }
            GC.Collect();
            return Ok(parametroSaidaFonetica);

        }

        private TimerOptions PegarMetricasdasAction(string nomeAction)
        {
            TimerOptions requestTimer = new TimerOptions()
            {
                Name = "Metricas da Action" + nomeAction,
                MeasurementUnit = Unit.Requests,
                DurationUnit = TimeUnit.Milliseconds,
                RateUnit = TimeUnit.Milliseconds
            };

            var quantidadeChamada = new CounterOptions
            {
                Name = nomeAction,
                MeasurementUnit = Unit.Calls
            };

            _metrics.Measure.Counter.Increment(quantidadeChamada);
            _metrics.Measure.Counter.Decrement(quantidadeChamada);
            _metrics.Measure.Counter.Increment(quantidadeChamada, 10);
            _metrics.Measure.Counter.Decrement(quantidadeChamada, 10);

            return requestTimer;
        }
    }
}
