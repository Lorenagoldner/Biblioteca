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
            string sql = "SELECT * FROM ImagemLivro WHERE ObrasID = @id";

            var result = DALPro.Query<ImagemLivro>(sql,
                new Dictionary<string, object>
                {
                    { "@id", id }
                });
            return result.FirstOrDefault();
        }


        public void NewImagemLivro(ImagemLivro imagemLivro)
        {
            string sql = @"INSERT INTO ImagemLivro (ObrasID, Imagem)
                           VALUES (@ObraID, @Imagem)";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@ObraID", imagemLivro.ObrasID },
                { "@Imagem", imagemLivro.Imagem }
            });
        }



        public void AtualizarImagem(int obraId, byte[] imagem)
        {
            string sql = @"
                        IF EXISTS (SELECT 1 FROM ImagemLivro WHERE ObrasID = @ObraID)
                        BEGIN
                            UPDATE ImagemLivro
                            SET Imagem = @Imagem
                            WHERE ObrasID = @ObraID
                        END
                        ELSE
                        BEGIN
                            INSERT INTO ImagemLivro (ObrasID, Imagem)
                            VALUES (@ObraID, @Imagem)
                        END
                    ";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@Imagem", imagem },
                { "@ObraID", obraId }
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
