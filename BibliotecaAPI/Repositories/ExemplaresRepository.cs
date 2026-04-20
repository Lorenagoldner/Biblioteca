using Biblioteca.ADONet;
using BibliotecaAPI.Models;
using Microsoft.Data.SqlClient;

namespace BibliotecaAPI.Repositories
{
    public class ExemplaresRepository : IExemplaresRepository
    {
        public List<Exemplares> GetAll()
        {
            string sql = "SELECT * FROM Exemplares";
            return DALPro.Query<Exemplares>(sql);
        }

        public Exemplares GetById(int id)
        {
            string sql = "SELECT * FROM Exemplares WHERE ExemplaresID = @id";

            var result = DALPro.Query<Exemplares>(sql,
                new Dictionary<string, object>
                {
                    { "@id", id }
                });
                return result.FirstOrDefault();
        }

        public void NewExemplar(Exemplares exemplar)
        {
            string sql = @"INSERT INTO Exemplares (ObraID, NucleoID, Disponivel)
                           VALUES (@ObraID, @NucleoID, @Disponivel)";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@ObraID", exemplar.ObraID },
                { "@NucleoID", exemplar.NucleoID },
                { "@Disponivel", exemplar.Disponivel }
            });
        }

        public void UpdateExemplar(Exemplares exemplar)
        {
            string sql = @"UPDATE Exemplares 
                           SET ObraID = @ObraID,
                               NucleoID = @NucleoID,
                               Disponivel = @Disponivel
                           WHERE ExemplaresID = @ExemplaresID";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@ExemplaresID", exemplar.ExemplaresID },
                { "@ObraID", exemplar.ObraID },
                { "@NucleoID", exemplar.NucleoID },
                { "@Disponivel", exemplar.Disponivel }
            });
        }

        public void DeleteExemplar(int id)
        {
            string sql = "DELETE FROM Exemplares WHERE ExemplaresID = @id";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@id", id }
            });
        }

        public void AlterNucleo(int id, int idNucleo)
        {
            string sql = @"UPDATE Exemplares 
                           SET NucleoID = @NucleoID 
                           WHERE ExemplaresID = @id";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@id", id },
                { "@NucleoID", idNucleo }
            });
        }

        public void DisponibilizarExemplar(int id)
        {
            string sql = "UPDATE Exemplares SET Disponivel = 1 WHERE ExemplaresID = @id";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@id", id }
            });
        }

        public void IndisponibilizarExemplar(int id)
        {
            string sql = "UPDATE Exemplares SET Disponivel = 0 WHERE ExemplaresID = @id";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@id", id }
            });
        }
    }
}
