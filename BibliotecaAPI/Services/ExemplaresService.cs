using Biblioteca.ADONet;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;

namespace BibliotecaAPI.Services
{
    public class ExemplaresService
    {
        private readonly IExemplaresRepository _repo;
        private readonly IObrasRepository _obraRepo;
        private readonly INucleoRepository _nucleoRepo;

        public ExemplaresService(
        IExemplaresRepository repo,
        IObrasRepository obraRepo,
        INucleoRepository nucleoRepo)
        {
            _repo = repo;
            _obraRepo = obraRepo;
            _nucleoRepo = nucleoRepo;
        }


        //METODO
        public List<ExemplarJoinDTO> GetAll()
        {
            return _repo.GetAll(); 
        }


        public Exemplares GetById(int id)
        {
            return _repo.GetById(id);
        }


        public int CriarExemplar(Exemplares exemplar)
        {
            if (exemplar.NucleoID < 1 || exemplar.NucleoID > 5)
                throw new ArgumentException("NucleoID inválido (1 a 5)");
                      
            string sql = @"
                        INSERT INTO Exemplares (ObraID, NucleoID, Disponivel)
                        VALUES (@ObraID, @NucleoID, @Disponivel);

                        SELECT CAST(SCOPE_IDENTITY() AS int);
                    ";

            var result = DALPro.ExecuteScalar(sql, new Dictionary<string, object>
            {
                { "@ObraID", exemplar.ObraID },
                { "@NucleoID", exemplar.NucleoID },
                { "@Disponivel", exemplar.Disponivel }
            });

            return Convert.ToInt32(result);
        }


        public void AtualizarExemplar(int id, CriarExemplarDTO dto)
        {
            if (dto.NucleoID < 1 || dto.NucleoID > 5)
                    throw new ArgumentException("NucleoID inválido (1 a 5)");

            // 1. VALIDAÇÃO BÁSICA
            if (dto.ObraID <= 0)
                throw new Exception("Obra inválida");

            if (dto.NucleoID <= 0)
                throw new Exception("Núcleo inválido");


            if (dto.ObraID <= 0 || dto.NucleoID <= 0)
                throw new Exception("Dados inválidos");

            var exemplar = new Exemplares
            {
                ExemplaresID = id,
                ObraID = dto.ObraID,
                NucleoID = dto.NucleoID,
                Disponivel = dto.Disponivel
            };

            _repo.UpdateExemplar(exemplar);
        }


        public void EliminarExemplar(int id)
        {
            _repo.DeleteExemplar(id);
        }


        public void TransferirExemplarParaNucleo(int exemplarId, int nucleoDestinoId)
        {
            //  Buscar exemplar atual
            var exemplar = _repo.GetById(exemplarId);
            if (exemplar == null)
                throw new Exception("Exemplar não encontrado");

            int obraId = exemplar.ObraID;
            int nucleoOrigem = exemplar.NucleoID;

            // Verificar se núcleo destino existe
            var nucleoDestino = _nucleoRepo.GetNucleoById(nucleoDestinoId);
            if (nucleoDestino == null)
                throw new Exception("Núcleo destino não existe");

            
            int totalNoNucleo = _repo.ContarExemplares(obraId, nucleoOrigem);


            if (totalNoNucleo - 1 < 2)  
                throw new Exception("Não é possível transferir. O núcleo deve manter pelo menos 2 exemplares desta obra.");

            _repo.AlterNucleo(exemplarId, nucleoDestinoId);      
        }


        public void Disponibilizar(int id)
        {
            _repo.DisponibilizarExemplar(id);
        }


        public void Indisponibilizar(int id)
        {
            _repo.IndisponibilizarExemplar(id);
        }


        public List<ExemplarJoinDTO> GetByNucleo(int nucleoId)
        {
            return _repo.GetByNucleo(nucleoId);
        }

    }
}
