using Biblioteca.ADONet;
using BibliotecaAPI.Models;

namespace BibliotecaAPI.Repositories
{
    public class NucleoRepository : INucleoRepository
    {
        public List<Nucleo> GetAllNucleos()
        {
            string sql = "SELECT * FROM Nucleo";
            return DALPro.Query<Nucleo>(sql);
        }


        public Nucleo GetNucleoById(int id)
        {
            string sql = "SELECT * FROM Nucleo WHERE ID = @id";

            var result = DALPro.Query<Nucleo>(sql,
                new Dictionary<string, object>
                {
                    { "@id", id }
                });
            return result.FirstOrDefault();
        }


        public void AddNucleo(Nucleo nucleo)
        {
            string sql = @"INSERT INTO Nucleo (Nome, Morada)
                           VALUES (@Nome, @Morada)";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@Nome", nucleo.Nome },
                { "@Morada", nucleo.Morada }
            });
        }

        public void UpdateNucleo(Nucleo nucleo)
        {
            string sql = @"UPDATE Nucleo 
                           SET Nome = @Nome,
                               Morada = @Morada
                           WHERE ID = @ID";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@ID", nucleo.ID },
                { "@Nome", nucleo.Nome },
                { "@Morada", nucleo.Morada }
            });
        }

        public void DeleteNucleo(int id)
        {
            string sql = "DELETE FROM Nucleo WHERE ID = @id";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@id", id }
            });
        }
    }
}
