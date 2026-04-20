using Biblioteca.ADONet;
using BibliotecaAPI.Models;

namespace BibliotecaAPI.Repositories
{
    public class HistoricoRepository : IHistoricoRepository
    {
        public List<Historico> GetAll(int? nucleoId = null, DateTime? inicio = null, DateTime? fim = null) //puxar o historico do banco de dados, podendo filtrar por nucleo e por periodo de tempo (entre a data de requisicao e a data de devolucao)
        {
            string sql = "SELECT * FROM Historico WHERE 1=1";
            var parametros = new Dictionary<string, object>();

            if (nucleoId.HasValue) //se o nucleoId for fornecido, adiciona a condição de filtro para o nucleoId
            {
                sql += " AND NucleoID = @nid"; 
                parametros.Add("@nid", nucleoId.Value);
            }
            if (inicio.HasValue && fim.HasValue) //se as datas de inicio e fim forem fornecidas, adiciona a condição de filtro para o periodo de tempo entre a data de requisicao e a data de devolucao
            {
                sql += " AND DataRequisicao BETWEEN @inicio AND @fim";
                parametros.Add("@inicio", inicio.Value);
                parametros.Add("@fim", fim.Value);
            }
            return DALPro.Query<Historico>(sql, parametros);
        }
        public Historico GetById(int id) //puxar um historico específico do banco de dados usando o id do historico ou do aluno
        {
            string sql = "SELECT * FROM Historico WHERE HistoricoID = @id";
            var p = new Dictionary<string, object> { { "@id", id } };
            return DALPro.Query<Historico>(sql, p).FirstOrDefault();
        }
      
        public void GerarHistoricoAutomatico() //pra quando quiser gerar o historico automaticamente, chamando a SP sp_GerarHistorico, que vai gerar o historico de todos os alunos com base nos emprestimos e devoluções registrados no banco de dados
        {
            DALPro.ExecuteSP("sp_GerarHistorico");
        }
    }
}
