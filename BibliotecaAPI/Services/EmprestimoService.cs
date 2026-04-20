using Biblioteca.ADONet;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;

namespace BibliotecaAPI.Services
{
    public class EmprestimoService : IEmprestimoService
    {
        private readonly IEmprestimoRepository _repo;
        private readonly IExemplaresRepository _exemplaresRepo;
        private readonly IUsuarioRepository _usuarioRepo;

        public EmprestimoService(
            IEmprestimoRepository repo,
            IExemplaresRepository exemplaresRepo,
            IUsuarioRepository usuarioRepo)
        {
            _repo = repo;
            _exemplaresRepo = exemplaresRepo;
            _usuarioRepo = usuarioRepo;
        }

        // ---------------- GET ----------------
        public List<EmprestimoDTO> GetAll()
        {              
            return _repo.GetAll();            
        }


        public EmprestimoDTO GetById(int id)
        {
            var e = _repo.GetById(id);

            if (e == null)
                return null;

            var usuario = _usuarioRepo.GetById(e.UsuarioID);
            var exemplar = _exemplaresRepo.GetById(e.ExemplarID);

            return new EmprestimoDTO
            {
                NomeUsuario = usuario?.Nome,
                TituloObra = exemplar?.ObraID.ToString(),               
                DataEmprestimo = e.DataEmprestimo,
                DataDevolucao = e.DataDevolucao,
                DataDevolucaoPrevista = e.DataEmprestimo.AddDays(15),
                EntregaAtrasada = e.EntregaAtrasada
            };
        }


        // ---------------- CREATE EMPRESTIMO ----------------
        public void NewEmprestimo(int usuarioId, int exemplarId)
        {
            var exemplar = _exemplaresRepo.GetById(exemplarId);

            if (exemplar == null || exemplar.Disponivel == false)
                throw new Exception("Exemplar inválido ou indisponível");

            // 🔧 1. MARCAR COMO INDISPONÍVEL
            exemplar.Disponivel = false;
            _exemplaresRepo.UpdateExemplar(exemplar);

            // 🔧 2. CRIAR EMPRÉSTIMO
            var emprestimo = new Emprestimos
            {
                UsuarioID = usuarioId,
                ExemplarID = exemplarId,
                DataEmprestimo = DateTime.Now,
                EntregaAtrasada = false
            };

            // 🔧 3. GUARDAR NO BANCO
            _repo.NewEmprestimo(emprestimo);
        }


        // ---------------- UPDATE ----------------
        public void UpdateEmprestimo(int id, EmprestimoUpdateDTO dto)
        {
            var emprestimo = _repo.GetById(id);

            if (emprestimo == null)
                throw new Exception("Empréstimo não encontrado");

            var exemplarAntigo = _exemplaresRepo.GetById(emprestimo.ExemplarID);
            var exemplarNovo = _exemplaresRepo.GetById(dto.ExemplarID);

            if (exemplarNovo == null)
                throw new Exception("Exemplar inválido");

            // 🔥 SÓ valida disponibilidade se mudou de exemplar
            if (emprestimo.ExemplarID != dto.ExemplarID)
            {
                if (exemplarNovo.Disponivel == false)
                    throw new Exception("Exemplar indisponível");

                // libertar antigo
                if (exemplarAntigo != null)
                {
                    exemplarAntigo.Disponivel = true;
                    _exemplaresRepo.UpdateExemplar(exemplarAntigo);
                }

                // bloquear novo
                exemplarNovo.Disponivel = false;
                _exemplaresRepo.UpdateExemplar(exemplarNovo);
            }

            // atualizar dados normalmente
            emprestimo.UsuarioID = dto.UsuarioID;
            emprestimo.ExemplarID = dto.ExemplarID;
            emprestimo.DataEmprestimo = dto.DataEmprestimo;
            emprestimo.DataDevolucao = dto.DataDevolucao;
            emprestimo.EntregaAtrasada = dto.EntregaAtrasada;

            _repo.UpdateEmprestimo(emprestimo);
        }


        // ---------------- DELETE ----------------
        public void DeleteEmprestimo(int id)
        {
            _repo.DeleteEmprestimo(id);
        }


        // ---------------- RETURN BOOK ----------------
        public void ReturnBook(int id)
        {
            var emprestimo = _repo.GetById(id);

            if (emprestimo == null)
                throw new Exception("Não encontrado");

            // marca devolução
            _repo.ReturnBook(id);

            // opcional: se quiseres garantir consistência
            _exemplaresRepo.DisponibilizarExemplar(emprestimo.ExemplarID);
        }
    }
}
