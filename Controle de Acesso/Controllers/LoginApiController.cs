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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Controle_de_Acesso.Controllers
{
    [Route("api/[controller]")]
    public class LoginApiController : Controller
    {
        [AllowAnonymous]
        [HttpPost]
        public object Post(
             [FromBody]Usuario usuario,
             [FromServices]UsersDAO usersDAO,
             [FromServices]SigningConfigurations signingConfigurations,
             [FromServices]TokenConfigurations tokenConfigurations)
        {

            bool credenciaisValidas = false;
            Usuario usuarioBase = new Usuario();

            if (usuario != null)
            {
                usuarioBase = usersDAO.BucarPorUsernameESenha(usuario.Username, usuario.Senha);
                credenciaisValidas = (usuarioBase != null);
            }

            if (credenciaisValidas)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(usuarioBase.Username, "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, usuarioBase.Username)
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });

                var token = handler.WriteToken(securityToken);

                return new
                {
                    authenticated = true,
                    accessToken = token,
                    message = "OK"
                };
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
            }
        }
    }
}