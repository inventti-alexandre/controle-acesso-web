using ControleAcesso.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace ControleAcesso.DAO
{
    public class UsersDAO
    {
        private IConfiguration _configuration;

        public UsersDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Usuario BucarPorUsernameESenha(string Username, string Senha)
        {
            using (SqlConnection conexao = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection")))
            {
                return conexao.QueryFirstOrDefault<Usuario>(
                    "SELECT a.Username, a.Senha, a.Nome, b.TipoUsuarioID " +
                    "FROM dbo.Usuarios a" +
                    "inner join dbo.TipoUsuario b on b.TipoUsuarioID = a.TipoUsuarioID " +
                    "WHERE Username = @Username and Senha = @Senha", new { Username, Senha });
            }
        }
    }
}
