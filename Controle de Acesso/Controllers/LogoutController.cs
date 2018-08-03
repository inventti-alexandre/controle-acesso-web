using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using ControleAcesso.Configurations;
using ControleAcesso.DAO;
using ControleAcesso.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Controle_de_Acesso.Controllers
{
    public class LogoutController : Controller
    { 
        [HttpPost]
        public async Task<ActionResult> Post()
        {
            await HttpContext.SignOutAsync(
    CookieAuthenticationDefaults.AuthenticationScheme); 
            await HttpContext.SignOutAsync("admin"); 
            return RedirectToAction("Index", "Login");
        }
    }
}