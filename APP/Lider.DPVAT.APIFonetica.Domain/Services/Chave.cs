using System;
using System.Linq;

namespace Lider.DPVAT.APIFonetica.Domain.Services
{
    public class Chave
    {
        private char[] _Key;
        private string _Origem;

        public string Origem
        {
            get { return _Origem; }
            set { _Origem = value; }
        }

        public char[] Key
        {
            get { return _Key; }
            set { _Key = value; }
        }

        public Chave(string nome)
        {
            this.Origem = nome;
        }

        public Chave(char[] chave)
        {
            _Key = chave;
        }

        public void AddMetaphoneCharacter(string primaryCharacter, int index)
        {
            if (this.Key == null) this.Key = new char[this.Origem.Length];
            for (int i = 0; i < primaryCharacter.Length; i++)
            {
                if (primaryCharacter.Length > 1)
                {
                    if ((primaryCharacter.Length - i) < 3)
                    {
                        this._Key[index + i] = Char.Parse(primaryCharacter.Substring(i, 1));
                        this.Origem = this.Origem.Remove(i + index, 1);
                        this._Origem = this._Origem.Insert(index + i, primaryCharacter.Substring(i, 1));
                    }
                }
                else
                {
                    this._Key[index + i] = Char.Parse(primaryCharacter.Substring(i, 1));
                    this.Origem = this.Origem.Remove(i + index, 1);
                    this._Origem = this._Origem.Insert(index + i, primaryCharacter.Substring(i, 1));
                }
            }

        }

        public void RemoverRepeticoes()
        {
            //if(this.Key != null) 
            //for (int i = 0; i < this._Origem.Length - 1; i++)
            //{
            //    if (i < this._Origem.Length) if (this._Key[i] == this._Key[i + 1]) if (this.Key[i] != '\0')
            //            {
            //                //this._Key[i + 1] = '\0';

            //                for (int j = i; j < this._Origem.Length -1; j++)
            //                {
            //                    this._Key[j] = this._Key[j + 1];
            //                    if (j == this._Origem.Length - 1)
            //                        this._Key[j] = '\0';
            //                }
            //            }
            //}

            foreach (var item in Constantes.LETRASREPETIDAS)
            {
                if (this.Origem.Contains(item))
                {
                    int index = this.Origem.IndexOf(item);

                    if (this.Key != null)
                        for (int j = index; j < this._Origem.Length - 1; j++)
                        {
                            this._Key[j] = this._Key[j + 1];
                            if (j == this._Origem.Length - 2)
                                this._Key[j + 1] = '\0';
                        }
                    this.Origem = this.Origem.Replace(item, item.Substring(0, 1));
                }
            }

        }

        public void Excecao()
        {
            for (int i = 0; i < this._Origem.Count(); i++)
            {
                if (this._Origem[i] == 'E')
                {
                    if (i > 0)
                    {
                        if (this._Origem[i - 1] != 'I')
                        {
                            this.Origem = this.Origem.Remove(i, 1);
                            this._Origem = this._Origem.Insert(i, "I");
                            continue;
                        }
                    }
                    if (i < this._Origem.Count() - 1)
                        if (this._Origem[i + 1] != 'U')
                        {
                            this.Origem = this.Origem.Remove(i, 1);
                            this._Origem = this._Origem.Insert(i, "I");
                        }
                }
            }

            //this._Origem.Replace("IE","II");
            //this._Origem.Replace("EU", "II");
        }

    }
}

