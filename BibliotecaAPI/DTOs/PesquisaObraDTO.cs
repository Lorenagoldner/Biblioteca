namespace BibliotecaAPI.DTOs
{
    public class PesquisaObraDTO
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Genero { get; set; }
        public string Nucleo { get; set; }
        public int QuantidadeDisponivel { get; set; }
    }
}
