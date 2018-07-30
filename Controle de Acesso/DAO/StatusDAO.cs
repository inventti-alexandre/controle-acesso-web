using ControleAcesso.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

namespace ControleAcesso.DAO
{
    public class StatusDAO
    {
        private IConfiguration _configuration;

        public StatusDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Status> Listar()
        {
            using (SqlConnection conexao = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection")))
            {
                return conexao.Query<Status>(
                    "SELECT * " +
                    "FROM dbo.Status order by Nome ");
            }
        }
    }
}
