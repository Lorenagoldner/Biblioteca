using Biblioteca.ADONet;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;

namespace BibliotecaAPI.Services
{
    public class ExemplaresService : IExemplaresService
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


        // --------------------- MÉTODOS --------------------- //
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


        public void AtualizarExemplar(Exemplares exemplar)
        {
            if (exemplar.NucleoID < 1 || exemplar.NucleoID > 5)
                    throw new ArgumentException("NucleoID inválido (1 a 5)");

            // 1. VALIDAÇÃO BÁSICA
            if (exemplar.ObraID <= 0)
                throw new Exception("Obra inválida");

            if (exemplar.NucleoID <= 0)
                throw new Exception("Núcleo inválido");


            if (exemplar.ObraID <= 0 || exemplar.NucleoID <= 0)
                throw new Exception("Dados inválidos");


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

            // 🚨 Evitar transferir para o mesmo núcleo
            if (nucleoOrigem == nucleoDestinoId)
                throw new Exception("O exemplar já está neste núcleo");


            // 🚨 REGRA DE NEGÓCIO:
            // Contar quantos exemplares dessa obra existem no núcleo de origem
            // - Se tiver 2 → NÃO pode sair nenhum.
            // - Se tiver 3 → pode sair 1(fica 2).
            int totalNoNucleo = _repo.ContarExemplares(obraId, nucleoOrigem);

            Console.WriteLine($"DEBUG: totalNoNucleo = {totalNoNucleo}");

            if (totalNoNucleo - 1 < 2)    // “depois de remover 1 exemplar, ainda tem de ficar pelo menos 2”
                throw new Exception("Não é possível transferir. O núcleo deve manter pelo menos 2 exemplares desta obra.");

            // 4. Transferir
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
