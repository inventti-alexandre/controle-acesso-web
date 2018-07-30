using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleAcesso.Helpers
{
    public static class Cookie
    { 
        public static bool SalvarCookie(string key, string value, HttpRequest request)
        {
            request.HttpContext.Response.Cookies.Append(
                key,
                value,
                new CookieOptions()
                {
                    Path = "/"
                }); 
            return true;
        }


        public static string LerCookie(string key, HttpRequest request)
        {    
            return request.Cookies[key];
        }

        public static void DeletarCookie (string key, HttpRequest request)
        {
            request.HttpContext.Response.Cookies.Delete("EventoSelecionado");
        }
    }
}
