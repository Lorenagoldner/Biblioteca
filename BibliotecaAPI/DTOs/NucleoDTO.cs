namespace BibliotecaAPI.DTOs
{
    public class NucleoDTO  // DTO de saída - OUTPUT (sistema devolve)
    {
        public int ID { get; set; }
        public required string Nome { get; set; }
        public string? Morada { get; set; }
    }
}
