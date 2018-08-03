
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ControleAcesso.DAO;
using ControleAcesso.Data;
using ControleAcesso.Helpers;
using ControleAcesso.Models;
using ControleAcesso.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ControleAcesso.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme + ",admin")]
    public class PessoasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PessoasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index([FromServices]PessoaDAO pessoaDAO,
             [FromServices]EventosDAO eventoDAO)
        {
            IEnumerable<Pessoa> pessoas;
            var id = int.Parse(Cookie.LerCookie("EventoSelecionado", Request) ?? "0");

            if (id != 0)
            {
                pessoas = pessoaDAO.ListarPorEvento(id);
            }
            else
            {
                Cookie.DeletarCookie("EventoSelecionado", Request);
                return RedirectToAction("Index", "Home");
            }

            ViewData["Eventos"] = eventoDAO.Listar();
            ViewData["EventoSelecionado"] = id;
            return View(pessoas);
        }

        public IActionResult SelecionarEvento(int id)
        {
            Cookie.SalvarCookie("EventoSelecionado", id.ToString(), Request);
            return RedirectToAction("Index", "Pessoas");
        }

        public IActionResult Novo([FromServices]StatusDAO statusDAO,
                                  [FromServices]CursosDAO cursoDAO)
        {
            var id = int.Parse(Cookie.LerCookie("EventoSelecionado", Request));
            ViewData["Cursos"] = cursoDAO.ListarPorEvento(id);
            ViewData["Status"] = statusDAO.Listar();
            return View();
        }

        public async Task<IActionResult> Gerenciar(int? id, [FromServices]StatusDAO statusDAO,
                                  [FromServices]CursosDAO cursoDAO)
        {

            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas.SingleOrDefaultAsync(m => m.PessoaID == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            var idEvento = int.Parse(Cookie.LerCookie("EventoSelecionado", Request));
            ViewData["Cursos"] = cursoDAO.ListarPorEvento(idEvento);
            ViewData["Status"] = statusDAO.Listar();
            ViewData["CursosVinculados"] = _context.CursoPresencas.Where(x => x.PessoaID == pessoa.PessoaID);

            var novaPessoaViewModel = new NovaPessoaViewModel()
            {
                Pessoa = pessoa,
                ImprimirEtiqueta = true
            };

            return View(novaPessoaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Novo([FromForm]IFormCollection form, [FromForm]NovaPessoaViewModel model, [FromServices]StatusDAO statusDAO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //model.Pessoa = Pessoas.RemoverPontosETracos(model.Pessoa);
                    _context.Add(model.Pessoa);

                    var cursos = form["Cursos"].ToString().Split(',');

                    foreach (var cursoID in cursos)
                    {
                        if (string.IsNullOrEmpty(cursoID))
                            continue;

                        var intId = int.Parse(cursoID);

                        var cursoPresenca = new CursoPresenca()
                        {
                            CursoID = intId,
                            PessoaID = model.Pessoa.PessoaID,
                            TipoPresencaID = 1, //Controlar no futuro
                            StatusID = 1 //Controlar no futuro
                        };

                        _context.Add(cursoPresenca);
                    }

                    await _context.SaveChangesAsync();


                    if (model.ImprimirEtiqueta)
                        Impressao.Imprimir(model.Pessoa);

                    return RedirectToAction(nameof(Index));
                }
                // ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusID", pessoa.StatusID);
                ViewData["Status"] = statusDAO.Listar();
                return View(model.Pessoa);
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Gerenciar([FromForm]IFormCollection form, [FromForm]NovaPessoaViewModel model, [FromServices]StatusDAO statusDAO)
        {
            if (ModelState.IsValid)
            {
                model.Pessoa = Pessoas.RemoverPontosETracos(model.Pessoa);

                _context.Update(model.Pessoa);
                await _context.SaveChangesAsync();

                var cursos = form["Cursos"].ToString().Split(',');
                var cursosPresenca = _context.CursoPresencas.Where(x => x.PessoaID == model.Pessoa.PessoaID);

                _context.CursoPresencas.RemoveRange(cursosPresenca);

                foreach (var cursoID in cursos)
                {
                    if (string.IsNullOrEmpty(cursoID))
                        continue;

                    var intId = int.Parse(cursoID);

                    var cursoPresenca = new CursoPresenca()
                    {
                        CursoID = intId,
                        PessoaID = model.Pessoa.PessoaID,
                        TipoPresencaID = 1, //Controlar no futuro
                        StatusID = 1 //Controlar no futuro
                    };

                    _context.Add(cursoPresenca);
                }

                await _context.SaveChangesAsync();

                if (model.ImprimirEtiqueta)
                    Impressao.Imprimir(model.Pessoa);

                return RedirectToAction(nameof(Index));
            }
            // ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusID", pessoa.StatusID);
            ViewData["Status"] = statusDAO.Listar();
            return View(model.Pessoa);
        }

        // POST api/values
        [HttpPost]
        public bool Imprimir([FromForm]int id, [FromServices]PessoaDAO pessoaDAO)
        {
            try
            {
                var pessoa = pessoaDAO.BuscarPorID(id);
                if (pessoa == null)
                    throw new Exception("Not Found");
                return Impressao.Imprimir(pessoa);
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return false;
            }
            //System.IO.File.Copy("C:\\Users\\Web\\Desktop\\tmp.prn", "\\\\localhost\\argox01", true);
        }


        [HttpPost]
        public IActionResult VerificarSeClienteExiste([FromForm] string CPF)
        {

            var pessoa = _context.Pessoas.FirstOrDefault(x => x.CPF == CPF);

            if (pessoa != null)
                return RedirectToAction("Gerenciar", "Pessoas", new { id = pessoa.PessoaID });
            else
                return RedirectToAction("Novo", "Pessoas", new { cpf = CPF });

        }
    }


}