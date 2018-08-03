using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ControleAcesso.DAO;
using ControleAcesso.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleAcesso.Controllers
{

    public class HomeController : Controller
    {
        [Authorize(AuthenticationSchemes =
       CookieAuthenticationDefaults.AuthenticationScheme + ",admin")]
        public IActionResult Index([FromServices]EventosDAO eventosDAO)
        {
            var eventoSelecionado = Cookie.LerCookie("EventoSelecionado", Request);

            if (!string.IsNullOrEmpty(eventoSelecionado))
                if (eventoSelecionado != "0")
                    return RedirectToAction("Index", "Pessoas");

            var eventos = eventosDAO.Listar();
            return View(eventos);
        }

        public IActionResult NaoAutorizado()
        {
            return View();
        }

    }
}