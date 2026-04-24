using BibliotecaAPI.Models;

namespace BibliotecaAPI.Services
{
    public interface INucleoService
    {
        List<Nucleo> GetAll();
        Nucleo GetById(int id);
        void Add(Nucleo nucleo);
        void Update(Nucleo nucleo);
        void Delete(int id);
        bool Exists(int id);
    }
}
