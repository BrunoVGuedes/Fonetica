using System.Linq;

namespace Lider.DPVAT.APIFonetica.Domain.Services
{
    public class Metaphone
    {
        private string _Nome;
        private char[] _Chave;

        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }

        public char[] Chave
        {
            get { return _Chave; }
            set { _Chave = value; }
        }

        public Metaphone(string nome)
        {
            this._Nome = nome;
            this._Chave = new char[nome.Count()];
            this.BuildMetaphoneKeys();
        }



        private void BuildMetaphoneKeys()
        {
            Chave chave = new Chave(this._Nome);

            //retirar letras repetidas
            chave.RemoverRepeticoes();
            this._Nome = chave.Origem;

            //Aplicando regra das consoantes e extrair a chave
            Consoantes consoante = new Consoantes(this._Nome);
            chave = consoante.RegraConsoante(this._Nome);

            //alterar "E" por "I"
            chave.Excecao();

            //retirar letras repetidas
            chave.RemoverRepeticoes();

            //inserir todos encontros vocalicos na chave
            Encontro encontro = new Encontro();
            chave = encontro.InsertEncontroVocalico(chave);

            //case quantidades de letras

            Vogais vogal = new Vogais();
            chave = vogal.RegraVogais(chave);

            this._Chave = chave.Key;
            this._Nome = chave.Origem;
        }

        private char[] RegraConsoante(string nome)
        {
            return null;
        }

        private char[] RegraEncontrovocalico()
        {
            return null;
        }

        private char[] RetirarRepeticoes()
        {
            return null;
        }

        private int IndexSilaba(int index)
        {
            int cont = -1;
            int i = 0;
            for (; cont < index; i++)
            {

                //cont += this.Palavra.Silibas[i].quantidade;
            }
            return i;
        }

    }
}

