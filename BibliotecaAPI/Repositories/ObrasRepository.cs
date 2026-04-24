using Biblioteca.ADONet;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;
using System.Collections.Generic;

namespace BibliotecaAPI.Repositories
{
    public class ObrasRepository : IObrasRepository
    {
        private readonly IGeneroRepository _generoRepo;

        // Construtor:
        public ObrasRepository(IGeneroRepository generoRepo)
        {
            _generoRepo = generoRepo;
        }


        //------------------- MÉTODOS -------------------- //
        public List<Obras> GetAll()
        {
            string sql = "SELECT * FROM Obras";
            return DALPro.Query<Obras>(sql);
        }


        public Obras GetById(int id)
        {
            string sql = "SELECT * FROM Obras WHERE ObraID = @id";

            var result = DALPro.Query<Obras>(sql,
                new Dictionary<string, object>
                {
                    { "@id", id }
                });

            return result.FirstOrDefault();
        }


        public int Add(Obras obra)
        {
            string sql = @"
        INSERT INTO Obras (Titulo, Autor, GeneroID, Descricao, ISBN)
        VALUES (@Titulo, @Autor, @GeneroID, @Descricao, @ISBN);

        SELECT SCOPE_IDENTITY();
        ";

            var id = DALPro.ExecuteScalar(sql, new Dictionary<string, object>
            {
                { "@Titulo", obra.Titulo },
                { "@Autor", obra.Autor },
                { "@GeneroID", obra.GeneroID },
                { "@Descricao", obra.Descricao },
                { "@ISBN", obra.ISBN }
            });

            return Convert.ToInt32(id);
        }


        public void Update(Obras obra)
        {     

            if (obra.GeneroID <= 0)
                throw new Exception("Genero obrigatório");

            string sql = @"UPDATE Obras 
                           SET Titulo = @Titulo,
                               Autor = @Autor,
                               GeneroID = @GeneroID,
                               Descricao = @Descricao
                           WHERE 
                               ObraID = @ObraID";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@ObraID", obra.ObraID },
                { "@Titulo", obra.Titulo },
                { "@Autor", obra.Autor },
                { "@GeneroID", obra.GeneroID },
                { "@Descricao", obra.Descricao }
            });
        }


        public void Delete(int id)
        {
            string sql = "DELETE FROM Obras WHERE ObraID = @id";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@id", id }
            });
        }


        public List<PesquisaObraDTO> PesquisarObra(string texto)
        {
            string sql = "EXEC sp_pesquisaObras @titulo";

            return DALPro.Query<PesquisaObraDTO>(sql, new Dictionary<string, object>
            {
                { "@titulo", texto }
            });
        }
    }
}
