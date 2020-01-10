using Lider.DPVAT.APIFonetica.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lider.DPVAT.APIFonetica.Domain.Services
{
    public class FoneticaService : IFoneticaService
    {
        public string RemoverPreposicoes(string texto)
        {
            texto = RemoverAcentos(texto.ToUpperInvariant());

            //Eliminar preposições
            string[] preposicoes = new[] { " DE ", " DA ", " DO ", " AS ", " OS ", " AO ", " NA ", " NO ", " DOS ", " DAS ", " AOS ", " NAS ", " NOS ", " COM " };

            texto = preposicoes.Aggregate(texto, (current, preposicao) => current.Replace(preposicao, " "));

            //Elimina preposições e artigos
            string[] letras = new[] { " A ", " B ", " C ", " D ", " E ", " F ", " G ", " H ", " I ", " J ", " K ", " L ", " M ", " N ", " O ", " P ", " Q ", " R ", " S ", " T ", " U ", " V ", " X ", " Z ", " W ", " Y " };

            texto = letras.Aggregate(texto, (current, letra) => current.Replace(letra, " "));
            texto = texto.Trim();

            string[] particulas = texto.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] tratados = new string[particulas.Length];

            for (var i = 0; i < particulas.Length; i++)
            {
                tratados[i] = particulas[i];
            }

            string tratado = string.Join(" ", tratados).Trim();

            return tratado;
        }

        public string RemoverAcentos(string texto)
        {
            const string comAcento = "áÁàÀâÂãÃéÉèÈêÊíÍìÌîÎóÓòÒôÔõÕúÚùÙûÛüÜçÇñÑ'";
            const string semAcento = "AAAAAAAAEEEEEEIIIIIIOOOOOOOOUUUUUUUUCCNN ";

            for (int i = 0; i < comAcento.Length; i++)
            {
                texto = texto.Replace(comAcento[i], semAcento[i]);
            }

            return texto;
        }

        public string[] Metaphone(string nome)
        {
            try
            {
                string str = RemoverPreposicoes(nome);

                string[] particulas = str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string[] tratados = new string[2];

                string primary = string.Empty;

                for (var i = 0; i < particulas.Length; i++)
                {
                    if (particulas[i].Length > 1)
                    {
                        Metaphone mphone = new Metaphone(particulas[i]);
                        if (mphone.Chave != null)
                            if (!string.IsNullOrEmpty(mphone.Chave.ToString()))
                            {
                                for (int j = 0; j < mphone.Chave.Count(); j++)
                                {
                                    if (!mphone.Chave[j].Equals('\0')) tratados[0] += mphone.Chave[j];
                                }
                                tratados[0] += " ";

                            }
                        if (!string.IsNullOrEmpty(mphone.Nome))
                        {
                            tratados[1] += mphone.Nome + " ";
                        }
                    }
                    else
                    {
                        tratados[0] += particulas[i] + " ";
                        tratados[1] += particulas[i] + " ";
                    }
                }

                return tratados;
            }
            catch (Exception)
            {

                return null;
            }

        }
    }
}

