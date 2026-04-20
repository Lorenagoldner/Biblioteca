namespace BibliotecaAPI.DTOs
{
    public class DisponibilidadeDTO  // DTO de saída - OUTPUT (sistema devolve)     // 👉 NÃO usar FK em DTO de saída
    {
        public required string Titulo { get; set; }
        public int Disponiveis { get; set; }
    }
}

// 👉 int NUNCA é nullável, então não tem sentido usar int? a menos que seja para um campo opcional
    