using Biblioteca.ADONet;
using BibliotecaAPI.Models;

namespace BibliotecaAPI.Repositories
{
    public class HistoricoRepository : IHistoricoRepository
    {
        public List<Historico> GetAll()
        {
            string sql = "SELECT * FROM Historico";
            return DALPro.Query<Historico>(sql);
        }

        public Historico GetById(int id)
        {
            string sql = "SELECT * FROM Historico WHERE HistoricoID = @id";

            var result = DALPro.Query<Historico>(sql,
                new Dictionary<string, object>
                {
                    { "@id", id }
                });
                return result.FirstOrDefault();
        }

        public void NewHistorico(Historico historico)
        {
            string sql = @"INSERT INTO Historico (Requisicao, Nucleo, DataRequisicao)
                           VALUES (@Requisicao, @Nucleo, @DataRequisicao)";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@Requisicao", historico.Requisicao ?? (object)DBNull.Value },
                { "@Nucleo", historico.Nucleo ?? (object)DBNull.Value },
                { "@DataRequisicao", historico.DataRequisicao}
            });
        }

    
        public void UpdateHistorico(Historico historico)
        {
            string sql = @"UPDATE Historico
                           SET  Requisicao = @Requisicao,
                                Nucleo = @Nucleo,
                                DataRequisicao  = @DataRequisicao
                           WHERE HistoricoID = @HistoricoID";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@HistoricoID", historico.HistoricoID },
                { "@Requisicao", historico.Requisicao ?? (object)DBNull.Value },
                { "@Nucleo", historico.Nucleo ?? (object)DBNull.Value },
                { "@DataRequisicao", historico.DataRequisicao}
            });
        }


        public void DeleteHistorico(Historico historico)
        {
            string sql = "DELETE FROM Historico WHERE HistoricoID = @id";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@id", historico.HistoricoID }
            });
        }

        public void GerarHistoricoAutomatico()
        {
            string sql = "EXEC sp_GerarHistorico";
            DALPro.Execute(sql);
        }
    }
}
