using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;

namespace BibliotecaAPI.Repositories
{
    public interface IExemplaresRepository
    {
        List<ExemplarJoinDTO> GetAll(); 
        Exemplares GetById(int id);
        int NewExemplar(Exemplares exemplar);
        void UpdateExemplar(Exemplares exemplar);
        void DeleteExemplar(int id);
        void AlterNucleo(int id, int idNucleo); //Método para alterar o núcleo de um exemplar específico, identificando-o pelo ID do exemplar e fornecendo o novo ID do núcleo.
        void DisponibilizarExemplar(int id);//Marcar o exemplar como disponível
        void IndisponibilizarExemplar(int id);//Marcar o exemplar como indisponível
        bool ExisteExemplar(int obraId, int nucleoId);
        public int ContarExemplares(int obraId, int nucleoId);
        public List<ExemplarJoinDTO> GetByNucleo(int nucleoId);

    }
}


/*
 ✔ Regra real (importante):
    - Add 👉 padrão mais comum em .NET
    - Create 👉 usado em APIs (mais “semântico”)
    - NewExemplar 👉 ❌ não é padrão (mas funciona)
*/  