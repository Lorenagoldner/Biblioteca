using Biblioteca.ADONet;
using BibliotecaAPI.Models;

namespace BibliotecaAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public List<Usuario> GetAll()
        {
            string sql = "SELECT * FROM Usuario";
            return DALPro.Query<Usuario>(sql);
        }

        public Usuario GetById(int id)
        {
            string sql = "SELECT * FROM Usuario WHERE UsuarioID = @id";

            var result = DALPro.Query<Usuario>(sql,
                new Dictionary<string, object>
                {
                    { "@id", id }
                });

            return result.FirstOrDefault();
        }

        public void Add(Usuario usuario)
        {
            string sql = @"INSERT INTO Usuario (Nome, Email, DataInscricao, TipoUsuarioID, StatusID)
                           VALUES (@Nome, @Email, @DataInscricao, @TipoUsuarioID, @StatusID)";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@Nome", usuario.Nome },
                { "@Email", usuario.Email },
                { "@DataInscricao", usuario.DataInscricao },
                { "@TipoUsuarioID", usuario.TipoUsuarioID },
                { "@StatusID", usuario.StatusID }
            });
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM Usuario WHERE UsuarioID = @id";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@id", id }
            });
        }

        public void Update(int id, Usuario usuario)
        {
            string sql = @"UPDATE Usuario
                           SET Nome = @Nome,
                               Email = @Email,
                               DataInscricao = @DataInscricao,
                               TipoUsuarioID = @TipoUsuarioID,
                               StatusID = @StatusID
                           WHERE UsuarioID = @id";

            DALPro.Execute(sql, new Dictionary<string, object>
            {
                { "@id", id },
                { "@Nome", usuario.Nome },
                { "@Email", usuario.Email },
                { "@DataInscricao", usuario.DataInscricao },
                { "@TipoUsuarioID", usuario.TipoUsuarioID },
                { "@StatusID", usuario.StatusID }
            });
        }
    }
}
