namespace BibliotecaAPI.DTOs
{
    public class ExemplarDetalheDTO
    {
        public int ExemplaresID { get; set; }
        public int ObraID { get; set; }
        public int NucleoID { get; set; }
        public bool Disponivel { get; set; }
        public string TituloObra { get; set; }
        public string NomeNucleo { get; set; }
    }
}
