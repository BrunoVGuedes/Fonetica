using System;
using System.Collections.Generic;
using System.Text;

namespace Lider.DPVAT.APIFonetica.Domain.Interfaces.Services
{
    public interface IFoneticaService
    {
        string RemoverPreposicoes(string texto);

        string RemoverAcentos(string texto);

        string[] Metaphone(string nome);
    }
}
