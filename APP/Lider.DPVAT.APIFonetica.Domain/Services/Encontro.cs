using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lider.DPVAT.APIFonetica.Domain.Services
{
    public class Encontro
    {
        public string EncontroVocalico(Chave phone, int index)
        {
            StringBuilder sub = new StringBuilder();

            for (int i = 0; (i + index) < phone.Origem.Length; i++)
            {
                if (Constantes.VOGAIS.Contains(Convert.ToChar(phone.Origem[i + index])))
                    sub.Append(phone.Origem[i + index]);
                else if (sub.Length > 0) return sub.ToString();
            }
            return sub.ToString();
        }

        public List<int> ProximoEncontroVocalico(string nome)
        {
            List<int> count = new List<int>();

            for (int i = 0; i < nome.Length - 1; i++)
            {

                if (Constantes.VOGAIS.Contains(Convert.ToChar(nome[i])) && Constantes.VOGAIS.Contains(Convert.ToChar(nome[i + 1])))
                {
                    count.Add(i);

                    for (; i < nome.Length; i++)
                    {
                        if (!Constantes.VOGAIS.Contains(Convert.ToChar(nome[i]))) break;
                    }
                }

            }

            return count;
        }

        public Chave InsertEncontroVocalico(Chave chave)
        {
            foreach (var item in ProximoEncontroVocalico(chave.Origem))
            {
                chave.AddMetaphoneCharacter(EncontroVocalico(chave, item), item);
            }

            return chave;
        }

        public bool EncontroVocalicoConsonantal(string palavra)
        {
            if (!Vogais.IsVowel(0, palavra) && !Vogais.IsVowel(1, palavra))
                return true;
            else
                return false;


        }
    }
}


