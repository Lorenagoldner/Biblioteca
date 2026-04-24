using BibliotecaAPI.Models;

namespace BibliotecaAPI.Repositories
{
    public interface IHistoricoRepository
    {
       List<Historico> GetAll(int? nucleoId = null, DateTime? inicio = null, DateTime? fim = null);
       Historico GetById(int id);
       void GerarHistoricoAutomatico(); //Metodo para chamar a SP sp_GerarHistorico
    }
}
