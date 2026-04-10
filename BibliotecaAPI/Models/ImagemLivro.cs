using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Models
{
    public class ImagemLivro
    {
        [Key]
        public int ObrasID { get; set; } //Obras id é chave primária e estrangeira ao mesmo tempo
        public byte[]? Imagem { get; set; }
    }
}
