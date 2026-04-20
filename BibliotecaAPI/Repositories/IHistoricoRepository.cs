using BibliotecaAPI.Models;

namespace BibliotecaAPI.Repositories
{
    public interface IHistoricoRepository
    {
       List<Historico> GetAll();
       Historico GetById(int id);
       void NewHistorico(Historico historico);
       void DeleteHistorico(Historico historico);
       void UpdateHistorico(Historico historico);
       void GerarHistoricoAutomatico(); //Metodo para chamar a SP sp_GerarHistorico
    }
}
