using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;

namespace BibliotecaAPI.Services
{
    public class ImagemLivroService
    {
        private readonly IImagemLivroRepository _repo;

        public ImagemLivroService(IImagemLivroRepository repo)
        {
            _repo = repo;
        }


        public void Upload(int obraId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new Exception("Imagem inválida");

            using var ms = new MemoryStream();
            file.CopyTo(ms);

            _repo.AtualizarImagem(obraId, ms.ToArray());
        }

        public ImagemLivro Get(int obraId)
        {
            return _repo.GetById(obraId);
        }
    }
}
