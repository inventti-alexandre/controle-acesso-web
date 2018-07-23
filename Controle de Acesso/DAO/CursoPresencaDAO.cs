using ControleAcesso.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

namespace ControleAcesso.DAO
{
    public class CursoPresencaDAO
    {
        private IConfiguration _configuration;

        public CursoPresencaDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<CursoPresenca> Listar(int id)
        {
            using (SqlConnection conexao = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection")))
            {
                return conexao.Query<CursoPresenca>(
                    "SELECT * " +
                    "FROM dbo.CursoPresencas " +
                    "Where CursoID = @CursoID", new { CursoID = id });
            }
        }
    }
}
