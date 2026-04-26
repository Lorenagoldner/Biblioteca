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
        private readonly IImagemLivroService _imagemService;

        // Cosntrutor para injetar os repositórios correspondentes:
        public ObraService(IObrasRepository repo, IExemplaresRepository exemplaresRepo, INucleoRepository nucleoRepo, IGeneroRepository generoRepo)
        {
            _repo = repo;
            _exemplaresRepo = exemplaresRepo;
            _nucleoRepo = nucleoRepo;
            _generoRepo = generoRepo;
        }
        public object GetAll()
        {
            var obras = _repo.GetAll();

            if (obras == null || obras.Count == 0)
                return new { message = "Nenhuma obra encontrada" };

            return obras.Select(o => new ObraDTO  
            {
                ObraID = o.ObraID,
                Titulo = o.Titulo,
                Autor = o.Autor,
                Genero = o.GeneroID.ToString(),
                Descricao = o.Descricao,
                ISBN = o.ISBN
            }).ToList();
        }
        public ObraDTO GetById(int id)
        {
            var o = _repo.GetById(id);

            if (o == null)
                return null;


            return new ObraDTO 
            {
                ObraID = o.ObraID,
                Titulo = o.Titulo,
                Autor = o.Autor,
                Genero = o.GeneroID.ToString(),
                Descricao = o.Descricao,
                ISBN = o.ISBN
            };
        }

        public int Add(CriarObraDTO dto)
        {
        
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
            var exemplares = _exemplaresRepo.GetAll()
                .Where(e => e.ObraID == id)
                .ToList();

            foreach (var ex in exemplares)
            {
                _exemplaresRepo.DeleteExemplar(ex.ExemplaresID);
            }

            _repo.Delete(id);
        }

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
