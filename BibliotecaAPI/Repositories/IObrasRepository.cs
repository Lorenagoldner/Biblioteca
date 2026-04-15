using BibliotecaAPI.Models;

namespace BibliotecaAPI.Repositories
{
    public interface IObrasRepository
    {
        List<Obras> GetAll();
        List<Obras> FindAll(); // Método para buscar obras por título, autor ou gênero
        void Add(Obras obra);
        void Update(Obras obra);
        void Delete(int id);
        
    }
}
