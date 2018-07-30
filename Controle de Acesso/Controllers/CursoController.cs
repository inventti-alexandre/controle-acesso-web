
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ControleAcesso.DAO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleAcesso.Controllers
{

    public class CursoController : Controller
    {
        [Authorize(AuthenticationSchemes =
       CookieAuthenticationDefaults.AuthenticationScheme)]
        public IActionResult Index(int id, [FromServices]CursoPresencaDAO cursoPresencaDAO)
        {
            var presentes = cursoPresencaDAO.Listar(id);
            return View(presentes);
        }

        // POST api/values
        [HttpPost]
        public bool Imprimir([FromForm]int id, [FromServices]CursoPresencaDAO cursoPresencaDAO)
        {
            try
            {
                var cliente = cursoPresencaDAO.BuscarPorID(id);

                if (cliente == null)
                    throw new Exception("Not Found");

                // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("C:\\Etiquetas\\etiqueta-terminal-" + "01" + ".prn"))
                {
                    // Read the stream to a string, and write the string to the console.
                    string line = sr.ReadToEnd();

                    line = line.Replace("<?CLIENTE?>", cliente.Pessoa.Nome);
                    line = line.Replace("<?FARMACIA?>", cliente.Pessoa.Empresa);
                    line = line.Replace("<?DIAS?>", "18 e 19");
                    line = line.Replace("<?CODIGO?>", cliente.Pessoa.CPF);

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
                //return "Erri";
                return false;
            }
            //System.IO.File.Copy("C:\\Users\\Web\\Desktop\\tmp.prn", "\\\\localhost\\argox01", true);
        }
    }
}