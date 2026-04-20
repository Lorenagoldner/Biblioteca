using BibliotecaAPI.Models;

namespace BibliotecaAPI.Repositories
{
    public interface IStatusUsuarioRepository
    {
        List<StatusUsuario> GetAll();
        StatusUsuario GetById(int id);
        //metodos criados para criar um novo status no futuro, caso seja necessario. Mas como essa tabela é mais estática, usaremos raramente. Talvez não seja necessário criar no controller
        void Add(StatusUsuario statusUsuario);
        void Update(StatusUsuario statusUsuario);
        void Delete(int id);
    }
}
