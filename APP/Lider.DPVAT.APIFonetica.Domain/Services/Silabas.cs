using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lider.DPVAT.APIFonetica.Domain.Services
{
    public class Silabas : Constantes
    {

        public static Palavra separa(string palavra)
        {
            palavra = palavra.ToLower();
            if (palavra.Equals("ao") || palavra.Equals("aos"))
            {
                Palavra separada = new Palavra();
                separada.Silibas.Add(new Palavra.silaba() { letra = palavra, quantidade = palavra.Length });
                return separada;


            }
            else
            {
                List<int> posicoes = constroiPosicoes1(palavra);
                posicoes = constroiPosicoes2(palavra, posicoes);
                return preencheSilabas(posicoes, palavra);

            }
        }

        public static String getSilabasString(String palavra)
        {
            return getSilabasString(separa(palavra));
        }

        public static String getSilabasString(Palavra silabas)
        {
            String ret = "";
            //for(int i = 0; i < silabas.Count; i++)
            //{
            //    ret = (new StringBuilder( ret)).Append((String)silabas[i]).ToString();
            //    if(i < silabas.Count - 1)
            //        ret = (new StringBuilder(ret)).Append("-").ToString();
            //}
            for (int i = 0; i < silabas.Silibas.Count; i++)
            {
                ret = (new StringBuilder(ret)).Append((String)silabas.Silibas[i].letra).ToString();
                if (i < silabas.Silibas.Count - 1)
                    ret = (new StringBuilder(ret)).Append("-").ToString();
            }

            return ret;
        }

        //private static void iniciaArraysCompostos()
        //{
        //    System.arraycopy(ACENTOS_GA, 0, ACENTOS, 0, ACENTOS_GA.length);
        //    System.arraycopy(CIRCUNFLEXO, 0, ACENTOS, ACENTOS_GA.length, CIRCUNFLEXO.length);
        //    System.arraycopy(TIL, 0, ACENTOS, ACENTOS_GA.length + CIRCUNFLEXO.length, TIL.length);
        //    System.arraycopy(VOGAIS, 0, VOGAIS_ACENTOS, 0, VOGAIS.length);
        //    System.arraycopy(ACENTOS, 0, VOGAIS_ACENTOS, VOGAIS.length, ACENTOS.length);
        //    System.arraycopy(SEMI_VOGAIS, 0, SEMI_VOGAIS_ACENTOS, 0, SEMI_VOGAIS.length);
        //    System.arraycopy(ACENTOS, 0, SEMI_VOGAIS_ACENTOS, SEMI_VOGAIS.length, ACENTOS.length);
        //}

        private static List<int> constroiPosicoes1(string palavra)
        {
            List<int> posicoes = new List<int>();
            for (int i = 1; i < palavra.Length; i++)
                if (pertence(palavra.ElementAt(i), Constantes.VOGAIS_ACENTOS) && !pertence(palavra.ElementAt(i - 1), Constantes.VOGAIS_ACENTOS) && i > 1)
                    if (pertence(palavra.ElementAt(i - 1), Constantes.H) && pertence(palavra.ElementAt(i - 2), Constantes.CONJ_2) || pertence(palavra.ElementAt(i - 1), Constantes.LATERAIS) && pertence(palavra.ElementAt(i - 2), Constantes.CONJ_1))
                        posicoes.Add(i - 2);
                    else
                        posicoes.Add(i - 1);

            if (posicoes.Count > 0 && (posicoes[0]) == 1 && !pertence(palavra.ElementAt(0), Constantes.VOGAIS_ACENTOS))
                //posicoes.set(0, Integer.valueOf(0));
                posicoes.Insert(0, 0);
            if (posicoes.Count == 0 || (posicoes[0]) != 0)
                posicoes.Insert(0, 0);
            return posicoes;
        }

        private static List<int> constroiPosicoes2(String palavra, List<int> posicoes)
        {
            for (int i = 1; i < palavra.Length; i++)
            {
                if (pertence(palavra[i], Constantes.VOGAIS_ACENTOS) && pertence(Convert.ToChar(palavra.Substring(i - 1, 1)), Constantes.VOGAIS_ACENTOS) && (i <= 1 || Convert.ToChar(palavra.Substring(i - 1, 1)) != 'u' || !pertence(Convert.ToChar(palavra.Substring(i - 2, 1)), Constantes.CONJ_8)) && !pertence(Convert.ToChar(palavra.Substring(i - 1, 1)), Constantes.TIL) && (!pertence(Convert.ToChar(palavra.Substring(i, 1)), Constantes.SEMI_VOGAIS) || ultimaSilaba(palavra, i, posicoes) && pertence2(Constantes.LATERAIS, palavra, i + 1) || pertence2(Constantes.NASAIS, palavra, i + 1) && !pertence2(Constantes.VOGAIS_ACENTOS, palavra, i + 2)))
                {
                    for (int j = 0; j < posicoes.Count; j++)
                    {
                        if (posicoes[j] > i)
                        {
                            posicoes.Insert(j, i);
                            break;
                        }
                        if (j != posicoes.Count - 1)
                            continue;
                        posicoes.Add(i);
                        break;
                    }

                }
                if (i > 2)
                {
                    if (Convert.ToChar(palavra.Substring(i - 1, 1)) == 'i' && Convert.ToChar(palavra.Substring(i, 1)) == 'u' && i != palavra.Length - 1)
                    {
                        for (int j = 0; j < posicoes.Count; j++)
                        {
                            if (posicoes[j] > i)
                            {
                                posicoes.Insert(j, i);
                                break;
                            }
                            if (j != posicoes.Count - 1)
                                continue;
                            posicoes.Add(i);
                            break;
                        }
                    }
                }
            }
            return posicoes;
        }

        public static bool pertence(char l, char[] conjunto)
        {
            for (int i = 0; i < conjunto.Length; i++)
                if (l == conjunto[i])
                    return true;

            return false;
        }

        private static bool pertence2(char[] conjunto, String palavra, int index)
        {
            if (index >= palavra.Length || index < 0)
                return false;
            else
                return pertence(Convert.ToChar(palavra.Substring(index, 1)), conjunto);
        }

        private static bool ultimaSilaba(String palavra, int index, List<int> posicoes)
        {
            //return index >= (palavra[posicoes.Count - 1]);j
            return index >= (palavra[posicoes.Count - 1]);
        }

        private static Palavra preencheSilabas(List<int> posicoes, String palavra)
        {
            Palavra ret = new Palavra();
            ret.Silibas = new List<Palavra.silaba>();
            if (posicoes.Count > 0)
            {
                int i;
                for (i = 0; i < posicoes.Count - 1; i++)
                {

                    ret.Silibas.Add(new Palavra.silaba() { letra = palavra.Substring(posicoes[i], posicoes[i + 1] - posicoes[i]).ToUpper(), quantidade = (posicoes[i + 1] - posicoes[i]) });
                    //ret.Add(palavra.Substring(posicoes[i], posicoes[i + 1] - posicoes[i]));

                }

                //ret.Add(palavra.Substring(posicoes[i]));
                ret.Silibas.Add(new Palavra.silaba() { letra = palavra.Substring(posicoes[i]).ToUpper(), quantidade = (palavra.Length - posicoes[i]) });
            }
            return ret;
        }

        public static int getTonicaInt(String palavra)
        {
            Palavra silabas = separa(palavra);
            //return getTonicaInt(palavra, silabas);
            return 0;
        }

        public static int getTonicaInt(String palavra, List<string> silabas)
        {
            if (estaEm(Constantes.MONOSSILABOS_ATONOS, palavra) || palavra.Length == 0)
                return -1;
            for (int i = silabas.Count - 1; i >= 0; i--)
            {
                String silaba = silabas[i];
                for (int j = 0; j < silaba.Length; j++)
                {
                    if ((pertence(Convert.ToChar(silaba.Substring(j, 1)), Constantes.ACENTOS_GA) || pertence(Convert.ToChar(silaba.Substring(j, 1)), Constantes.CIRCUNFLEXO)) && i > silabas.Count - 4)
                        return i;
                    if (pertence(Convert.ToChar(silaba.Substring(j, 1)), Constantes.TIL) && i > silabas.Count - 3)
                        return i;
                }

            }

            char ultimo = Convert.ToChar(palavra.Substring(palavra.Length - 1));
            if (silabas.Count == 1 || pertence(ultimo, Constantes.LATERAIS))
                return silabas.Count - 1;
            String ultima = (String)silabas[silabas.Count - 1];
            for (int i = 0; i < ultima.Length; i++)
                if (pertence(Convert.ToChar(ultima.Substring(i)), Constantes.SEMI_VOGAIS) && (Convert.ToChar(ultima.Substring(i, 1)) != 'u' || !pertence2(Constantes.CONJ_8, ultima, i - 1)))
                    return silabas.Count - 1;

            return silabas.Count - 2;
        }

        public static String getTonicaString(String palavra)
        {
            Palavra silabas = separa(palavra);
            //return getTonicaString(palavra, silabas);
            return null;
        }

        public static String getTonicaString(String palavra, List<string> silabas)
        {
            int i = getTonicaInt(palavra, silabas);
            return i < 0 ? "-" : (String)silabas[i];
        }

        public static bool estaEm(string[] lista, string palavra)
        {
            for (int i = 0; i < lista.Length; i++)
                if (palavra.Equals(lista[i]))
                    return true;

            return false;
        }
    }
}


