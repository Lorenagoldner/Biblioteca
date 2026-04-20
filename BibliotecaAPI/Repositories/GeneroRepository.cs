using Biblioteca.ADONet;
using BibliotecaAPI.Models;

namespace BibliotecaAPI.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        public List<Genero> GetAllGeneros()
        {
            string sql = "SELECT * FROM Genero";
            return DALPro.Query<Genero>(sql);
        }

        public Genero GetGeneroById(int id)
        {
            string sql = "SELECT * FROM Genero WHERE GeneroID = @id";

            var result = DALPro.Query<Genero>(sql,
                new Dictionary<string, object>
                {
                    { "@id", id }
                });
                return result.FirstOrDefault();
        }

        public void AddGenero(Genero genero)
        {
            string sql = @"INSERT INTO Genero (Nome)
                           VALUES (@Nome)";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@Nome", genero.Nome }
            });
        }

        public void UpdateGenero(Genero genero)
        {
            string sql = @"UPDATE Genero 
                           SET Nome = @Nome
                           WHERE GeneroID = @GeneroID";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@GeneroID", genero.GeneroID },
                { "@Nome", genero.Nome }
            });
        }

        public void DeleteGenero(int id)
        {
            string sql = "DELETE FROM Genero WHERE GeneroID = @id";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@id", id }
            });
        }
    }
}
