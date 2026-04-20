namespace BibliotecaAPI.DTOs
{
    public class ObraMaisRequisitadaDTO  // DTO de saída - OUTPUT (sistema devolve)
    {
        public required string Titulo { get; set; }
        public required int TotalRequisicoes { get; set; }
    }
}

// 👉 NÃO usar FK em DTO de saída