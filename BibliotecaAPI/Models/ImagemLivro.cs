using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Models
{
    public class ImagemLivro
    {
        [Key]
        public int ObrasID { get; set; } 
        public Obras? Obra { get; set; }  
        public byte[]? Imagem { get; set; } 

    }
}
