using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ControleAcesso.Controllers
{


    public class Cliente
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Farmacia { get; set; }
        public string Dias { get; set; }
    }

    public class TesteImpressaoViewModel
    {
        public string Id;
        public string CodigoTerminal;
    }

    [Route("api/[controller]")]
    public class TesteImpressaoController : Controller
    {

        List<Cliente> clientes;

        public TesteImpressaoController()
        {
            clientes = new List<Cliente>();

            clientes.Add(new Cliente()
            {
                Id = "CLI0001",
                Nome = "Marcus Toloto",
                Farmacia = "Consulfarma",
                Dias = "18 e 19"
            });

            clientes.Add(new Cliente()
            {
                Id = "CLI0002",
                Nome = "Daniel Gregatto",
                Farmacia = "Pharmacorum",
                Dias = "18"
            });


            clientes.Add(new Cliente()
            {
                Id = "CLI0003",
                Nome = "Matheus Lima",
                Farmacia = "TI Teste",
                Dias = "19"
            });

            clientes.Add(new Cliente()
            {
                Id = "CLI0004",
                Nome = "Alexandre Lima",
                Farmacia = "Iberoquimica",
                Dias = "18 e 19"
            });
        }



        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]TesteImpressaoViewModel model)
        {
            try
            { 
                var cliente = clientes.FirstOrDefault(x => x.Id == model.Id);
                if (cliente == null)
                    throw new Exception("Not Found");

                // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("C:\\Etiquetas\\etiqueta-terminal-" + model.CodigoTerminal + ".prn"))
                {
                    // Read the stream to a string, and write the string to the console.
                    string line = sr.ReadToEnd();

                    line = line.Replace("<?CLIENTE?>", cliente.Nome);
                    line = line.Replace("<?FARMACIA?>", cliente.Farmacia);
                    line = line.Replace("<?DIAS?>", cliente.Dias);
                    line = line.Replace("<?CODIGO?>", cliente.Id); 

                    System.IO.File.WriteAllText("C:\\Temp\\tmp" + model.CodigoTerminal + ".prn", line);
                    System.IO.File.Copy("C:\\Temp\\tmp" + model.CodigoTerminal + ".prn", "\\\\localhost\\argox01", true);
                    System.IO.File.Delete("C:\\Temp\\tmp" + model.CodigoTerminal + ".prn");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return;
                //return "Erri";
            } 
            //System.IO.File.Copy("C:\\Users\\Web\\Desktop\\tmp.prn", "\\\\localhost\\argox01", true);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
