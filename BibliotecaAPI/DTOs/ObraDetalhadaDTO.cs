namespace BibliotecaAPI.DTOs
{
    public class ObraDetalhadaDTO  // DTO de saída - OUTPUT (sistema devolve)
    {
        public int ObraID { get; set; }
        public required string Titulo { get; set; }
        public required string Autor { get; set; }

        // public int GeneroID { get; set; }  // ❌ não expor FK        
        public string? Genero { get; set; }  // ✅ versão correta (traduzida)
        public string? Descricao { get; set; }
        public string? ISBN { get; set; }

        // 🔥 RELAÇÃO IMPORTANTE (Núcleo da obra)
        public string? Nucleo { get; set; }

        public int TotalExemplares { get; set; }
    }
}
