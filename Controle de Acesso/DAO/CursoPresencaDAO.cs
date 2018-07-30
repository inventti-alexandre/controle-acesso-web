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

        public CursoPresenca BuscarPorID(int id)
        {
            using (SqlConnection conexao = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection")))
            {
                return conexao.QueryFirstOrDefault<CursoPresenca>(
                    "SELECT * " +
                    "FROM dbo.CursoPresencas " +
                    "Where CursoPresencaID = @CursoPresencasID", new { CursoPresencasID = id });
            }
        }


        public CursoPresenca BuscarPorAlunoECurso(int CursoID, string CPF)
        {
            using (SqlConnection conexao = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection")))
            {
                return conexao.QueryFirstOrDefault<CursoPresenca>(
                    "SELECT * " +
                    "FROM   dbo.cursopresencas a " +
                    "      INNER JOIN dbo.pessoas b " +
                    "               ON b.pessoaid = a.pessoaid " +
                    " WHERE  a.cursoid = @CursoID " +
                    " AND b.cpf = @CPF", new { CursoID, CPF });
            }
        }
    }
}
