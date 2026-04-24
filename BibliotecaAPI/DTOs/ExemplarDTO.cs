namespace BibliotecaAPI.DTOs
{
    public class ExemplarDTO  // DTO de saída - OUTPUT (sistema devolve)
    {
        public int ExemplaresID { get; set; }
        public int ObraID { get; set; }
        public int NucleoID { get; set; }
        public bool Disponivel { get; set; }
        public string? TituloObra { get; set; }
        public string? Nucleo { get; set; }
    }   
}
