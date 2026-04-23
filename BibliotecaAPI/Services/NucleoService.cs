using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;

namespace BibliotecaAPI.Services
{
    public class NucleoService : INucleoService
    {
        private readonly INucleoRepository _repo;

        public NucleoService(INucleoRepository repo)
        {
            _repo = repo;
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

        public void Delete(int id)
        {
            _repo.DeleteNucleo(id);
        }

        public bool Exists(int id)
        {
            return _repo.Exists(id);
        }
    }
}
