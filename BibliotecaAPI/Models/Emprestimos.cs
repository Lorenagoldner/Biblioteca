using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Models
{
    public class Emprestimos
    {
        [Key] //utilizado para indicar que esta propriedade é a chave primária da tabela
        public int EmprestimoID { get; set; }
        public int UsuarioID { get; set; }
        public int ExemplarID { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public bool? EntregaAtrasada { get; set; }

        // Coluna calculada no SQL (não mapeada para INSERT)
        public DateTime DataDevolucaoPrevista { get; private set; }
    }

}
