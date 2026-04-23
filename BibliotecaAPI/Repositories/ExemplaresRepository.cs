using Biblioteca.ADONet;
using BibliotecaAPI.Models;
using Microsoft.Data.SqlClient;
using BibliotecaAPI.DTOs;

namespace BibliotecaAPI.Repositories
{
    public class ExemplaresRepository : IExemplaresRepository
    {
        public List<ExemplarJoinDTO> GetAll()
        {
            string sql = @"
                        SELECT 
                            e.ExemplaresID,
                            e.ObraID,
                            e.NucleoID,
                            o.Titulo AS TituloObra,
                            n.Nome AS Nucleo,
                            e.Disponivel
                        FROM Exemplares e
                        INNER JOIN Obras o ON e.ObraID = o.ObraID
                        INNER JOIN Nucleo n ON e.NucleoID = n.ID
                    ";                
            return DALPro.Query<ExemplarJoinDTO>(sql);
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



        public int NewExemplar(Exemplares exemplar)
        {
            string sql = @"
                        INSERT INTO Exemplares (ObraID, NucleoID, Disponivel)
                        VALUES (@ObraID, @NucleoID, @Disponivel);

                        SELECT CAST(SCOPE_IDENTITY() AS int);
                    ";


            int id = DALPro.Query<int>(sql, new Dictionary<string, object>
            {
                { "@ObraID", exemplar.ObraID },
                { "@NucleoID", exemplar.NucleoID },
                { "@Disponivel", exemplar.Disponivel }
            }).FirstOrDefault();

            return id;
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



        public bool ExisteExemplar(int obraId, int nucleoId)
        {
            string sql = @"SELECT COUNT(1)
                   FROM Exemplares
                   WHERE ObraID = @ObraID
                   AND NucleoID = @NucleoID";

            var result = DALPro.ExecuteScalar(sql, new Dictionary<string, object>
            {
                { "@ObraID", obraId },
                { "@NucleoID", nucleoId }
            });

            return Convert.ToInt32(result) > 0;
        }



        public int ContarExemplares(int obraId, int nucleoId)
        {
            string sql = @"
                        SELECT COUNT(*)
                        FROM Exemplares
                        WHERE ObraID = @ObraID
                        AND NucleoID = @NucleoID
                        ";

            var result = DALPro.ExecuteScalar(sql, new Dictionary<string, object>
            {
                { "@ObraID", obraId },
                { "@NucleoID", nucleoId }
            });

            return Convert.ToInt32(result);
        }



        public List<ExemplarJoinDTO> GetByNucleo(int nucleoId)
        {
            string sql = @"
                        SELECT 
                            e.ExemplaresID,
                            e.ObraID,
                            e.NucleoID,
                            o.Titulo AS TituloObra,
                            n.Nome AS Nucleo,
                            e.Disponivel
                        FROM Exemplares e
                        INNER JOIN Obras o ON e.ObraID = o.ObraID
                        INNER JOIN Nucleo n ON e.NucleoID = n.ID
                        WHERE e.NucleoID = @NucleoID
                    ";

            return DALPro.Query<ExemplarJoinDTO>(sql, new Dictionary<string, object>
            {
                { "@NucleoID", nucleoId }
            });
        }


    }
}
