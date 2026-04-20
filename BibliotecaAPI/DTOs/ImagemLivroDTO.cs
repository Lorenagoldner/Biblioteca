namespace BibliotecaAPI.DTOs
{
    public class ImagemLivroDTO  // DTO de saída - OUTPUT (sistema devolve)
    {
        public int ObrasID { get; set; }
        public required byte[] Imagem { get; set; }
    }
}  
  
