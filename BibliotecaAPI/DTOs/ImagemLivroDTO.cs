namespace BibliotecaAPI.DTOs
{
    public class ImagemLivroDTO 
    {
        public int ObrasID { get; set; }
        public required byte[] Imagem { get; set; }
        public string? Url { get; set; }
    }
}  
  
