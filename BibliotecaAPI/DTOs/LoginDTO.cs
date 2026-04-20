namespace BibliotecaAPI.DTOs
{
    public class LoginDTO  // DTO de entrada - INPUT (o que o utilizador envia)
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
