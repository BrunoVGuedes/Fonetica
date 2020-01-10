using System;
using System.Collections.Generic;
using System.Text;

namespace Lider.DPVAT.APIFonetica.Domain.Services
{
    public class DoubleMetaphone
    {
        public const int METAPHONE_KEY_LENGTH = 15;

        private StringBuilder _PrimaryKey, _AlternateKey;
        private string _PrimaryKeyString, _AlternateKeyString;
        private int _PrimaryKeyLength, _AlternateKeyLength;
        private string _Word, _OriginalWord;
        private Palavra _Palavra;
        private char[] key;

        public Palavra Palavra
        {
            get { return _Palavra; }
            set { _Palavra = value; }
        }


        int _Length, _Last;

        bool _HasAlternate;

        public DoubleMetaphone()
        {
            this._PrimaryKey = new StringBuilder(METAPHONE_KEY_LENGTH + 2);
            this._AlternateKey = new StringBuilder(METAPHONE_KEY_LENGTH + 2);

            this.ComputeKeys(string.Empty);
        }

        public DoubleMetaphone(string word)
            : this()
        {
            this.ComputeKeys(word);
        }

        public string PrimaryKey
        {
            get { return _PrimaryKey.ToString(); }
            //get { return _PrimaryKeyString; }
        }

        public string AlternateKey
        {
            get { return _HasAlternate ? _AlternateKey.ToString() : null; }
            //get { return _HasAlternate ? _AlternateKeyString : null; }
        }



        public string Word
        {
            get { return _OriginalWord; }
        }

        public static void DoDoubleMetaphone(string word, ref string primaryKey, ref string alternateKey)
        {
            DoubleMetaphone metaphone = new DoubleMetaphone(word);

            primaryKey = metaphone.PrimaryKey;
            alternateKey = metaphone.AlternateKey;
        }

        public void ComputeKeys(string word)
        {
            this._PrimaryKey.Length = 0;
            this._AlternateKey.Length = 0;

            this._PrimaryKeyString = string.Empty;
            this._AlternateKeyString = string.Empty;

            this._PrimaryKeyLength = this._AlternateKeyLength = 0;

            this._HasAlternate = false;

            this._OriginalWord = word;

            this._Word = word;
            this._Word = string.Concat(this._Word, "    ");
            this._Word = this._Word.ToUpper();

            this._Length = this._Word.Length;

            this._Last = this._Length - 1;

            this.Palavra = Silabas.separa(this.Word);
            this.BuildMetaphoneKeys();

        }

