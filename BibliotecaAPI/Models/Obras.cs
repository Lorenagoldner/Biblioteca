using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Models
{
    public class Obras
    {
        [Key] //utilizado para indicar que a propriedade é a chave primária da tabela no banco de dados
        public int ObraID { get; set; }
        public string Titulo { get; set; } = string.Empty; //utilizando string.Empty para evitar nulls
        public string Autor { get; set; } = string.Empty;
        public int GeneroID { get; set; }
        public string? Descricao { get; set; }
        public string? ISBN { get; set; }
    }
}
