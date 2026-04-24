using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Models
{
    public class TiposDeUsuario
    {
        [Key] //utilizado para indicar que a propriedade é a chave primária da tabela no banco de dados
        public int TipoID { get; set; }
        public string Descricao { get; set; } = null!; 
    }
}
