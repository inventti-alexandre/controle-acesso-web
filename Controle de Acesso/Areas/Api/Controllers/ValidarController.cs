using System;
using System.Collections.Generic;
using System.Linq;
using ControleAcesso.Areas.Api.ViewModels;
using ControleAcesso.DAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Acesso.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [Area("API")]
    [Authorize("Bearer")]
    public class ValidarController : Controller
    {
        [HttpPost]
        public bool Post([FromBody] ValidarViewModel model, [FromServices] CursoPresencaDAO cursoPresencaDAO)
        {
            var cursoPresenca = cursoPresencaDAO.BuscarPorAlunoECurso(model.CursoID, model.CPF);
            return cursoPresenca != null;
        }
    }
}