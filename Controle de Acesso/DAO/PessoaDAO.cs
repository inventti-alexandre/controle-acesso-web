using ControleAcesso.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

namespace ControleAcesso.DAO
{
    public class PessoaDAO
    {
        private IConfiguration _configuration;

        public PessoaDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Pessoa> Listar()
        {
            using (SqlConnection conexao = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection")))
            {
                return conexao.Query<Pessoa>(
                    "SELECT * " +
                    "FROM dbo.Pessoas");
            }
        }

        public IEnumerable<Pessoa> ListarPorEvento(int id)
        {
            using (SqlConnection conexao = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection")))
            {
                return conexao.Query<Pessoa>(
                    "SELECT * " +
                        "FROM   dbo.pessoas a " +
                        " WHERE  EXISTS(SELECT 1 " +
                                      "FROM   dbo.cursopresencas b " +
                         "                    INNER JOIN dbo.cursos c " +
                         "                           ON c.cursoid = b.cursoid " +
                         "           WHERE  c.eventoid = @EventoID and b.PessoaID = a.PessoaID)  ", new { EventoID = id });
            }
        }

        public Pessoa BuscarPorID(int id)
        {
            using (SqlConnection conexao = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection")))
            {
                return conexao.QueryFirstOrDefault<Pessoa>(
                    "SELECT * " +
                    "FROM dbo.Pessoas " +
                    "Where PessoaID = @PessoaID", new { PessoaID = id });
            }
        }
    }
}
