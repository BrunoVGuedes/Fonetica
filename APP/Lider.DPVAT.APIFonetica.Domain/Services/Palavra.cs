using System;
using System.Collections.Generic;
using System.Text;

namespace Lider.DPVAT.APIFonetica.Domain.Services
{
    public class Palavra
    {
        public struct silaba { public string letra; public int quantidade; }
        private List<silaba> _Silibas;
        public List<silaba> Silibas
        {
            get { return _Silibas; }
            set { _Silibas = value; }

        }


    }
}
