using BibliotecaAPI.Models;

namespace BibliotecaAPI.Repositories
{
    public interface ITiposDeUsuarioRepository
    {
        List<TiposDeUsuario> GetAll();
        void Add(TiposDeUsuario tipo);
        void Delete(int id);
        void Update(int id, TiposDeUsuario tipo);
    }
}