        private void BuildMetaphoneKeys()
        {
            int current = 0;
            string encontro, encontroConsonantal;

            if (this._Length < 1)
                return;

            if (this.AreStringsAt(0, 2, "GN", "KN", "PR", "WR", "PS"))
                current += 1;

            //if (this._Word[0].Equals('X'))
            //{
            //    this.AddMetaphoneCharacter("S");
            //    current += 1;
            //}

            while ((this._PrimaryKeyLength < METAPHONE_KEY_LENGTH) || (this._AlternateKeyLength < METAPHONE_KEY_LENGTH))
            {
                if (current >= this._Length)
                    break;

                switch (this._Word[current])
                {
                    case 'A':
                        encontro = EncontroVocalico(current);
                        encontroConsonantal = EncontroVocalicoConsonantal(0);

                        if ((IndexSilaba(current) == 1 && this.Palavra.Silibas.Count == 2 && encontroConsonantal.Length < 2) || encontro.Length > 1)
                        {
                            int silaba = IndexSilaba(current);
                            if (encontro.Length == 1)
                            {
                                this.AddMetaphoneCharacter("A");
                                current++;
                                break;
                            }
                            else if (encontro.Length < 3)
                            {
                                if (!ProximoEncontroVocalico(silaba))
                                    this.AddMetaphoneCharacter(encontro.ToUpper().ToString());
                                current += 2;
                                break;
                            }
                            else
                            {
                                if (!ProximoEncontroVocalico(silaba))
                                    this.AddMetaphoneCharacter(encontro.Substring(encontro.Length - 2, 2));
                                current += encontro.Length;
                                break;
                            }

                        }
                        if (IndexSilaba(current) == this.Palavra.Silibas.Count && this.Palavra.Silibas.Count != 2)
                        {
                            this.AddMetaphoneCharacter("A");
                            current++;
                            break;
                        }
                        if (encontroConsonantal.Length < 2 && this.Palavra.Silibas.Count == 2 && IndexSilaba(current) == 1)
                        {
                            this.AddMetaphoneCharacter("A");
                            current++;
                            break;

                        }

                        if (this.Palavra.Silibas.Count == 2 && IndexSilaba(current) == 2)
                        {
                            if (encontroConsonantal.Length > 1)
                            {
                                if (EncontroVocalico(0).Length < 2)
                                {
                                    this.AddMetaphoneCharacter("A");
                                    current++;
                                    break;
                                }

                            }

                        }
                        //if (EncontroVocalico(0).Length < 2 && this.Palavra.Silibas.Count == 2 && IndexSilaba(current) == 2)
                        //{ 
                        //    this.AddMetaphoneCharacter("A");
                        //    current++;
                        //    break;
                        //}
                        current++;
                        break;

                    case 'E':
                        encontro = EncontroVocalico(current);
                        encontroConsonantal = EncontroVocalicoConsonantal(0);

                        if (encontro.Length > 1)
                        {
                            int silaba = IndexSilaba(current);
                            if (encontro.Length == 1)
                            {
                                this.AddMetaphoneCharacter("E");
                                current++;
                                break;
                            }
                            else if (encontro.Length < 3)
                            {
                                if (!ProximoEncontroVocalico(silaba))
                                    this.AddMetaphoneCharacter(encontro.ToUpper().ToString());
                                current += 2;
                                break;
                            }
                            else
                            {
                                if (!ProximoEncontroVocalico(silaba))
                                    this.AddMetaphoneCharacter(encontro.Substring(encontro.Length - 2, 2));
                                current += encontro.Length;
                                break;
                            }
                        }
                        if (IndexSilaba(current) == this.Palavra.Silibas.Count && this.Palavra.Silibas.Count != 2)
                        {
                            this.AddMetaphoneCharacter("E");
                            current++;
                            break;
                        }
                        if (encontroConsonantal.Length < 2 && this.Palavra.Silibas.Count == 2 && IndexSilaba(current) == 1)
                        {
                            this.AddMetaphoneCharacter("E");
                            current++;
                            break;

                        }
                        if (this.Palavra.Silibas.Count == 2 && IndexSilaba(current) == 2)
                        {
                            if (encontroConsonantal.Length > 1)
                            {
                                if (EncontroVocalico(0).Length < 2)
                                {
                                    this.AddMetaphoneCharacter("E");
                                    current++;
                                    break;
                                }

                            }

                        }
                        //if (EncontroVocalico(0).Length > 2)
                        //    if (encontroConsonantal.Length < 2 && this.Palavra.Silibas.Count == 2 && IndexSilaba(current) == 2)
                        //    { 
                        //        this.AddMetaphoneCharacter("E");
                        //        current++;
                        //        break;
                        //    }
                        //if (EncontroVocalico(0).Length < 2 && this.Palavra.Silibas.Count == 2 && IndexSilaba(current) == 2)
                        //{ 
                        //    this.AddMetaphoneCharacter("E");
                        //    current++;
                        //    break;
                        //}
                        current++;
                        break;
                    case 'I':
                        encontro = EncontroVocalico(current);
                        encontroConsonantal = EncontroVocalicoConsonantal(0);

                        if ((IndexSilaba(current) == 1 && this.Palavra.Silibas.Count == 2 && encontroConsonantal.Length < 2) || encontro.Length > 1)
                        {
                            int silaba = IndexSilaba(current);


                            if (encontro.Length == 1)
                            {
                                this.AddMetaphoneCharacter("I");
                                current++;
                                break;
                            }
                            else if (encontro.Length < 3)
                            {
                                if (!ProximoEncontroVocalico(silaba))
                                    this.AddMetaphoneCharacter(encontro.ToUpper().ToString());
                                current += 2;
                                break;

                            }
                            else
                            {
                                if (!ProximoEncontroVocalico(silaba))
                                    this.AddMetaphoneCharacter(encontro.Substring(encontro.Length - 2, 2));
                                current += encontro.Length;
                                break;
                            }
                        }
                        if (IndexSilaba(current) == this.Palavra.Silibas.Count && this.Palavra.Silibas.Count != 2)
                        {
                            this.AddMetaphoneCharacter("I");
                            current++;
                            break;
                        }
                        if (encontroConsonantal.Length < 2 && this.Palavra.Silibas.Count == 2 && IndexSilaba(current) == 1)
                        {
                            this.AddMetaphoneCharacter("I");
                            current++;
                            break;

                        }
                        if (this.Palavra.Silibas.Count == 2 && IndexSilaba(current) == 2)
                        {
                            if (encontroConsonantal.Length > 1)
                            {
                                if (EncontroVocalico(0).Length < 2)
                                {
                                    this.AddMetaphoneCharacter("I");
                                    current++;
                                    break;
                                }

                            }

                        }
                        current++;
                        break;
                    case 'O':
                        encontro = EncontroVocalico(current);
                        encontroConsonantal = EncontroVocalicoConsonantal(0);

                        if ((IndexSilaba(current) == 1 && this.Palavra.Silibas.Count == 2 && encontroConsonantal.Length < 2) || encontro.Length > 1)
                        {
                            int silaba = IndexSilaba(current);

                            if (encontro.Length == 1)
                            {
                                this.AddMetaphoneCharacter("O");
                                current++;
                                break;
                            }
                            else if (encontro.Length < 3)
                            {
                                if (!ProximoEncontroVocalico(silaba))
                                    this.AddMetaphoneCharacter(encontro.ToUpper().ToString());
                                current += 2;
                                break;
                            }
                            else
                            {
                                if (!ProximoEncontroVocalico(silaba))
                                    this.AddMetaphoneCharacter(encontro.Substring(encontro.Length - 2, 2));
                                current += encontro.Length;
                                break;
                            }
                        }
                        if (IndexSilaba(current) == this.Palavra.Silibas.Count && this.Palavra.Silibas.Count != 2)
                        {
                            this.AddMetaphoneCharacter("O");
                            current++;
                            break;
                        }
                        if (encontroConsonantal.Length < 2 && this.Palavra.Silibas.Count == 2 && IndexSilaba(current) == 1)
                        {
                            this.AddMetaphoneCharacter("O");
                            current++;
                            break;

                        }
                        if (this.Palavra.Silibas.Count == 2 && IndexSilaba(current) == 2)
                        {
                            if (encontroConsonantal.Length > 1)
                            {
                                if (EncontroVocalico(0).Length < 2)
                                {
                                    this.AddMetaphoneCharacter("O");
                                    current++;
                                    break;
                                }

                            }

                        }
                        //if (encontroConsonantal.Length < 2 && this.Palavra.Silibas.Count == 2 && IndexSilaba(current) == 2)
                        //{ 
                        //    this.AddMetaphoneCharacter("O");
                        //    current++;
                        //    break;
                        //}
                        //if (EncontroVocalico(0).Length < 2 && this.Palavra.Silibas.Count == 2 && IndexSilaba(current) == 2)
                        //{ 
                        //    this.AddMetaphoneCharacter("O");
                        //    current++;
                        //    break;
                        //}
                        current++;
                        break;
                    case 'U':
                        encontro = EncontroVocalico(current);

                        //if (current > 0)
                        //{
                        //    if (this._Word[current - 1].Equals('G'))
                        //    {
                        //        if (encontro.Length > )
                        //        {

                        //        }
                        //        this.AddMetaphoneCharacter("U");
                        //        current += 1;
                        //        break;
                        //    }                            
                        //}                        

                        encontroConsonantal = EncontroVocalicoConsonantal(0);

                        if ((IndexSilaba(current) == 1 && this.Palavra.Silibas.Count == 2 && encontroConsonantal.Length < 2) || encontro.Length > 1)
                        {
                            if (encontro.Length == 1)
                            {
                                this.AddMetaphoneCharacter("U");
                                current++;
                                break;
                            }
                            else if (encontro.Length < 3)
                            {
                                int silaba = IndexSilaba(current);
                                if (!ProximoEncontroVocalico(silaba))
                                    this.AddMetaphoneCharacter(encontro.ToUpper().ToString());
                                current += 2;
                                break;
                            }
                            else
                            {
                                int silaba = IndexSilaba(current);
                                if (!ProximoEncontroVocalico(silaba))
                                {
                                    if (current > 0)
                                    {
                                        if (this._Word[current - 1].Equals('G') || this._Word[current - 1].Equals('Q'))
                                        {
                                            this.AddMetaphoneCharacter("U");
                                        }
                                    }
                                    this.AddMetaphoneCharacter(encontro.Substring(encontro.Length - 2, 2));
                                }
                            }
                            current += encontro.Length;
                            break;
                        }
                        if (IndexSilaba(current) == this.Palavra.Silibas.Count && this.Palavra.Silibas.Count != 2)
                        {
                            this.AddMetaphoneCharacter("U");
                            current++;
                            break;
                        }
                        if (encontroConsonantal.Length < 2 && this.Palavra.Silibas.Count == 2 && IndexSilaba(current) == 1)
                        {
                            this.AddMetaphoneCharacter("U");
                            current++;
                            break;

                        }
                        if (this.Palavra.Silibas.Count == 2 && IndexSilaba(current) == 2)
                        {
                            if (encontroConsonantal.Length > 1)
                            {
                                if (EncontroVocalico(0).Length < 2)
                                {
                                    this.AddMetaphoneCharacter("U");
                                    current++;
                                    break;
                                }

                            }

                        }

                        current += 1;
                        break;
                    case 'Y':
                        //if (current == 0)
                        //{
                        //    this.AddMetaphoneCharacter("A");
                        //    current += 1;
                        //    break;
                        //}
                        //else 
                        //{
                        //this.AddMetaphoneCharacter("I");
                        //current += 1;
                        //break;
                        //}
                        encontro = EncontroVocalico(current);
                        encontroConsonantal = EncontroVocalicoConsonantal(0);

                        if ((IndexSilaba(current) == 1 && this.Palavra.Silibas.Count == 2 && encontroConsonantal.Length < 2) || encontro.Length > 1)
                        {
                            int silaba = IndexSilaba(current);


                            if (encontro.Length == 1)
                            {
                                this.AddMetaphoneCharacter("I");
                                current++;
                                break;
                            }
                            else if (encontro.Length < 3)
                            {
                                if (!ProximoEncontroVocalico(silaba))
                                    this.AddMetaphoneCharacter(encontro.ToUpper().Replace("Y", "I").ToString());
                                current += 2;
                                break;
                            }
                            else
                            {
                                if (!ProximoEncontroVocalico(silaba))
                                    this.AddMetaphoneCharacter(encontro.ToUpper().Replace("Y", "I").Substring(encontro.Length - 2, 2));
                                current += encontro.Length;
                                break;
                            }
                        }
                        if (IndexSilaba(current) == this.Palavra.Silibas.Count && this.Palavra.Silibas.Count != 2)
                        {
                            this.AddMetaphoneCharacter("I");
                            current++;
                            break;
                        }
                        if (encontroConsonantal.Length < 2 && this.Palavra.Silibas.Count == 2 && IndexSilaba(current) == 1)
                        {
                            this.AddMetaphoneCharacter("I");
                            current++;
                            break;

                        }
                        if (this.Palavra.Silibas.Count == 2 && IndexSilaba(current) == 2)
                        {
                            if (encontroConsonantal.Length > 1)
                            {
                                if (EncontroVocalico(0).Length < 2)
                                {
                                    this.AddMetaphoneCharacter("I");
                                    current++;
                                    break;
                                }

                            }

                        }
                        current++;
                        break;


                    case 'B':
                        this.AddMetaphoneCharacter("B", "P");
                        //this.AddMetaphoneCharacter("P");

                        if (this._Word[current + 1].Equals('B'))
                        {
                            current += 2;
                        }
                        else
                        {
                            current += 1;
                        }
                        break;

                    case 'Ç':
                        this.AddMetaphoneCharacter("S");
                        current += 1;
                        break;

                    case 'C':
                        // germânico
                        if ((current > 1) && !this.IsVowel(current - 2) && this.AreStringsAt((current - 1), 3, "ACH") && ((this._Word[current + 2] != 'I') && ((this._Word[current + 2] != 'E') || this.AreStringsAt((current - 2), 6, "BACHER", "MACHER"))))
                        {
                            this.AddMetaphoneCharacter("C", "K");
                            //this.AddMetaphoneCharacter("K");
                            current += 2;
                            break;
                        }

                        // caso especial 'caesar'
                        if ((current == 0) && this.AreStringsAt(current, 6, "CAESAR"))
                        {
                            this.AddMetaphoneCharacter("C", "S");
                            //this.AddMetaphoneCharacter("S");
                            //current += 2;
                            current += 1;
                            break;
                        }

                        //// italiano 'chianti'
                        //if (this.AreStringsAt(current, 4, "CHIA"))
                        //{
                        //    this.AddMetaphoneCharacter("K");
                        //    current += 2;
                        //    break;
                        //}

                        if (this.AreStringsAt(current, 2, "CH"))
                        {
                            //// encontra 'michael'
                            //if ((current > 0) && this.AreStringsAt(current, 4, "CHAE"))
                            //{
                            //    this.AddMetaphoneCharacter("K", "X");
                            //    current += 2;
                            //    break;
                            //}

                            //// raízes gregas ex.: 'chemistry', 'chorus'
                            //if ((current == 0) && (this.AreStringsAt((current + 1), 5, "HARAC", "HARIS") || this.AreStringsAt((current + 1), 3, "HOR", "HYM", "HIA", "HEM")) && !this.AreStringsAt(0, 5, "CHORE"))
                            //{
                            //    this.AddMetaphoneCharacter("K");
                            //    current += 2;
                            //    break;
                            //}

                            // germânico, grego... ou 'ch' para som 'kh'
                            //if ((this.AreStringsAt(0, 4, "VAN ", "VON ") || this.AreStringsAt(0, 3, "SCH")) ||

                            //    // 'architect mas não 'arch', 'orchestra', 'orchid'...
                            //    this.AreStringsAt((current - 2), 6, "ORCHES", "ARCHIT", "ORCHID") ||
                            //    this.AreStringsAt((current + 2), 1, "T", "S") ||
                            //    ((this.AreStringsAt((current - 1), 1, "A", "O", "U", "E") || (current == 0)) &&

                            //    // ex.: 'wachtler', 'wechsler', mas não 'tichner'...
                            //    this.AreStringsAt((current + 2), 1, "L", "R", "N", "M", "B", "H", "F", "V", "W", " ")))
                            //{
                            //    this.AddMetaphoneCharacter("K");
                            //}
                            //else
                            //{
                            //    if (current > 0)
                            //    {
                            //        if (this.AreStringsAt(0, 2, "MC"))
                            //        {
                            //            // ex.: "McHugh"
                            //            this.AddMetaphoneCharacter("K");
                            //        }
                            //        else
                            //        {
                            //            this.AddMetaphoneCharacter("X", "K");
                            //        }
                            //    }
                            //    else
                            //    {
                            //        this.AddMetaphoneCharacter("X");
                            //    }
                            //}
                            this.AddMetaphoneCharacter("X");
                            current += 2;
                            break;
                        }
                        //// ex.: 'czerny'
                        //if (this.AreStringsAt(current, 2, "CZ") && !this.AreStringsAt((current - 2), 4, "WICZ"))
                        //{
                        //    this.AddMetaphoneCharacter("S", "X");
                        //    current += 2;
                        //    break;
                        //}

                        //// ex.: 'focaccia'
                        //if (this.AreStringsAt((current + 1), 3, "CIA"))
                        //{
                        //    this.AddMetaphoneCharacter("X");
                        //    current += 3;
                        //    break;
                        //}

                        //// duplo 'C', mas não se ex.: 'McClellan'
                        //if (this.AreStringsAt(current, 2, "CC") && !((current == 1) && (this._Word[0] == 'M')))
                        //{
                        //    // 'bellocchio' mas não 'bacchus'...
                        //    if (this.AreStringsAt((current + 2), 1, "I", "E", "H") && !this.AreStringsAt((current + 2), 2, "HU"))
                        //    {
                        //        if (((current == 1) && (this._Word[current - 1] == 'A')) || this.AreStringsAt((current - 1), 5, "UCCEE", "UCCES"))
                        //        {
                        //            // 'accident', 'accede', 'succeed'...
                        //            this.AddMetaphoneCharacter("KS");
                        //        }
                        //        else
                        //        {
                        //            // 'bacci', 'bertucci', outros italianos...
                        //            this.AddMetaphoneCharacter("X");
                        //        }

                        //        current += 3;
                        //        break;
                        //    }
                        //    else
                        //    {
                        //        // regra de Pierce...
                        //        this.AddMetaphoneCharacter("K");
                        //        current += 2;
                        //        break;
                        //    }
                        //}

                        //if (this.AreStringsAt(current, 2, "CK", "CG", "CQ"))
                        //{
                        //    this.AddMetaphoneCharacter("K");
                        //    current += 2;
                        //    break;
                        //}

                        if (this.AreStringsAt(current, 2, "CI", "CE", "CY"))
                        {
                            // italiano vs. inglês
                            if (this.AreStringsAt(current, 3, "CIO", "CIE", "CIA"))
                            {
                                this.AddMetaphoneCharacter("S", "X");
                            }
                            else
                            {
                                this.AddMetaphoneCharacter("S");
                            }

                            //current += 2;
                            current += 1;
                            break;
                        }

                        // else
                        this.AddMetaphoneCharacter("C", "K");
                        current += 1;
                        //// nome enviado em 'mac caffrey', 'mac gregor'...
                        //if (this.AreStringsAt((current + 1), 2, " C", " Q", " G"))
                        //{
                        //    current += 3;
                        //}
                        //else
                        //{
                        //    if (this.AreStringsAt((current + 1), 1, "C", "K", "Q") && !this.AreStringsAt((current + 1), 2, "CE", "CI"))
                        //    {
                        //        current += 2;
                        //    }
                        //    else
                        //    {
                        //        current += 1;
                        //    }
                        //}

                        break;

                    case 'D':
                        //if (this.AreStringsAt(current, 2, "DG"))
                        //{
                        //    if (this.AreStringsAt((current + 2), 1, "I", "E", "Y"))
                        //    {
                        //        // ex.: 'edge'
                        //        this.AddMetaphoneCharacter("J");
                        //        current += 3;
                        //        break;
                        //    }
                        //    else
                        //    {
                        //        // ex.: 'edgar'
                        //        this.AddMetaphoneCharacter("TK");
                        //        current += 2;
                        //        break;
                        //    }
                        //}

                        //if (this.AreStringsAt(current, 2, "DT", "DD"))
                        //{
                        //    this.AddMetaphoneCharacter("T");
                        //    current += 2;
                        //    break;
                        //}

                        // else
                        //this.AddMetaphoneCharacter("T");
                        this.AddMetaphoneCharacter("D");
                        current += 1;
                        break;

                    case 'F':
                        if (this._Word[current + 1].Equals('F'))
                        {
                            current += 2;
                        }
                        else
                        {
                            current += 1;
                        }

                        this.AddMetaphoneCharacter("F");
                        break;

                    case 'G':
                        //if (this._Word[current + 1].Equals('H'))
                        //{
                        //    if ((current > 0) && !this.IsVowel(current - 1))
                        //    {
                        //        this.AddMetaphoneCharacter("K");
                        //        current += 2;
                        //        break;
                        //    }

                        //    if (current < 3)
                        //    {
                        //        // 'ghislane', ghiradelli...
                        //        if (current == 0)
                        //        {
                        //            if (this._Word[current + 2].Equals('I'))
                        //                this.AddMetaphoneCharacter("J");
                        //            else
                        //                this.AddMetaphoneCharacter("K");

                        //            current += 2;
                        //            break;
                        //        }
                        //    }

                        //    // regra de Parker (com alguns refinamentos) ex.: 'hugh'
                        //    if (((current > 1) && this.AreStringsAt((current - 2), 1, "B", "H", "D"))

                        //        // ex.: 'bough'
                        //        || ((current > 2) && this.AreStringsAt((current - 3), 1, "B", "H", "D"))

                        //        // ex.: 'broughton'
                        //        || ((current > 3) && this.AreStringsAt((current - 4), 1, "B", "H")))
                        //    {
                        //        current += 2;
                        //        break;
                        //    }
                        //    else
                        //    {
                        //        // ex.: 'laugh', 'McLaughlin', 'cough', 'gough', 'rough', 'tough'
                        //        if ((current > 2) && (this._Word[current - 1].Equals('U')) && this.AreStringsAt((current - 3), 1, "C", "G", "L", "R", "T"))
                        //        {
                        //            this.AddMetaphoneCharacter("F");
                        //        }
                        //        else
                        //            if ((current > 0) && this._Word[current - 1] != 'I')
                        //                this.AddMetaphoneCharacter("K");

                        //        current += 2;
                        //        break;
                        //    }
                        //}

                        //if (this._Word[current + 1].Equals('N'))
                        //{
                        //    if ((current == 1) && this.IsVowel(0) && !this.IsWordSlavoGermanic())
                        //    {
                        //        this.AddMetaphoneCharacter("KN", "N");
                        //    }
                        //    else
                        //        // nao... ex.: 'cagney'
                        //        if (!this.AreStringsAt((current + 2), 2, "EY") && (this._Word[current + 1] != 'Y') && !this.IsWordSlavoGermanic())
                        //        {
                        //            this.AddMetaphoneCharacter("N", "KN");
                        //        }
                        //        else
                        //        {
                        //            this.AddMetaphoneCharacter("KN");
                        //        }

                        //    current += 2;
                        //    break;
                        //}

                        // 'tagliaro'
                        //if (this.AreStringsAt((current + 1), 2, "LI") && !this.IsWordSlavoGermanic())
                        //{
                        //    this.AddMetaphoneCharacter("KL", "L");
                        //    current += 2;
                        //    break;
                        //}

                        // -ges-, -gep-, -gel-, -gie- no início...
                        //if ((current == 0) && ((this._Word[current + 1].Equals('Y')) || this.AreStringsAt((current + 1), 2, "ES", "EP", "EB", "EL", "EY", "IB", "IL", "IN", "IE", "EI", "ER")))
                        //{
                        //    this.AddMetaphoneCharacter("K", "J");
                        //    current += 2;
                        //    break;
                        //}

                        // -ger-,  -gy-
                        //if ((this.AreStringsAt((current + 1), 2, "ER") || (this._Word[current + 1].Equals('Y'))) &&
                        //    !this.AreStringsAt(0, 6, "DANGER", "RANGER", "MANGER") &&
                        //    !this.AreStringsAt((current - 1), 1, "E", "I") &&
                        //    !this.AreStringsAt((current - 1), 3, "RGY", "OGY"))
                        //{
                        //    this.AddMetaphoneCharacter("K", "J");
                        //    current += 2;
                        //    break;
                        //}

                        // italiano ex.: 'biaggi'
                        //if (this.AreStringsAt((current + 1), 1, "E", "I", "Y") || this.AreStringsAt((current - 1), 4, "AGGI", "OGGI"))
                        //{
                        //    // germânico óbveis
                        //    if ((this.AreStringsAt(0, 4, "VAN ", "VON ") || this.AreStringsAt(0, 3, "SCH")) || this.AreStringsAt((current + 1), 2, "ET"))
                        //    {
                        //        this.AddMetaphoneCharacter("K");
                        //    }
                        //    else
                        //    {
                        //        // sempre suavizar se o final for francês...
                        //        if (this.AreStringsAt((current + 1), 4, "IER "))
                        //        {
                        //            this.AddMetaphoneCharacter("J");
                        //        }
                        //        else
                        //        {
                        //            this.AddMetaphoneCharacter("J", "K");
                        //        }
                        //    }

                        //    current += 2;
                        //    break;
                        //}

                        //nova regra somente portugues

                        //if (this._Word[current + 1].Equals('U'))
                        //{
                        this.AddMetaphoneCharacter("G", "K");
                        //current += 1;
                        //break;
                        //}

                        if (this._Word[current + 1].Equals('G'))
                        {
                            current += 2;
                        }
                        else
                        {
                            current += 1;
                        }

                        //this.AddMetaphoneCharacter("K");
                        break;

                    case 'H':

                        if (current == 0 && this.IsVowel(current + 1))
                        {
                            this.AddMetaphoneCharacter(this.Word[current + 1].ToString());
                            current += 2;
                        }
                        // somente mantém se primeira & vogal anterior ou entre 2 vogais...
                        else if (((current == 0) || this.IsVowel(current - 1)) && this.IsVowel(current + 1))
                        {
                            this.AddMetaphoneCharacter("H");
                            current += 2;
                        }
                        else
                        {
                            if (this.AreStringsAt(current - 1, 2, "NH", "LH"))
                            {
                                this.AddMetaphoneCharacter("I");
                                current += 1;
                                break;
                            }
                            // também toma cuidado com 'HH'
                            current += 1;
                        }

                        break;

                    case 'J':
                        // espanhóis óbveis... 'jose', 'san jacinto'
                        //if (this.AreStringsAt(current, 4, "JOSE") || this.AreStringsAt(0, 4, "SAN "))
                        //{
                        //    if (((current == 0) && (this._Word[current + 4] == ' ')) || this.AreStringsAt(0, 4, "SAN "))
                        //    {
                        //        this.AddMetaphoneCharacter("H");
                        //    }
                        //    else
                        //    {
                        //        this.AddMetaphoneCharacter("J", "H");
                        //    }
                        //    current += 1;
                        //    break;
                        //}

                        if ((current == 0) && !this.AreStringsAt(current, 4, "JOSE"))
                            this.AddMetaphoneCharacter("J", "A"); // Yankelovich/Jankelowicz
                        else
                            // pronomes espanhóis... ex.: 'bajador'
                            if (this.IsVowel(current - 1) && !this.IsWordSlavoGermanic() && ((this._Word[current + 1] == 'A') || (this._Word[current + 1] == 'O')))
                            this.AddMetaphoneCharacter("J", "H");
                        else
                                if (current == this._Last)
                            this.AddMetaphoneCharacter("J", " ");
                        else
                                    if (!this.AreStringsAt((current + 1), 1, "L", "T", "K", "S", "N", "M", "B", "Z") && !this.AreStringsAt((current - 1), 1, "S", "K", "L")) this.AddMetaphoneCharacter("J");

                        // isso pode acontecer!
                        if (this._Word[current + 1].Equals('J'))
                            current += 2;
                        else
                            current += 1;
                        break;

                    case 'K':
                        if (this._Word[current + 1] == 'K')
                            current += 2;
                        else
                            current += 1;
                        //this.AddMetaphoneCharacter("K");
                        this.AddMetaphoneCharacter("C", "K");
                        break;

                    case 'L':
                        if (this._Word[current + 1].Equals('L'))
                        {
                            // spanish e.g. 'cabrillo', 'gallegos'
                            //if (((current == (this._Length - 3))
                            //     && this.AreStringsAt((current - 1), 4, "ILLO", "ILLA", "ALLE"))
                            //    || ((this.AreStringsAt((this._Last - 1), 2, "AS", "OS") || this.AreStringsAt(this._Last, 1, "A", "O"))
                            //        && this.AreStringsAt((current - 1), 4, "ALLE")))
                            //{
                            //    this.AddMetaphoneCharacter("L", " ");
                            //    current += 2;
                            //    break;
                            //}
                            current += 2;
                        }
                        else
                            current += 1;
                        this.AddMetaphoneCharacter("L");
                        break;

                    case 'M':
                        //if ((this.AreStringsAt((current - 1), 3, "UMB") && (((current + 1) == this._Last) || this.AreStringsAt((current + 2), 2, "ER")))
                        //    //'dumb','thumb'
                        //    || (this._Word[current + 1].Equals('M')))
                        //    current += 2;
                        //else
                        //    current += 1;
                        //this.AddMetaphoneCharacter("M");
                        //break;

                        if (this._Word[current + 1].Equals('M'))
                            current += 2;
                        else
                            current += 1;
                        this.AddMetaphoneCharacter("M");
                        break;

                    case 'N':
                        if (this._Word[current + 1].Equals('N'))
                            current += 2;
                        else
                            current += 1;
                        this.AddMetaphoneCharacter("N");
                        break;

                    case 'Ñ': // '�'
                        current += 1;
                        this.AddMetaphoneCharacter("N");
                        break;

                    case 'P':
                        if (this._Word[current + 1].Equals('H'))
                        {
                            this.AddMetaphoneCharacter("F");
                            current += 2;
                            break;
                        }

                        //also account for "campbell", "raspberry"
                        if (this.AreStringsAt((current + 1), 1, "P", "B"))
                            current += 2;
                        else
                            current += 1;
                        this.AddMetaphoneCharacter("P");
                        break;

                    case 'Q':
                        if (this._Word[current + 1].Equals('Q'))
                            current += 2;
                        else
                            current += 1;
                        //this.AddMetaphoneCharacter("K");
                        //if (this.Word[current +1].Equals('U'))
                        //{

                        //}
                        this.AddMetaphoneCharacter("C", "K");
                        break;

                    case 'R':
                        //french e.g. 'rogier', but exclude 'hochmeier'
                        //if ((current == this._Last)
                        //    && !this.IsWordSlavoGermanic()
                        //    && this.AreStringsAt((current - 2), 2, "IE")
                        //    && !this.AreStringsAt((current - 4), 2, "ME", "MA"))
                        //    this.AddMetaphoneCharacter("", "R");
                        //else
                        //    this.AddMetaphoneCharacter("R");

                        this.AddMetaphoneCharacter("R");

                        if (this._Word[current + 1].Equals('R'))
                            current += 2;
                        else
                            current += 1;
                        break;

                    case 'S':
                        //special cases 'island', 'isle', 'carlisle', 'carlysle'
                        //if (this.AreStringsAt((current - 1), 3, "ISL", "YSL"))
                        //{
                        //    current += 1;
                        //    break;
                        //}

                        //special case 'sugar-'
                        //if ((current == 0) && this.AreStringsAt(current, 5, "SUGAR"))
                        //{
                        //    this.AddMetaphoneCharacter("X", "S");
                        //    current += 1;

                        //    break;
                        //}

                        //if (this.AreStringsAt(current, 2, "SH"))
                        //{
                        //    //germanic
                        //    if (this.AreStringsAt((current + 1), 4, "HEIM", "HOEK", "HOLM", "HOLZ"))
                        //    {
                        //        this.AddMetaphoneCharacter("S");
                        //    }
                        //    else
                        //    {
                        //        this.AddMetaphoneCharacter("X");
                        //    }

                        //    current += 2;

                        //    break;
                        //}

                        ////italian & armenian
                        //if (this.AreStringsAt(current, 3, "SIO", "SIA") || this.AreStringsAt(current, 4, "SIAN"))
                        //{
                        //    if (!this.IsWordSlavoGermanic())
                        //    {
                        //        this.AddMetaphoneCharacter("S", "X");
                        //    }
                        //    else
                        //    {
                        //        this.AddMetaphoneCharacter("S");
                        //    }

                        //    current += 3;

                        //    break;
                        //}

                        //german & anglicisations, e.g. 'smith' match 'schmidt', 'snider' match 'schneider'
                        //also, -sz- in slavic language altho in hungarian it is pronounced 's'
                        //if (((current == 0) && this.AreStringsAt((current + 1), 1, "M", "N", "L", "W")) || this.AreStringsAt((current + 1), 1, "Z"))
                        //{
                        //    this.AddMetaphoneCharacter("S", "X");

                        //    if (this.AreStringsAt((current + 1), 1, "Z"))
                        //    {
                        //        current += 2;
                        //    }
                        //    else
                        //    {
                        //        current += 1;
                        //    }

                        //    break;
                        //}

                        //if (this.AreStringsAt(current, 2, "SC"))
                        //{
                        //    //Schlesinger's rule
                        //    if (this._Word[current + 2] == 'H')
                        //        //dutch origin, e.g. 'school', 'schooner'
                        //        if (this.AreStringsAt((current + 3), 2, "OO", "ER", "EN", "UY", "ED", "EM"))
                        //        {
                        //            //'schermerhorn', 'schenker'
                        //            if (this.AreStringsAt((current + 3), 2, "ER", "EN"))
                        //            {
                        //                this.AddMetaphoneCharacter("X", "SK");
                        //            }
                        //            else
                        //                this.AddMetaphoneCharacter("SK");
                        //            current += 3;
                        //            break;
                        //        }
                        //        else
                        //        {
                        //            if ((current == 0) && !this.IsVowel(3) && (this._Word[3] != 'W'))
                        //                this.AddMetaphoneCharacter("X", "S");
                        //            else
                        //                this.AddMetaphoneCharacter("X");
                        //            current += 3;
                        //            break;
                        //        }

                        //    if (this.AreStringsAt((current + 2), 1, "I", "E", "Y"))
                        //    {
                        //        this.AddMetaphoneCharacter("S");
                        //        current += 3;
                        //        break;
                        //    }
                        //    //else
                        //    this.AddMetaphoneCharacter("SK");
                        //    current += 3;
                        //    break;
                        //}

                        if (this.AreStringsAt(current, 2, "SH"))
                        {
                            this.AddMetaphoneCharacter("X", "S");
                            current += 2;
                            break;
                        }

                        this.AddMetaphoneCharacter("S");
                        if (this._Word[current + 1].Equals('S'))
                            current += 2;
                        else
                            current += 1;
                        break;

                    ////french e.g. 'resnais', 'artois'
                    //if ((current == this._Last) && this.AreStringsAt((current - 2), 2, "AI", "OI"))
                    //    this.AddMetaphoneCharacter("", "S");
                    //else
                    //    this.AddMetaphoneCharacter("S");

                    //if (this.AreStringsAt((current + 1), 1, "S", "Z"))
                    //    current += 2;
                    //else
                    //    current += 1;
                    //break;

                    case 'T':
                        //if (this.AreStringsAt(current, 4, "TION"))
                        //{
                        //    this.AddMetaphoneCharacter("X");
                        //    current += 3;
                        //    break;
                        //}

                        //if (this.AreStringsAt(current, 3, "TIA", "TCH", "TYA"))
                        //if (this.AreStringsAt(current, 3, "TCH"))
                        //{
                        //    this.AddMetaphoneCharacter("X");
                        //    current += 3;
                        //    break;
                        //}

                        if (this.AreStringsAt(current, 2, "TH")
                            || this.AreStringsAt(current, 3, "TTH"))
                        {
                            //special case 'thomas', 'thames' or germanic
                            if (this.AreStringsAt((current + 2), 2, "OM", "AM")
                                || this.AreStringsAt(0, 4, "VAN ", "VON ")
                                || this.AreStringsAt(0, 3, "SCH"))
                            {
                                this.AddMetaphoneCharacter("T");
                            }
                            else
                            {
                                //this.AddMetaphoneCharacter("0", "T");
                                this.AddMetaphoneCharacter("T", "O");
                            }
                            current += 2;
                            break;
                        }

                        if (this.AreStringsAt((current + 1), 1, "T", "D"))
                            current += 2;
                        else
                            current += 1;
                        this.AddMetaphoneCharacter("T");
                        break;

                    case 'V':
                        if (this._Word[current + 1].Equals('V'))
                            current += 2;
                        else
                            current += 1;
                        //this.AddMetaphoneCharacter("F");
                        this.AddMetaphoneCharacter("V", "F");
                        break;

                    case 'W':
                        //can also be in middle of word
                        //if (this.AreStringsAt(current, 2, "WR"))
                        //{
                        //    this.AddMetaphoneCharacter("R");
                        //    current += 2;
                        //    break;
                        //}

                        //if ((current == 0)
                        //    && (this.IsVowel(current + 1) || this.AreStringsAt(current, 2, "WH")))
                        //{
                        //    //Wasserman should match Vasserman
                        //    if (this.IsVowel(current + 1))
                        //        this.AddMetaphoneCharacter("A", "F");
                        //    else
                        //        //need Uomo to match Womo
                        //        this.AddMetaphoneCharacter("A");
                        //}

                        ////Arnow should match Arnoff
                        //if (((current == this._Last) && this.IsVowel(current - 1)) 
                        //    || this.AreStringsAt((current - 1), 5, "EWSKI", "EWSKY", "OWSKI", "OWSKY")
                        //    || this.AreStringsAt(0, 3, "SCH"))
                        //{
                        //    this.AddMetaphoneCharacter("", "F");
                        //    current += 1;
                        //    break;
                        //}

                        ////polish e.g. 'filipowicz'
                        //if (this.AreStringsAt(current, 4, "WICZ", "WITZ"))
                        //{
                        //    this.AddMetaphoneCharacter("TS", "FX");
                        //    current += 4;
                        //    break;
                        //}

                        this.AddMetaphoneCharacter("U", "V");

                        //else skip it
                        current += 1;
                        break;

                    case 'X':
                        //french e.g. breaux
                        //if (!((current == this._Last)
                        //      && (this.AreStringsAt((current - 3), 3, "IAU", "EAU")
                        //           || this.AreStringsAt((current - 2), 2, "AU", "OU"))))
                        //    //this.AddMetaphoneCharacter("KS");
                        //    this.AddMetaphoneCharacter("X","KS");

                        //if (this.AreStringsAt((current + 1), 1, "C", "X"))
                        //    current += 2;
                        //else
                        //    current += 1;
                        //break;
                        this.AddMetaphoneCharacter("X", "KS");
                        current += 1;
                        break;

                    case 'Z':
                        //chinese pinyin e.g. 'zhao'
                        //if (this._Word[current + 1].Equals('H'))
                        //{
                        //    this.AddMetaphoneCharacter("J");
                        //    current += 2;
                        //    break;
                        //}
                        //else
                        //    if (this.AreStringsAt((current + 1), 2, "ZO", "ZI", "ZA")
                        //        || (this.IsWordSlavoGermanic() && ((current > 0) && this._Word[current - 1] != 'T')))
                        //    {
                        //        this.AddMetaphoneCharacter("S", "TS");
                        //    }
                        //    else
                        //        this.AddMetaphoneCharacter("S");

                        this.AddMetaphoneCharacter("S");

                        if (this._Word[current + 1].Equals('Z'))
                            current += 2;
                        else
                            current += 1;
                        break;

                    default:
                        current += 1;
                        break;
                }
            }
        }

