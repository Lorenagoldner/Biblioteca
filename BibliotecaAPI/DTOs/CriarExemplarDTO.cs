namespace BibliotecaAPI.DTOs
{
    public class CriarExemplarDTO  // DTO de entrada - INPUT (o que o utilizador envia)
    {
        public int ObraID { get; set; }
        public int NucleoID { get; set; }
        public bool Disponivel { get; set; }
}
