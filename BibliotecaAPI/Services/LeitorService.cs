using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;

namespace BibliotecaAPI.Services
{
    public class LeitorService
    {
        private readonly IUsuarioRepository _repo;

        public LeitorService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        // cria usuário (admin ou leitor)
        public void CriarUsuario(CriarUsuarioDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Nome))
                throw new Exception("Nome obrigatório");

            if (string.IsNullOrEmpty(dto.Email))
                throw new Exception("Email obrigatório");

            if (string.IsNullOrEmpty(dto.Password))
                throw new Exception("Password obrigatória");

            var existente = _repo.GetByEmail(dto.Email);
            if (existente != null)
                throw new Exception("Email já cadastrado");

            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                DataInscricao = DateTime.Now,
                TipoUsuarioID = dto.TipoUsuarioID,
                StatusID = 1,
                Password = dto.Password
            };

            _repo.Add(usuario);
        }

        // cria leitor (força tipo = leitor)
        public void CriarLeitor(CriarUsuarioDTO dto)
        {
            dto.TipoUsuarioID = 1;
            CriarUsuario(dto);
        }

        // suspende usuário
        public void Suspender(int id)
        {
            var user = _repo.GetById(id);

            if (user == null)
                throw new Exception("Usuário não encontrado");

            user.StatusID = 2;

            _repo.Update(id, user);
        }

        // reativa usuário
        public void Reativar(int id)
        {
            var user = _repo.GetById(id);

            if (user == null)
                throw new Exception("Usuário não encontrado");

            user.StatusID = 1;

            _repo.Update(id, user);
        }

        // cancela usuário (soft delete)
        public void Cancelar(int id)
        {
            var user = _repo.GetById(id);

            if (user == null)
                throw new Exception("Usuário não encontrado");

            _repo.Delete(id);
        }

        // lista todos
        public List<Usuario> GetAll()
        {
            return _repo.GetAll();
        }

        // busca por id
        public Usuario GetById(int id)
        {
            return _repo.GetById(id);
        }

        // login
        public Usuario Login(LoginDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
                throw new Exception("Email e password obrigatórios");

            var user = _repo.GetByEmail(dto.Email);

            if (user == null)
                throw new Exception("Usuário não encontrado");

            if (user.Password != dto.Password)
                throw new Exception("Password inválida");

            return user;
        }
    }
}