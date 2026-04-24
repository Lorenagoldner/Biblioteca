namespace BibliotecaAPI.DTOs
{
    public class EmprestimoDTO
    {
        public int EmprestimoID { get; set; }
        public int UsuarioID { get; set; }
        public int ExemplarID { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucaoPrevista { get; set; }                                     
        public DateTime? DataDevolucao { get; set; }
        public bool? EntregaAtrasada { get; set; }
    }
}  
  
