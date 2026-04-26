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
        void DisponibilizarExemplar(int id);
        void IndisponibilizarExemplar(int id);
        bool ExisteExemplar(int obraId, int nucleoId);
        public int ContarExemplares(int obraId, int nucleoId);
        public List<ExemplarJoinDTO> GetByNucleo(int nucleoId);

    }
}
