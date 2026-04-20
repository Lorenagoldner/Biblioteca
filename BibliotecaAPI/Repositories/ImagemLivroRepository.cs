using Biblioteca.ADONet;
using BibliotecaAPI.Models;

namespace BibliotecaAPI.Repositories
{
    public class ImagemLivroRepository : IImagemLivroRepository
    {
        public List<ImagemLivro> GetAll()
        {
            string sql = "SELECT * FROM ImagemLivro";
            return DALPro.Query<ImagemLivro>(sql);
        }


        public ImagemLivro GetById(int id)
        {
            string sql = "SELECT * FROM ImagemLivro WHERE ObraID = @id";

            var result = DALPro.Query<ImagemLivro>(sql,
                new Dictionary<string, object>
                {
                    { "@id", id }
                });
            return result.FirstOrDefault();
        }


        public void NewImagemLivro(ImagemLivro imagemLivro)
        {
            string sql = @"INSERT INTO ImagemLivro (ObraID, Imagem)
                           VALUES (@ObraID, @Imagem)";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@ObraID", imagemLivro.ObrasID },
                { "@Imagem", imagemLivro.Imagem }
            });
        }


        public void UpdateImagemLivro(ImagemLivro imagemLivro)
        {
            string sql = @"UPDATE ImagemLivro
                           SET Imagem = @Imagem
                           WHERE ObrasID = @ObrasID";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@ObrasID", imagemLivro.ObrasID },
                { "@Imagem", imagemLivro.Imagem }
            });
        }


        public void DeleteById(int id)
        {
            string sql = "DELETE FROM ImagemLivro WHERE ObrasID = @id";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@id", id }
            });
        }
    }
}
