using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Models
{
    public class ImagemLivro
    {
        [Key]
        public int ObrasID { get; set; } // PK e FK
        public Obras? Obra { get; set; }  // RELAÇÃO para navegação para Obras (objetos relacionados) -  NÃO existe na tabela do SQL
        public byte[]? Imagem { get; set; } // 1 imagem por obra 

        //public int ImagemLivroID { get; set; } // PK - várias imagens por obra, ID próprio da imagem.
    }
}
