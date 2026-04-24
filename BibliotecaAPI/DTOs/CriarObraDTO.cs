namespace BibliotecaAPI.DTOs
{
    public class CriarObraDTO  // DTO de entrada - INPUT (o que o utilizador envia)
    {
        public required string Titulo { get; set; }
        public required string Autor { get; set; }
        public int GeneroID { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public required string ISBN { get; set; }  // ISBN = identificador único de um livro
    }
}
    