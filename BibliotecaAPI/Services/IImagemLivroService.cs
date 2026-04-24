using BibliotecaAPI.Models;

namespace BibliotecaAPI.Services
{
    public interface IImagemLivroService
    {
        void Upload(int obraId, IFormFile file);
        ImagemLivro Get(int obraId);
    }
}
