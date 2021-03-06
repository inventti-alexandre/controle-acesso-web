﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using ControleAcesso.Configurations;
using ControleAcesso.DAO;
using ControleAcesso.Data;
using ControleAcesso.Models;
using ControleAcesso.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Controle_de_Acesso.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Post(
             [FromForm]LoginViewModel model,
             [FromServices]UsersDAO usersDAO)
        {

            try
            {
                bool credenciaisValidas = false;
                Usuario usuarioBase = new Usuario();

                if (model != null)
                {
                    usuarioBase = _context.Usuarios.Include(x => x.TipoUsuario).FirstOrDefault(x => x.Username == model.Username && x.Senha == model.Senha);
                    credenciaisValidas = (usuarioBase != null);
                }

                if (credenciaisValidas)
                {

                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuarioBase.Username),
                    new Claim("FullName", usuarioBase.Nome),
                    new Claim(ClaimTypes.Role, "Administrator"),
                };

                    var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    string scheme = "";
                    if (usuarioBase.TipoUsuario.Administrador)
                        scheme = "admin";
                    else
                        scheme = CookieAuthenticationDefaults.AuthenticationScheme;

                    await HttpContext.SignInAsync(
                     scheme,
                     new ClaimsPrincipal(claimsIdentity),
                     authProperties);


                    if (usuarioBase.TipoUsuario.Administrador)
                        return RedirectToAction("Index", "Home", new { Area = "Admin" });
                    else
                        return RedirectToAction("Index", "Home");


                }
                else
                {
                    return RedirectToAction("Index", "Login", new { err = "Sim" });
                }
            }
            catch (Exception ex)
            {
                var a = ex;
                return RedirectToAction("Index", "Login", new { err = "Sim" });
            }
        }
    }
}