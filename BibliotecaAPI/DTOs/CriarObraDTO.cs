namespace BibliotecaAPI.DTOs
{
    public class CriarObraDTO 
    {
        public required string Titulo { get; set; }
        public required string Autor { get; set; }
        public int GeneroID { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public required string ISBN { get; set; }  
    }
}
    