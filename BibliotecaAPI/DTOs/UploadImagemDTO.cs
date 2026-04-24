namespace BibliotecaAPI.DTOs
{
    public class UploadImagemDTO
    {
        public int ObraId { get; set; }
        public IFormFile? Imagem { get; set; }
    }
}
