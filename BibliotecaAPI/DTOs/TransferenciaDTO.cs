namespace BibliotecaAPI.DTOs
{
    public class TransferenciaDTO  // DTO de entrada - INPUT (o que o utilizador envia)
    {
        // SP transfere 1 exemplar de um núcleo para outro
        public int ExemplarID { get; set; }
        public int NovoNucleoID { get; set; }
    }
}   
  
