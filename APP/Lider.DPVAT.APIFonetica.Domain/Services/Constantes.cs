using System;
using System.Collections.Generic;
using System.Text;

namespace Lider.DPVAT.APIFonetica.Domain.Services
{
    public class Constantes
    {
        public static char[] CONSOANTES = new char[] {
        'b', 'c', 'd', 'f', 'g', 'h', 'j', 'l', 'm', 'n',
        'p', 'q', 'r', 's', 't', 'v', 'x', 'z', 'ç'
         };

        public static string[] LETRASREPETIDAS = new string[] {
        "AA","BB","CC", "DD", "EE", "FF", "GG", "HH", "II", "JJ", "LL", "MM", "NN",
        "OO", "PP", "QQ", "RR", "SS", "TT", "UU", "VV", "XX", "ZZ", "WW", "YY", "KK"};

        public static char[] VOGAIS = new char[] {
        'A', 'E', 'I', 'O', 'U'
    };
        public static char[] CONJ_1 = new char[] {
        'b', 'c', 'd', 'f', 'g', 'p', 't', 'v'
    };
        public static char[] CONJ_2 = new char[] {
        'c', 'l', 'n'
    };
        public static char[] LATERAIS = new char[] {
        'l', 'r', 'z'
    };
        public static char[] H = new char[]  {
        'h'
    };
        public static char[] CONJ_5 = new char[] {
        'c', 'g', 'm', 'p'
    };
        public static char[] CONJ_6 = new char[] {
        'n'
    };
        public static char[] ACENTOS_GA = new char[] {
        'à', 'á', 'é', 'í', 'ó', 'ú'
    };
        public static char[] CIRCUNFLEXO = new char[] {
        'â', 'ê', 'î', 'ô'
    };
        public static char[] SEMI_VOGAIS = new char[] {
        'i', 'u'
    };
        public static char[] TIL = new char[] {
        'ã', 'õ'
    };
        public static char[] NASAIS = new char[] {
        'm', 'n'
    };
        public static char[] CONJ_8 = new char[] {
        'q', 'g'
    };
        public static char[] HIFEN = new char[] {
        '-'
    };
        public static string[] MONOSSILABOS_ATONOS = new string[]{
        "o", "a", "os", "as", "um", "uns", "me", "te", "se", "lhe",
        "nos", "lhes", "que", "com", "de", "por", "sem", "sob", "e", "mas",
        "nem", "ou"
    };
        public static string PROLONGAMENTO = "~";
        public static int G_SEMINIMA = 1;
        public static int G_COLCHEIA = 2;
        public static int G_SEMICOLCHEIA = 4;
        public static int N0 = 0;
        public static int N1 = 1;
        public static int N2 = 2;
        public static int N3 = 3;
        public static int N4 = 4;
        public static int N5 = 5;
        public static int NIVEL_FRACO = 6;
        public static int[] DOIS_DOIS = {
        0, 4, 3, 4, 2, 4, 3, 4, 1, 4,
        3, 4, 2, 4, 3, 4
    };
        public static int[] QUATRO_QUATRO = {
        0, 4, 3, 4, 2, 4, 3, 4, 1, 4,
        3, 4, 2, 4, 3, 4
    };
        public static int[] TRES_QUATRO = {
        0, 3, 2, 3, 1, 3, 2, 3, 1, 3,
        2, 3
    };
        public static int[] DOIS_QUATRO = {
        0, 3, 2, 3, 1, 3, 2, 3
    };
        public static int[] TRES_OITO = {
        0, 2, 1, 2, 1, 2
    };
        public static int[] SEIS_OITO = {
        0, 3, 2, 3, 2, 3, 1, 3, 2, 3,
        2, 3
    };
        public static int NENHUM = -1;
        public static int AMBOS = 0;
        public static int TEMPO = 1;
        public static int NIVEIS = 2;
        public static string FICHEIROS_DIR = "ficheiros/";
        public static int TEMPO_FORTE = 0;
        public static int PAUSA = 1;
        public static int TEMPO_FORTE_PAUSA = 2;
        public static int FIM = 3;
        public static int TEMPO_FORTE_FIM = 4;
        public static int TEMPO_FORTE_OUTRO_PAUSA = 5;
        public static int ULTIMO_FORTE_PARTE = 6;
        public static int NOTA_CURTA = 8;
        public static char[] ACENTOS = { 'à', 'á', 'é', 'í', 'ó', 'ú', 'â', 'ê', 'î', 'ô' };
        public static char[] VOGAIS_ACENTOS = { 'a', 'e', 'i', 'o', 'u', 'à', 'á', 'é', 'í', 'ó', 'ú', 'â', 'ê', 'î', 'ô' };
        public static char[] SEMI_VOGAIS_ACENTOS = { 'i', 'u', 'à', 'á', 'é', 'í', 'ó', 'ú', 'â', 'ê', 'î', 'ô' };

        public static void inicializar()
        {


            //VOGAIS_ACENTOS = new char[VOGAIS.length + ACENTOS.length];
            //SEMI_VOGAIS_ACENTOS = new char[SEMI_VOGAIS.length + ACENTOS.length];
        }

    }
}
