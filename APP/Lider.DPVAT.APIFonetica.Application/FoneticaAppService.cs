using Lider.DPVAT.APIFonetica.Domain.Interfaces.Services;
using Lider.DPVAT.APIFonetica.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lider.DPVAT.APIFonetica.Application
{
    public class FoneticaAppService : IFoneticaAppService
    {
        private readonly IFoneticaService _FoneticaService;

        public FoneticaAppService(IFoneticaService foneticaService)
        {
            _FoneticaService = foneticaService;
        }

        public string[] GerarFonetica(string nome)
        {
            return _FoneticaService.Metaphone(nome);
        }
    }
}

