using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Models
{
    public class Usuario
    {
        [Key] //utilizando para definir a chave primária
        public int UsuarioID { get; set; }
        public string Nome { get; set; } = string.Empty; //utilizando string.Empty para evitar nulls
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        public DateTime DataInscricao { get; set; }
        public int TipoUsuarioID { get; set; }
        public int StatusID { get; set; }
        public TiposDeUsuario? TipoUsuario { get; set; }  // RELAÇÕES para navegação (objetos relacionados) -  NÃO existe na tabela do SQL
        public StatusUsuario? Status { get; set; }  // RELAÇÕES para navegação (objetos relacionados) -  NÃO existe na tabela do SQL
    }
}
