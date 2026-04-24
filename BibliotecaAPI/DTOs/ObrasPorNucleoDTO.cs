namespace BibliotecaAPI.DTOs
{
    public class ObrasPorNucleoDTO  // DTO de saída - OUTPUT (sistema devolve)
    {
        public required string Nucleo { get; set; }
        public string? NomeNucleo { get; set; }
        public required int TotalObras { get; set; }
    }
}

