using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;

namespace BibliotecaAPI.Services
{
    public interface IEmprestimoService
    {
        List<EmprestimoDTO> GetAll();
        EmprestimoDTO GetById(int id);
        void NewEmprestimo(int usuarioId, int exemplarId);
        void UpdateEmprestimo(int id, EmprestimoUpdateDTO dto);
        void DeleteEmprestimo(int id);
        void ReturnBook(int id);
    }
}
