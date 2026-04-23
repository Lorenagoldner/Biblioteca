using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;

namespace BibliotecaAPI.Services
{
    public class GeneroService : IGeneroService
    {
       
        private readonly IGeneroRepository _repo;

        public GeneroService(IGeneroRepository repo)
        {
            _repo = repo;
        }

        public List<Genero> GetAll()
        {
            return _repo.GetAllGeneros();
        }

        public Genero GetById(int id)
        {
            return _repo.GetGeneroById(id);
        }

        public void Add(Genero genero)
        {
            if (string.IsNullOrWhiteSpace(genero.Nome))
                throw new Exception("Nome do género é obrigatório");

            _repo.AddGenero(genero);
        }

        public void Update(Genero genero)
        {
            if (genero.GeneroID <= 0)
                throw new Exception("ID inválido");

            _repo.UpdateGenero(genero);
        }

        public void Delete(int id)
        {
            _repo.DeleteGenero(id);
        }        
    }
}
