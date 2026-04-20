using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;

namespace BibliotecaAPI.Repositories
{
    public interface IObrasRepository
    {
        List<Obras> GetAll();
        Obras GetById(int id);
        int Add(Obras obra);
        void Update(Obras obra);
        void Delete(int id);
        List<PesquisaObraDTO> PesquisarObra(string texto);
    }
}
