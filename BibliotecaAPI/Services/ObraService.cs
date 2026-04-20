using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using System.Reflection;

namespace BibliotecaAPI.Services
{
    public class ObraService : IObraService
    {
        // Injeção de dependência do repositório:
        private readonly IObrasRepository _repo;
        private readonly IExemplaresRepository _exemplaresRepo;
        private readonly INucleoRepository _nucleoRepo;
        private readonly IGeneroRepository _generoRepo; 


        // Cosntrutor para injetar os repositórios correspondentes:
        public ObraService(IObrasRepository repo, IExemplaresRepository exemplaresRepo, INucleoRepository nucleoRepo, IGeneroRepository generoRepo)
        {
            _repo = repo;
            _exemplaresRepo = exemplaresRepo;
            _nucleoRepo = nucleoRepo;
            _generoRepo = generoRepo;
        }


        // --------------------------------------------------- CRUD ---------------------------------------------- //
        // ================================ MÉTODOS DE SERVIÇO PARA GERENCIAR OBRAS =============================== //


        // ObraDTO: OUTPUT - DTO de saída (sistema devolve - vai para o utilizador).
        public List<ObraDTO> GetAll()
        {
            var obras = _repo.GetAll();

            // convertendo (MAPPING): 
            //   - Model (Obras) → vem do banco;
            //   - DTO(ObraDTO) → vai para o utilizador.
            // ✔ Você precisa criar um novo objeto DTO, baseado no Model.
            /* 
                | Fluxo | Conversão   |
                | ----- | ----------- |
                | GET   | Model → DTO |
                | POST  | DTO → Model |
            */
            return obras.Select(o => new ObraDTO  // *** uso do SELECT para converter cada obra do banco (Model), obras = lista, precisa converter cada item, em um ObraDTO (DTO de saída).
                                                  // *** GetById(): NÃO precisa de SELECT, porque é apenas 1 item (objeto), não uma lista.
            {
                ObraID = o.ObraID,
                Titulo = o.Titulo,
                Autor = o.Autor,
                Genero = o.GeneroID.ToString(), // depois melhora com JOIN
                Descricao = o.Descricao,
                ISBN = o.ISBN
            }).ToList();
        }


        // ObraDTO: OUTPUT - DTO de saída (sistema devolve - vai para o utilizador).
        public ObraDTO GetById(int id)
        {
            var o = _repo.GetById(id);

            if (o == null)
                throw new Exception("Obra não encontrada");

            return new ObraDTO // *** GetById(): NÃO precisa de SELECT, porque é apenas 1 item (objeto), não uma lista.
            {
                ObraID = o.ObraID,
                Titulo = o.Titulo,
                Autor = o.Autor,
                Genero = o.GeneroID.ToString(),
                Descricao = o.Descricao,
                ISBN = o.ISBN
            };
        }


        // CriarObraDTO: INPUT - DTO de entrada (o que o utilizador envia).
        public int Add(CriarObraDTO dto)
        {
            // convertendo (MAPPING): 
            //   - DTO(CriarObraDTO) → vem do utilizador;
            //   - Model (Obras) → vai para o banco.
            // ✔ Você precisa criar um novo objeto Model, baseado no DTO. Você está convertendo DTO → Model.
            /* 
                  | Fluxo | Conversão   |
                  | ----- | ----------- |
                  | GET   | Model → DTO |
                  | POST  | DTO → Model |
            */

            var genero = _generoRepo.GetGeneroById(dto.GeneroID);

            if (genero == null)
                throw new Exception("Genero não existe");

            var obra = new Obras
            {
                Titulo = dto.Titulo,
                Autor = dto.Autor,
                GeneroID = dto.GeneroID,
                Descricao = dto.Descricao,
                ISBN = dto.ISBN
            };

            return _repo.Add(obra);
        }


        // AtualizarObraDTO: INPUT - DTO de entrada (o que o utilizador envia).
        public void Update(int id, AtualizarObraDTO dto)
        {
            var obra = new Obras
            {
                ObraID = id,
                Titulo = dto.Titulo,
                Autor = dto.Autor,
                GeneroID = dto.GeneroID,
                Descricao = dto.Descricao
            };

            _repo.Update(obra);
        }


        public void Delete(int id)
        {
            // Efeito cascata: 👉 Primeiro apaga exemplares, depois obra.
            var exemplares = _exemplaresRepo.GetAll()
                .Where(e => e.ObraID == id)
                .ToList();

            foreach (var ex in exemplares)
            {
                _exemplaresRepo.DeleteExemplar(ex.ExemplaresID);
            }

            _repo.Delete(id);
        }



        // ---------------- 🔥 NOVO (JOIN) ----------------

        // DTO de saída - OUTPUT (sistema devolve - vai para o utilizador)
        public List<ObraDetalhadaDTO> GetObrasDetalhadas()
        {
        var obras = _repo.GetAll();
        var exemplares = _exemplaresRepo.GetAll();
        var nucleos = _nucleoRepo.GetAllNucleos();

        return obras.Select(o =>
        {
            var exemplaresObra = exemplares.Where(e => e.ObraID == o.ObraID).ToList();

            var nucleo = exemplaresObra.FirstOrDefault() != null
                ? nucleos.FirstOrDefault(n => n.ID == exemplaresObra.First().NucleoID)
                : null;

            return new ObraDetalhadaDTO
            {
                ObraID = o.ObraID,
                Titulo = o.Titulo,
                Autor = o.Autor,
                Genero = o.GeneroID.ToString(),
                Descricao = o.Descricao,
                ISBN = o.ISBN,
                Nucleo = nucleo?.Nome,
                TotalExemplares = exemplaresObra.Count
            };
            }).ToList();
        }


        public List<PesquisaObraDTO> PesquisarObra(string texto)
        {
            return _repo.PesquisarObra(texto);
        }

    }
 }
