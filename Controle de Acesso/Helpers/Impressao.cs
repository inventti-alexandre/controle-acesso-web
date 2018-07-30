using ControleAcesso.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ControleAcesso.Helpers
{
    public static class Impressao
    {
        public static bool Imprimir(Pessoa pessoa)
        {
            try
            {
                if (pessoa == null)
                    throw new Exception("Not Found");

                // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("C:\\Etiquetas\\etiqueta-terminal-" + "01" + ".prn"))
                {
                    // Read the stream to a string, and write the string to the console.
                    string line = sr.ReadToEnd();

                    line = line.Replace("<?CLIENTE?>", pessoa.Nome);
                    line = line.Replace("<?FARMACIA?>", pessoa.Empresa);
                    line = line.Replace("<?DIAS?>", "18 e 19");
                    line = line.Replace("<?CODIGO?>", pessoa.CPF);

                    System.IO.File.WriteAllText("C:\\Temp\\tmp" + "01" + ".prn", line);
                    System.IO.File.Copy("C:\\Temp\\tmp" + "01" + ".prn", "\\\\localhost\\argox01", true);
                    System.IO.File.Delete("C:\\Temp\\tmp" + "01" + ".prn");

                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return false;
            }
            //System.IO.File.Copy("C:\\Users\\Web\\Desktop\\tmp.prn", "\\\\localhost\\argox01", true);
        }
    }
}
