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
    public class EventosController : Controller
    {
        [HttpPost]
        public IEnumerable<Evento> Get(int id, [FromBody] ValidarViewModel model, [FromServices] EventosDAO eventosDAO)
        {
            return eventosDAO.Listar();
        }
    }
}