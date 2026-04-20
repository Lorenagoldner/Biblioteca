using BibliotecaAPI.Models;

namespace BibliotecaAPI.Repositories
{
    public interface IGeneroRepository
    {
        List<Genero> GetAllGeneros();
        Genero GetGeneroById(int id);
        void AddGenero(Genero genero);
        void UpdateGenero(Genero genero);
        void DeleteGenero(int id);
    }
}