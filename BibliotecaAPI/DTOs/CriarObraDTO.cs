namespace BibliotecaAPI.DTOs
{
    public class CriarObraDTO
    {
        public required string Titulo { get; set; }
        public required string Autor { get; set; }
        public int GeneroID { get; set; }
        public required string Descricao { get; set; }
        public required string ISBN { get; set; }  // ISBN = identificador único de um livro
    }
}
    