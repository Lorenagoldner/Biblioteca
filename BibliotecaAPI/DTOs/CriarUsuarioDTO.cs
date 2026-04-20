namespace BibliotecaAPI.DTOs
{
    public class CriarUsuarioDTO
    {
        public required string Nome { get; set; }  // required: Isso obriga, quem criar o objeto a preencher
        public required string Email { get; set; }
        public required string Password { get; set; }
        public int TipoUsuarioID { get; set; }
    }
}
