namespace BibliotecaAPI.DTOs
{
    public class ObraDetalhadaDTO
    {
        public int ObraID { get; set; }
        public required string Titulo { get; set; }
        public required string Autor { get; set; }  
        public string? Genero { get; set; } 
        public string? Descricao { get; set; }
        public string? ISBN { get; set; }
        public string? Nucleo { get; set; }
        public int TotalExemplares { get; set; }
    }
}
