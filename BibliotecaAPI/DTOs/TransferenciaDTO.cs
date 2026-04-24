namespace BibliotecaAPI.DTOs
{
    public class TransferenciaDTO  // DTO de entrada - INPUT (o que o utilizador envia)
    {
        public int ObraID { get; set; }
        public int NucleoOrigemID { get; set; }
        public int NucleoDestinoID { get; set; }
        public int Quantidade { get; set; }
        public int ExemplarID { get; set; }
        public int NovoNucleoID { get; set; }
    }
}   
  
