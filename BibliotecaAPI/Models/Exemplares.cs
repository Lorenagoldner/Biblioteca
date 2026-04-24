using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Models
{
    public class Exemplares
    {
        [Key]  //utilizado para indicar que a propriedade é a chave primária da tabela no banco de dados
        public int ExemplaresID { get; set; }
        public int ObraID { get; set; }
        public int NucleoID { get; set; }
        public bool Disponivel { get; set; }
        public Obras? Obra { get; set; }  // RELAÇÕES para navegação (objetos relacionados) -  NÃO existe na tabela do SQL
        public Nucleo? Nucleo { get; set; }  // RELAÇÕES para navegação (objetos relacionados) -  NÃO existe na tabela do SQL
    }
}
