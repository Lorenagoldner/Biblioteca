using Biblioteca.ADONet;
using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using System.Data;

namespace BibliotecaAPI.Services
{
    public class EmprestimoService
    {
        private readonly IEmprestimoRepository _emprestimoRepo;
        private readonly IHistoricoRepository _historicoRepo;

        public EmprestimoService(IEmprestimoRepository emprestimoRepo, IHistoricoRepository historicoRepo)
        {
            _emprestimoRepo = emprestimoRepo;
            _historicoRepo = historicoRepo;
        }

       
        public void RequisitarLivro(int usuarioId, int exemplarId)
        {
            var situacao = ConsultarSituacaoDetalhada(usuarioId);

            if (situacao.Any(s => s.Status == "ATRASO")) //verifica se o usuário tem algum empréstimo com status de atraso. Se tiver, lança uma exceção para impedir o novo empréstimo.
            {
                throw new Exception("Empréstimo negado: O leitor possui livros em ATRASO.");
            }
            // Aqui chamamos o repository que por sua vez chama a SP.
            // A SP já valida: Se o usuário existe, se está ativo e se tem menos de 4 livros.
            var novoEmprestimo = new Emprestimos
            {
                UsuarioID = usuarioId,
                ExemplarID = exemplarId
            };

            _emprestimoRepo.NewEmprestimo(novoEmprestimo); 
        }

        //DEVOLUÇÃO + GERAÇÃO DE HISTÓRICO
        public void DevolverLivro(int emprestimoId)
        {
            // Primeiro faz a devolução no banco
            _emprestimoRepo.ReturnBook(emprestimoId);

            //Depois aciona a geração automatica do histórico, que é feita por uma SP. A SP já pega os dados do empréstimo e do livro para criar o histórico.
            _historicoRepo.GerarHistoricoAutomatico();
        }

        public List<dynamic> ConsultarSituacaoDetalhada(int usuarioId)
        {
            //Busca os dados via SP 
            var parametros = new Dictionary<string, object>
            {
                { "@UsuarioID", usuarioId }
            };

            DataTable dt = DALPro.ExecuteSP("sp_situacaoDetalhadaLeitor", parametros);

            //Validação para não quebrar se não houver dados
            if (dt == null || dt.Rows.Count == 0)
            {
                return new List<dynamic>();
            }

            //conversão do DataTable para uma lista de objetos anônimos, onde cada objeto representa um empréstimo com suas informações detalhadas.
            var lista = dt.AsEnumerable().Select(row => new {
                Id = row.Table.Columns.Contains("EmprestimoID") ? row["EmprestimoID"] : 0,
                Obra = Convert.ToString(row["titulo"]),             
                Nucleo = Convert.ToString(row["Nucleo"]),           
                EntregaPrevista = row["DataDevolucaoPrevista"],     
                Status = Convert.ToString(row["StatusPrazo"])       
            }).Cast<dynamic>().ToList();

            return lista;
        }

        public List<Historico> ConsultarHistorico(int? nucleoId, DateTime? inicio, DateTime? fim)
        {
            return _historicoRepo.GetAll(nucleoId, inicio, fim);
        }
    }
}