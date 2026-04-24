using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaAPI.Models
{
    public class StatusUsuario
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)] //utilizado para indicar que esta propriedade é a chave primária da tabela e que o valor não é gerado automaticamente
        public int StatusID { get; set; }
        public string Descricao { get; set; } = null!; 
    }
}


