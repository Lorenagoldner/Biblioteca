namespace BibliotecaAPI.DTOs
{
    public class CriarUsuarioDTO  // DTO de entrada - INPUT (o que o utilizador envia)
    {
        public required string Nome { get; set; }  // required: Isso obriga, quem criar o objeto a preencher
        public required string Email { get; set; }
        public int TipoUsuarioID { get; set; }
    }
}
