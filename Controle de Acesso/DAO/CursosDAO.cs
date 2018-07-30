using ControleAcesso.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

namespace ControleAcesso.DAO
{
    public class CursosDAO
    {
        private IConfiguration _configuration;

        public CursosDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Curso> Listar()
        {
            using (SqlConnection conexao = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection")))
            {
                return conexao.Query<Curso>(
                    "SELECT * " +
                    "FROM dbo.Cursos");
            }
        }

        public IEnumerable<Curso> ListarPorEvento(int id)
        {
            using (SqlConnection conexao = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection")))
            {
                return conexao.Query<Curso>(
                    "SELECT * " +
                    "FROM dbo.Cursos " +
                    "Where EventoId = @EventoID", new { EventoID = id });
            }
        }
    }
}