        private bool IsWordSlavoGermanic()
        {
            if ((this._Word.IndexOf("W") != -1) || (this._Word.IndexOf("K") != -1) || (this._Word.IndexOf("CZ") != -1) || (this._Word.IndexOf("WITZ") != -1))
                return true;

            return false;
        }

        private bool IsVowel(int index)
        {
            if ((index < 0) || (index >= this._Length))
                return false;

            char it = this._Word[index];

            if (it.Equals('A') || it.Equals('E') || it.Equals('I') || it.Equals('O') || it.Equals('U') || it.Equals('Y'))
                return true;

            return false;
        }
        private bool IsVowel(int index, string palavra)
        {
            if ((index < 0) || (index >= this._Length))
                return false;

            char it = palavra[index];

            if (it.Equals('A') || it.Equals('E') || it.Equals('I') || it.Equals('O') || it.Equals('U') || it.Equals('Y'))
                return true;

            return false;
        }

        private void AddMetaphoneCharacter(string primaryCharacter)
        {
            this.AddMetaphoneCharacter(primaryCharacter, null);
        }

        private void AddMetaphoneCharacter(string primaryCharacter, string alternateCharacter)
        {
            if (primaryCharacter.Length > 0)
            {
                int i = 0;

                while (i < primaryCharacter.Length)
                {
                    this._PrimaryKey.Length++;
                    this._PrimaryKey[this._PrimaryKeyLength++] = primaryCharacter[i++];
                }
            }

            if (alternateCharacter != null)
            {
                if (alternateCharacter.Length > 0)
                {
                    this._HasAlternate = true;

                    if (alternateCharacter[0] != ' ')
                    {
                        int i = 0;

                        while (i < alternateCharacter.Length)
                        {
                            this._AlternateKey.Length++;
                            this._AlternateKey[this._AlternateKeyLength++] = alternateCharacter[i++];
                        }
                    }
                }
                else
                {
                    if (primaryCharacter.Length > 0 && (primaryCharacter[0] != ' '))
                    {
                        int i = 0;

                        while (i < primaryCharacter.Length)
                        {
                            this._AlternateKey.Length++;
                            this._AlternateKey[this._AlternateKeyLength++] = primaryCharacter[i++];
                        }
                    }
                }
            }
            else if (primaryCharacter.Length > 0)
            {
                int i = 0;

                while (i < primaryCharacter.Length)
                {
                    this._AlternateKey.Length++;
                    this._AlternateKey[this._AlternateKeyLength++] = primaryCharacter[i++];
                }
            }
        }

