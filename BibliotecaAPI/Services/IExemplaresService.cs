using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;

namespace BibliotecaAPI.Services
{
    public interface IExemplaresService
    {
        List<ExemplarJoinDTO> GetAll();
        Exemplares GetById(int id);
        int CriarExemplar(Exemplares exemplar);
        void AtualizarExemplar(Exemplares exemplar);
        void EliminarExemplar(int id);
        void TransferirExemplarParaNucleo(int exemplarId, int nucleoDestinoId);
        void Disponibilizar(int id);
        void Indisponibilizar(int id);
        List<ExemplarJoinDTO> GetByNucleo(int nucleoId);
    }
}
