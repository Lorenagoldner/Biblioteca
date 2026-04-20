using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Models
{
    public class Obras
    {
        [Key] //utilizado para indicar que a propriedade é a chave primária da tabela no banco de dados
        public int ObraID { get; set; }
        public string Titulo { get; set; } = null!; 
        public string Autor { get; set; } = null!;
        public int GeneroID { get; set; }
        public Genero? Genero { get; set; }  //RELAÇÕES para navegação (objetos relacionados) -  NÃO existe na tabela do SQL
        public string Descricao { get; set; } = null!;
        public string ISBN { get; set; } = null!;
    }
}
