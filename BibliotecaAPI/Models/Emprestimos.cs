using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaAPI.Models
{
    public class Emprestimos
    {
        [Key] //utilizado para indicar que esta propriedade é a chave primária da tabela
        public int EmprestimoID { get; set; }
        public int UsuarioID { get; set; }
        public Usuario? Usuario { get; set; }  // RELAÇÕES para navegação   // Propriedade de navegação para a entidade Usuario, ele mostra os detalhes do usuário associado ao empréstimo, como nome e email.
        public int ExemplarID { get; set; }
        public Exemplares? Exemplar { get; set; }  // RELAÇÕES para navegação (objetos relacionados) -  NÃO existe na tabela do SQL   // Propriedade de navegação para a entidade Exemplar, ele mostra os detalhes do exemplar associado ao empréstimo, como título e autor.
        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public bool? EntregaAtrasada { get; set; }

        // Coluna calculada no SQL (não mapeada para INSERT)
        // [NotMapped]
        // public DateTime DataDevolucaoPrevista { get; private set; }
    }

  
}
