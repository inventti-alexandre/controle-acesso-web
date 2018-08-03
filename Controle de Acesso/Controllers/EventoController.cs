using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ControleAcesso.DAO;
using ControleAcesso.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleAcesso.Controllers
{

    public class EventoController : Controller
    {
        [Authorize(AuthenticationSchemes =
       CookieAuthenticationDefaults.AuthenticationScheme + ",admin")]
        public IActionResult Index(int id, [FromServices]CursosDAO cursosDAO)
        {
            if (id != 0)
                Cookie.SalvarCookie("EventoSelecionado", id.ToString(), Request);

            //var cursos = cursosDAO.ListarPorEvento(id);
            return RedirectToAction("Index","Pessoas");
        }

    }
}