using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Models
{
    public class Genero
    {
        [Key] //utilizado para indicar que a propriedade é a chave primária da tabela no banco de dados
        public int GeneroID { get; set; }
        public string Nome { get; set; } = null!; 
    }
}
