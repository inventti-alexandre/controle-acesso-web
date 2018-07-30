using ControleAcesso.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

namespace ControleAcesso.DAO
{
    public class TipoPresencaDAO
    {
        private IConfiguration _configuration;

        public TipoPresencaDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<TipoPresenca> Listar()
        {
            using (SqlConnection conexao = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection")))
            {
                return conexao.Query<TipoPresenca>(
                    "SELECT * " +
                    "FROM dbo.TipoPresenca");
            }
        }
    }
}
