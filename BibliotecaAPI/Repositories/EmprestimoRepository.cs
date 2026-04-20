using BibliotecaAPI.Models;
using Microsoft.Data.SqlClient;
using Biblioteca.ADONet;
using System.Data;

namespace BibliotecaAPI.Repositories
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        public List<Emprestimos> GetAll()
        {
            string sql = "Select * from Emprestimos";
            return DALPro.Query<Emprestimos>(sql);
        }
        public Emprestimos GetById(int id)
        {
            string sql = "SELECT * FROM Emprestimos WHERE EmprestimoID = @id";
            var parametros = new Dictionary<string, object> { { "@id", id } };
            return DALPro.Query<Emprestimos>(sql, parametros).FirstOrDefault(); //usamos FirstOrDefault para retornar o primeiro resultado ou null se não encontrar nenhum registro com o id fornecido
        }
        public void NewEmprestimo(Emprestimos emprestimo)
        {
            var parametros = new Dictionary<string, object> //dicionario para os parametros do sp, onde a chave é o nome do parametro e o valor é o valor do parametro
            {
                { "@usuarioid", emprestimo.UsuarioID },
                { "@ExemplarID", emprestimo.ExemplarID }
            };

            DataTable dt = DALPro.ExecuteSP("sp_requisicaoEmprestimo", parametros);
        }
        public void UpdateEmprestimo(Emprestimos emprestimo)
        {
            string sql = @"UPDATE Emprestimos 
                   SET UsuarioID = @uid, 
                       ExemplarID = @eid, 
                       DataEmprestimo = @data 
                   WHERE EmprestimoID = @id";

            var parametros = new Dictionary<string, object>
            {
                { "@uid", emprestimo.UsuarioID },
                { "@eid", emprestimo.ExemplarID },
                { "@data", emprestimo.DataEmprestimo },
                { "@id", emprestimo.EmprestimoID }
            };

            DALPro.Execute(sql, parametros);
        }
        public void DeleteEmprestimo(int id)
        {
            string sqlLiberar = @"UPDATE Exemplares SET Disponivel = 1 
                          WHERE ExemplaresID = (SELECT ExemplarID FROM Emprestimos WHERE EmprestimoID = @id)";

            string sqlDelete = "DELETE FROM Emprestimos WHERE EmprestimoID = @id";

            var parametros = new Dictionary<string, object> { { "@id", id } };

            DALPro.Execute(sqlLiberar, parametros);
            DALPro.Execute(sqlDelete, parametros);
        }
        public void ReturnBook(int id)
        {
            string sql = @"
            UPDATE Emprestimos SET DataDevolucao = GETDATE() WHERE EmprestimoID = @id;
        
            UPDATE Exemplares SET Disponivel = 1 
            WHERE ExemplaresID = (SELECT ExemplarID FROM Emprestimos WHERE EmprestimoID = @id);";

            var parametros = new Dictionary<string, object> { { "@id", id } };
            DALPro.Execute(sql, parametros);
            DALPro.ExecuteSP("sp_GerarHistorico");
        }
    }
}
