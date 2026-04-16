namespace BibliotecaAPI.DTOs
{
    public class CriarEmprestimoDTO
    {
        public int UsuarioID { get; set; }
        public int ExemplarID { get; set; }     
    }
}

// 👉 int NUNCA é nullável, então não tem sentido usar int? a menos que seja para um campo opcional
// por isso é desnecessário usar int? para UsuarioID e ExemplarID, já que ambos são campos obrigatórios para criar um empréstimo.
 //erika