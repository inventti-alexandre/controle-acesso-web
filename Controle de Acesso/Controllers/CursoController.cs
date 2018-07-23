
using System;
using System.Collections.Generic;
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
         
    }
}