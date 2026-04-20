namespace BibliotecaAPI.DTOs
{
    public class EmprestimoUpdateDTO
    {
        public int UsuarioID { get; set; }
        public int ExemplarID { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public bool EntregaAtrasada { get; set; }
    }
}
