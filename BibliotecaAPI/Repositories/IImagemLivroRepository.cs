using BibliotecaAPI.Models;

namespace BibliotecaAPI.Repositories
{
    public interface IImagemLivroRepository
    {
        List<ImagemLivro> GetAll();
        ImagemLivro GetById(int id);
        void NewImagemLivro(ImagemLivro imagemLivro);
        void DeleteById(int id);
        void AtualizarImagem(int obraId, byte[] imagem);

    }
}
