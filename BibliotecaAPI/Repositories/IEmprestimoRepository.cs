using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;

namespace BibliotecaAPI.Repositories
{
    public interface IEmprestimoRepository
    {
        List<EmprestimoDTO> GetAll(); 
        Emprestimos GetById(int id); 
        void NewEmprestimo(Emprestimos emprestimo);
        void UpdateEmprestimo(Emprestimos emprestimo); 
        void DeleteEmprestimo(int id); 
        void ReturnBook(int id);
        void RequisitarEmprestimo(int usuarioId, int exemplarId);
    }
}
