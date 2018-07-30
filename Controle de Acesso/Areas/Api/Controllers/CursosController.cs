using System;
using System.Collections.Generic;
using System.Linq;
using ControleAcesso.Areas.Api.ViewModels;
using ControleAcesso.DAO;
using ControleAcesso.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Acesso.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [Area("API")]
    [Authorize("Bearer")]
    public class CursosController : Controller
    {
        [HttpPost]
        public IEnumerable<Curso> Get(int id, [FromBody] ValidarViewModel model, [FromServices] CursosDAO cursosDAO)
        {
            var cursos = cursosDAO.ListarPorEvento(id);
            return cursos;
        }
    }
}