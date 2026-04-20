using Biblioteca.ADONet;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;

namespace BibliotecaAPI.Repositories
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        public List<EmprestimoDTO> GetAll()
        {
            string sql = @"
                        SELECT 
                            e.EmprestimoID,
                            u.Nome AS NomeUsuario,
                            o.Titulo AS TituloObra,
                            e.DataEmprestimo,
                            e.DataDevolucao,
                            e.EntregaAtrasada,
                            DATEADD(DAY, 15, e.DataEmprestimo) AS DataDevolucaoPrevista
                        FROM Emprestimos e
                        INNER JOIN Usuario u ON u.UsuarioID = e.UsuarioID
                        INNER JOIN Exemplares ex ON ex.ExemplaresID = e.ExemplarID
                        INNER JOIN Obras o ON o.ObraID = ex.ObraID";

            return DALPro.Query<EmprestimoDTO>(sql);
        }


        public Emprestimos GetById(int id)
        {
            string sql = "SELECT * FROM Emprestimos WHERE EmprestimoID = @id";

            var result = DALPro.Query<Emprestimos>(sql,
                new Dictionary<string, object>
                {
                    { "@id", id }
                });

            return result.FirstOrDefault();
        }

        public void NewEmprestimo(Emprestimos emprestimo)
        {
            string sql = @"INSERT INTO Emprestimos (UsuarioID, ExemplarID, DataEmprestimo, DataDevolucao, EntregaAtrasada)
                           VALUES (@UsuarioID, @ExemplarID, @DataEmprestimo, @DataDevolucao, @EntregaAtrasada)";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@UsuarioID", emprestimo.UsuarioID },
                { "@ExemplarID", emprestimo.ExemplarID },
                { "@DataEmprestimo", emprestimo.DataEmprestimo },
                { "@DataDevolucao", (object?)emprestimo.DataDevolucao ?? DBNull.Value },
                { "@EntregaAtrasada", (object?)emprestimo.EntregaAtrasada ?? DBNull.Value }
            });
        }

        public void UpdateEmprestimo(Emprestimos emprestimo)
        {
            string sql = @"UPDATE Emprestimos
                           SET UsuarioID = @UsuarioID,
                               ExemplarID = @ExemplarID,
                               DataEmprestimo = @DataEmprestimo,
                               DataDevolucao = @DataDevolucao,
                               EntregaAtrasada = @EntregaAtrasada
                           WHERE EmprestimoID = @EmprestimoID";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@EmprestimoID", emprestimo.EmprestimoID },
                { "@UsuarioID", emprestimo.UsuarioID },
                { "@ExemplarID", emprestimo.ExemplarID },
                { "@DataEmprestimo", emprestimo.DataEmprestimo },
                { "@DataDevolucao", (object?)emprestimo.DataDevolucao ?? DBNull.Value },
                { "@EntregaAtrasada", (object?)emprestimo.EntregaAtrasada ?? DBNull.Value }
            });
        }

        public void DeleteEmprestimo(int id)
        {
            string sql = "DELETE FROM Emprestimos WHERE EmprestimoID = @id";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@id", id }
            });
        }

        public void ReturnBook(int id)
        {
            string sql = @"UPDATE Emprestimos
                           SET DataDevolucao = GETDATE()
                           WHERE EmprestimoID = @id";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@id", id }
            });
        }


        public void RequisitarEmprestimo(int usuarioId, int exemplarId)
        {
            string sql = "EXEC sp_requisicaoEmprestimo @UsuarioID, @ExemplarID";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@UsuarioID", usuarioId },
                { "@ExemplarID", exemplarId }
            });
        }
    }
}
