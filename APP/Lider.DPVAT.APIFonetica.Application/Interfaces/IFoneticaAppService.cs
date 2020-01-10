using System;
using System.Collections.Generic;
using System.Text;

namespace Lider.DPVAT.APIFonetica.Application.Interfaces
{
    public interface IFoneticaAppService
    {
        string[] GerarFonetica(string nome);
    }
}
