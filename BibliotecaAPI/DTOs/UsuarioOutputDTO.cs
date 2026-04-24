namespace BibliotecaAPI.DTOs
{
    public class UsuarioOutputDTO
    {
        public int UsuarioID { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TipoUsuario { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}