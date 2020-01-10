using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Lider.DPVAT.APIFonetica.Domain.Services
{
    public class Consoantes
    {
        private char[] _Key;

        public Consoantes(string nome)
        {
            this._Key = new char[nome.Count()];
        }

        public Chave RegraConsoante(string nome)
        {
            int current = 0;
            Chave chave = new Chave(nome);

            while (current < nome.Length)
            {
                switch (nome[current])
                {
                    case 'Y':
                        //chave.AddMetaphoneCharacter("I", current);
                        chave.Origem = chave.Origem.Remove(current, 1);
                        chave.Origem = chave.Origem.Insert(current, "I");
                        current += 1;
                        break;

                    case 'B':
                        chave.AddMetaphoneCharacter("B", current);
                        current += 1;
                        break;

                    case 'Ç':
                        chave.AddMetaphoneCharacter("S", current);
                        current += 1;
                        break;

                    case 'C':
                        if (current < nome.Length - 1)
                            if (nome.Substring(current, 2).Equals("CH"))
                            {
                                if (current + 2 < nome.Length)
                                {
                                    if (Vogais.IsVowel(current + 2, nome))
                                    {
                                        chave.AddMetaphoneCharacter("X", current);
                                        current += 2;
                                        break;
                                    }
                                }
                            }
                        if (current < nome.Length - 1)
                            if (nome.Substring(current, 2).Equals("CE") || nome.Substring(current, 2).Equals("CI") || nome.Substring(current, 2).Equals("CY"))
                            {
                                chave.AddMetaphoneCharacter("S", current);
                                current += 1;
                                break;
                            }
                        chave.AddMetaphoneCharacter("C", current);
                        current += 1;
                        break;

                    case 'D':
                        chave.AddMetaphoneCharacter("D", current);
                        current += 1;
                        break;

                    case 'F':

                        chave.AddMetaphoneCharacter("F", current);
                        current += 1;
                        break;

                    case 'G':
                        if (current < nome.Length - 1)
                            if (nome.Substring(current, 2).Equals("GE") || nome.Substring(current, 2).Equals("GI") || nome.Substring(current, 2).Equals("GY"))
                            {
                                chave.AddMetaphoneCharacter("J", current);
                                current += 1;
                                break;
                            }
                        chave.AddMetaphoneCharacter("G", current);
                        current += 1;
                        break;

                    case 'H':

                        if (current == 0 && Vogais.IsVowel(current + 1, nome))
                        {
                            //chave.AddMetaphoneCharacter(nome.Substring(current + 1,1), current);
                            current += 2;
                            break;
                        }
                        // somente mantém se primeira & vogal anterior ou entre 2 vogais...
                        //else if (((current == 0) || IsVowel(current - 1,nome)) && IsVowel(current + 1,nome))
                        //{
                        //    chave.AddMetaphoneCharacter("H",current);
                        //    current += 2;
                        //}
                        if (current - 1 > 0)
                            if (nome.Substring(current - 1, 2).Equals("NH") || nome.Substring(current - 1, 2).Equals("LH"))
                            {
                                chave.AddMetaphoneCharacter("I", current);
                                current += 1;
                                break;
                            }

                        // também toma cuidado com 'HH'                            

                        current += 1;
                        break;

                    case 'J':

                        chave.AddMetaphoneCharacter("J", current);
                        current += 1;
                        break;

                    case 'K':
                        chave.AddMetaphoneCharacter("C", current);
                        current += 1;
                        break;

                    case 'L':
                        chave.AddMetaphoneCharacter("L", current);
                        current += 1;
                        break;

                    case 'M':
                        chave.AddMetaphoneCharacter("M", current);
                        current += 1;
                        break;

                    case 'N':
                        chave.AddMetaphoneCharacter("N", current);
                        current += 1;
                        break;

                    case 'Ñ': // '�'                        
                        chave.AddMetaphoneCharacter("N", current);
                        current += 1;
                        break;

                    case 'P':
                        if (current < nome.Length - 1)
                            if (nome.Substring(current + 1, 1).Equals("H"))
                            {
                                chave.AddMetaphoneCharacter("F", current);
                                current += 2;
                                break;
                            }

                        chave.AddMetaphoneCharacter("P", current);
                        current += 1;
                        break;

                    case 'Q':

                        chave.AddMetaphoneCharacter("C", current);
                        current += 1;
                        break;

                    case 'R':
                        chave.AddMetaphoneCharacter("R", current);
                        current += 1;
                        break;

                    case 'S':
                        if (current < nome.Length - 1)
                            if (nome.Substring(current, 2).Equals("SH"))
                            {
                                chave.AddMetaphoneCharacter("X", current);
                                current += 2;
                                break;
                            }

                        chave.AddMetaphoneCharacter("S", current);
                        current += 1;
                        break;

                    case 'T':
                        chave.AddMetaphoneCharacter("T", current);
                        current += 1;
                        break;

                    case 'V':
                        chave.Origem = chave.Origem.Remove(current, 1);
                        chave.Origem = chave.Origem.Insert(current, "U");
                        current += 1;
                        break;

                    case 'W':
                        chave.Origem = chave.Origem.Remove(current, 1);
                        chave.Origem = chave.Origem.Insert(current, "U");
                        current += 1;
                        break;

                    case 'X':
                        chave.AddMetaphoneCharacter("X", current);
                        current += 1;
                        break;

                    case 'Z':
                        chave.AddMetaphoneCharacter("S", current);
                        current += 1;
                        break;

                    default:
                        current += 1;
                        break;
                }
            }
            return chave;
        }

    }
}
