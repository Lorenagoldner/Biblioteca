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
                StatusID = 1, // ativo
                Password = dto.Password
            };

            _repo.Add(usuario);
        }

        // cria leitor (força tipo = utilizador)
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

        // cancela usuário
        public void Cancelar(int id)
        {
            var user = _repo.GetById(id);

            if (user == null)
                throw new Exception("Usuário não encontrado");

            _repo.Delete(id);
        }

        // lista todos
        public List<UsuarioOutputDTO> GetAll()
        {
            var usuarios = _repo.GetAll();

            return usuarios.Select(u => new UsuarioOutputDTO
            {
                UsuarioID = u.UsuarioID,
                Nome = u.Nome,
                Email = u.Email,
                TipoUsuario = u.TipoUsuarioID == 2 ? "Admin" : "Utilizador",
                Status = u.StatusID == 1 ? "Ativo" : "Suspenso"
            }).ToList();
        }

        // busca por id
        public UsuarioOutputDTO GetById(int id)
        {
            var u = _repo.GetById(id);

            if (u == null)
                return null;

            return new UsuarioOutputDTO
            {
                UsuarioID = u.UsuarioID,
                Nome = u.Nome,
                Email = u.Email,
                TipoUsuario = u.TipoUsuarioID == 2 ? "Admin" : "Utilizador",
                Status = u.StatusID == 1 ? "Ativo" : "Suspenso"
            };
        }

        // login
        public UsuarioOutputDTO Login(LoginDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
                throw new Exception("Email e password obrigatórios");

            var user = _repo.GetByEmail(dto.Email);

            if (user == null)
                throw new Exception("Usuário não encontrado");

            if (user.Password != dto.Password)
                throw new Exception("Password inválida");

            return new UsuarioOutputDTO
            {
                UsuarioID = user.UsuarioID,
                Nome = user.Nome,
                Email = user.Email,
                TipoUsuario = user.TipoUsuarioID == 2 ? "Admin" : "Utilizador",
                Status = user.StatusID == 1 ? "Ativo" : "Suspenso"
            };
        }
    }
}