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
                    "SELECT * " +
                    "FROM dbo.Usuarios " +
                    "WHERE Username = @Username and Senha = @Senha", new { Username, Senha });
            }
        }
    }
}
