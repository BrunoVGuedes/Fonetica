using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Lider.DPVAT.APIFonetica.Domain.Services
{
    public class Vogais
    {
        public static bool IsVowel(int index, string palavra)
        {
            char it = palavra[index];

            if (it.Equals('A') || it.Equals('E') || it.Equals('I') || it.Equals('O') || it.Equals('U'))
                return true;

            return false;
        }

        public Chave RegraVogais(Chave chave)
        {
            Encontro encontro = new Encontro();
            int qtdEncontros = encontro.ProximoEncontroVocalico(chave.Origem).Count();
            int current = chave.Origem.Length;
            //if(chave.Origem[0].Equals("H")) current--;

            switch (current)
            {
                case 1:
                    chave = PrimeiraVogal(chave);
                    break;

                case 2:
                    chave = PrimeiraVogal(chave);
                    break;

                case 3:
                    chave = PrimeiraVogal(chave);
                    break;

                case 4:
                    if (qtdEncontros == 0) chave = PrimeiraVogal(chave);
                    break;

                case 5:
                    if (qtdEncontros == 0)
                    {
                        if (!encontro.EncontroVocalicoConsonantal(chave.Origem)) chave = PrimeiraVogal(chave);
                        else chave = UltimaVogal(chave);
                    }
                    break;

                case 6:
                    if (qtdEncontros == 0)
                    {
                        if (!Vogais.IsVowel(chave.Origem.Length - 1, chave.Origem)) chave = PrimeiraVogal(chave);
                        else chave = UltimaVogal(chave);
                    }
                    break;

                case 7:
                    chave = UltimaVogal(chave);
                    break;

                default:
                    chave = UltimaVogal(chave);
                    break;
            }


            return chave;
        }

        public Chave PrimeiraVogal(Chave chave)
        {
            int current = 0;

            while (current < chave.Origem.Length)
            {
                if (Vogais.IsVowel(current, chave.Origem))
                {
                    chave.AddMetaphoneCharacter(chave.Origem[current].ToString(), current);
                    return chave;
                }
                current++;
            }
            return chave;
        }

        public Chave UltimaVogal(Chave chave)
        {
            int current = chave.Origem.Length - 1;

            while (current > 0)
            {
                if (Vogais.IsVowel(current, chave.Origem))
                {
                    chave.AddMetaphoneCharacter(chave.Origem[current].ToString(), current);
                    return chave;
                }
                current--;
            }
            return chave;
        }
    }
}


