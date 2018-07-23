using ControleAcesso.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

namespace ControleAcesso.DAO
{
    public class EventosDAO
    {
        private IConfiguration _configuration;

        public EventosDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Evento> Listar()
        {
            using (SqlConnection conexao = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection")))
            {
                return conexao.Query<Evento>(
                    "SELECT * " +
                    "FROM dbo.Eventos");
            }
        }
    }
}
