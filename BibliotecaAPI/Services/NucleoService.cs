using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;

namespace BibliotecaAPI.Services
{
    public class NucleoService 
    {
        private readonly INucleoRepository _repo;
        private readonly IExemplaresRepository _exemplaresRepo;

        public NucleoService(INucleoRepository repo, IExemplaresRepository exemplaresRepo)
        {
            _repo = repo;
            _exemplaresRepo = exemplaresRepo;
        }

        public List<Nucleo> GetAll()
        {
            return _repo.GetAllNucleos();
        }

        public Nucleo GetById(int id)
        {
            return _repo.GetNucleoById(id);
        }

        public void Add(Nucleo nucleo)
        {
            if (string.IsNullOrWhiteSpace(nucleo.Nome))
                throw new Exception("Nome obrigatório");

            _repo.AddNucleo(nucleo);
        }

        public void Update(Nucleo nucleo)
        {
            if (nucleo.ID <= 0)
                throw new Exception("ID inválido");

            _repo.UpdateNucleo(nucleo);
        }

        public string Delete(int id)
        {
            var temExemplares = _exemplaresRepo.GetAll()
           .Any(e => e.NucleoID == id);

            if (temExemplares)
                return "Não é possível eliminar este Núcleo porque existem exemplares associados.";

            _repo.DeleteNucleo(id);
            return "Núcleo eliminado com sucesso";

        }

        public bool Exists(int id)
        {
            return _repo.Exists(id);
        }
    }
}
