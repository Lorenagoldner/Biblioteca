namespace BibliotecaAPI.DTOs
{
    public class UsuarioDTO  
    {
        public int UsuarioID { get; set; }
        public required string Nome { get; set; }  // required: Isso obriga, quem criar o objeto a preencher
        public required string Email { get; set; }
        public string? TipoUsuario { get; set; }
        public string? Status { get; set; }

    }
}   

 