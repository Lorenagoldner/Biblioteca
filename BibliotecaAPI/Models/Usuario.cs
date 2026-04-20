using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Models
{
    public class Usuario
    {
        [Key] //utilizando para definir a chave primária
        public int UsuarioID { get; set; }
        public string Nome { get; set; } = null!; 
        public string Email { get; set; } = null!;
        public DateTime DataInscricao { get; set; }
        public int TipoUsuarioID { get; set; }
        public int StatusID { get; set; }
        public TiposDeUsuario? TipoUsuario { get; set; }  // RELAÇÕES para navegação (objetos relacionados) -  NÃO existe na tabela do SQL
        public StatusUsuario? Status { get; set; }  // RELAÇÕES para navegação (objetos relacionados) -  NÃO existe na tabela do SQL
    }
}
