namespace BibliotecaAPI.DTOs
{
    public class ObraDTO 
    {
        public int ObraID { get; set; }
        public required string Titulo { get; set; }
        public required string Autor { get; set; }
        public int GeneroID { get; set; }
        public string? Genero { get; set; }
        public string? Descricao { get; set; }
        public string? ISBN { get; set; }  

    }
}
