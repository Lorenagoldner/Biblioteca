namespace BibliotecaAPI.DTOs
{
    public class ExemplarDTO  
    {
        public int ExemplaresID { get; set; }
        public int ObraID { get; set; }
        public int NucleoID { get; set; }
        public bool Disponivel { get; set; }
        public string? TituloObra { get; set; }
        public string? Nucleo { get; set; }
    }   
}