        private bool AreStringsAt(int start, int length, params string[] strings)
        {
            if (start < 0)
            {
                return false;
            }

            string target = this._Word.Substring(start, length);

            for (int i = 0; i < strings.Length; i++)
            {
                if (strings[i].Equals(target))
                    return true;
            }

            return false;
        }

        private int IndexSilaba(int index)
        {
            int cont = -1;
            int i = 0;
            for (; cont < index; i++)
            {

                cont += this.Palavra.Silibas[i].quantidade;
            }
            return i;
        }

        private string EncontroVocalico(int index)
        {
            StringBuilder sub = new StringBuilder();
            int silaba = IndexSilaba(index), i = 0;

            for (; i < this.Palavra.Silibas[silaba - 1].quantidade; i++)
            {
                if (this.IsVowel(i, this.Palavra.Silibas[silaba - 1].letra))
                    sub.Append(this.Palavra.Silibas[silaba - 1].letra[i]);
            }
            return sub.ToString();
        }

        private string EncontroVocalicoConsonantal(int index)
        {
            StringBuilder sub = new StringBuilder();
            int silaba = IndexSilaba(index), i = 0;
            for (; i < this.Palavra.Silibas[silaba - 1].quantidade; i++)
            {
                if (!this.IsVowel(i, this.Palavra.Silibas[silaba - 1].letra))
                    sub.Append(this.Palavra.Silibas[silaba - 1].letra[i]);
                else
                    return sub.ToString();
            }
            return sub.ToString();
        }

        private bool ProximoEncontroVocalico(int silaba)
        {
            int j = 0;
            if (silaba < (this.Palavra.Silibas.Count))
            {
                for (int i = 0; i < this.Palavra.Silibas[silaba].quantidade; i++)
                {
                    if (this.IsVowel(i, this.Palavra.Silibas[silaba].letra))
                        j++;
                }
                return (ProximoEncontroVocalico(silaba + 1) == true || j > 1) ? true : false;
            }
            else return false;
        }

    }
}

