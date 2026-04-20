using BibliotecaAPI.Models;
using Biblioteca.ADONet;

namespace BibliotecaAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        // retorna todos os usuários
        public List<Usuario> GetAll()
        {
            return DALPro.Query<Usuario>("SELECT * FROM Usuario");
        }

        // busca usuário por id
        public Usuario GetById(int id)
        {
            var prm = new Dictionary<string, object>
            {
                { "@UsuarioID", id }
            };

            return DALPro.Query<Usuario>(
                "SELECT * FROM Usuario WHERE UsuarioID = @UsuarioID",
                prm
            ).FirstOrDefault();
        }

        // busca usuário por email (usado no login)
        public Usuario GetByEmail(string email)
        {
            var prm = new Dictionary<string, object>
            {
                { "@Email", email }
            };

            return DALPro.Query<Usuario>(
                "SELECT * FROM Usuario WHERE Email = @Email",
                prm
            ).FirstOrDefault();
        }

        // cria um novo usuário
        public void Add(Usuario usuario)
        {
            var prm = new Dictionary<string, object>
            {
                { "@Nome", usuario.Nome },
                { "@Email", usuario.Email },
                { "@DataInscricao", usuario.DataInscricao },
                { "@TipoUsuarioID", usuario.TipoUsuarioID },
                { "@StatusID", usuario.StatusID },
                { "@Password", usuario.Password }
            };

            DALPro.Execute(
                @"INSERT INTO Usuario 
                  (Nome, Email, DataInscricao, TipoUsuarioID, StatusID, password) 
                  VALUES (@Nome, @Email, @DataInscricao, @TipoUsuarioID, @StatusID, @Password)",
                prm
            );
        }

        // cancela usuário (não apaga da base)
        public void Delete(int id)
        {
            var prm = new Dictionary<string, object>
            {
                { "@UsuarioID", id }
            };

            DALPro.Execute(
                "UPDATE Usuario SET StatusID = 3 WHERE UsuarioID = @UsuarioID",
                prm
            );
        }

        // atualiza os dados do usuário
        public void Update(int id, Usuario usuario)
        {
            var prm = new Dictionary<string, object>
            {
                { "@UsuarioID", id },
                { "@Nome", usuario.Nome },
                { "@Email", usuario.Email },
                { "@DataInscricao", usuario.DataInscricao },
                { "@TipoUsuarioID", usuario.TipoUsuarioID },
                { "@StatusID", usuario.StatusID },
                { "@Password", usuario.Password }
            };

            DALPro.Execute(
                @"UPDATE Usuario SET 
                    Nome = @Nome,
                    Email = @Email,
                    DataInscricao = @DataInscricao,
                    TipoUsuarioID = @TipoUsuarioID,
                    StatusID = @StatusID,
                    password = @Password
                  WHERE UsuarioID = @UsuarioID",
                prm
            );
        }
    }
}