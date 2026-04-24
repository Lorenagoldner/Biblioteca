using BibliotecaAPI.Models;

namespace BibliotecaAPI.Repositories
{
    public interface IEmprestimoRepository
    {
        List<Emprestimos> GetAll();
        Emprestimos GetById(int id);
        void NewEmprestimo(Emprestimos emprestimo);
        void UpdateEmprestimo(Emprestimos emprestimo);
        void DeleteEmprestimo(int id);
        void ReturnBook(int id);

    }
}