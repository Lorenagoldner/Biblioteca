using BibliotecaAPI.Models;

namespace BibliotecaAPI.Services
{
    public interface IGeneroService
    {
        List<Genero> GetAll();
        Genero GetById(int id);
        void Add(Genero genero);
        void Update(Genero genero);
        void Delete(int id);
    }
}
