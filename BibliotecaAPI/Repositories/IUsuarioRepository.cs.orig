using BibliotecaAPI.Models;

namespace BibliotecaAPI.Repositories
{
    public interface IUsuarioRepository
    {
        List<Usuario> GetAll();
        Usuario GetById(int id);
        void Add(Usuario usuario);
        void Delete(int id);
        void Update(int id, Usuario usuario);

        Usuario GetByEmail(string email); // Login
    }
}
