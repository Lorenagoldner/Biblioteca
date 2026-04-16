namespace BibliotecaAPI.DTOs
{
    public class AtualizarObraDTO
    {
        public required string Titulo { get; set; }  
        public required string Autor { get; set; }
        public int GeneroID { get; set; }
        public required string Descricao { get; set; }  
    }
}
