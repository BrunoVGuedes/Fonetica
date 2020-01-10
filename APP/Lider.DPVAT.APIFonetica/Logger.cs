using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lider.DPVAT.APIFonetica
{
    public static class Logger
    {

        public static void AddLog(string str)
        {
            StreamWriter arquivoWS = null;


            String Path = System.AppDomain.CurrentDomain.BaseDirectory + @"Logs\";
            String Time = "";           

            try
            {
                //verifica se existe o diretorio
                if (System.IO.Directory.Exists(Path) == false)
                {
                    System.IO.Directory.CreateDirectory(Path);
                }

                //verifica se existe o arquivo com a data de hoje
                if (arquivoWS == null)
                {
                    arquivoWS = new StreamWriter(Path + "Fonetica_" + DateTime.Now.Date.ToString("yyyy_MM_dd") + ".log", true);
                }


                Time = "LOCAL TIME[" + DateTime.Now + "]";


                arquivoWS.WriteLine(Time + " - " + str); 
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
            finally
            {
                arquivoWS.Flush();
                arquivoWS.Dispose();
                arquivoWS = null;
            }
        }

    }
}