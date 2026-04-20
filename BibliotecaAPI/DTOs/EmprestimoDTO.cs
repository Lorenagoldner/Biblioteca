namespace BibliotecaAPI.DTOs
{
    public class EmprestimoDTO  // DTO de saída - OUTPUT (sistema devolve)   // 👉 NÃO usar FK em DTO de saída
    {
        public string NomeUsuario { get; set; }
        public string TituloObra { get; set; }
        public bool? EntregaAtrasada { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public DateTime DataDevolucaoPrevista { get; set; }  // DataDevolucaoPrevista é calculada automaticamente no SQL (15 dias após o empréstimo)
                                                             // Não é nullable porque sempre existe após a criação do empréstimo
                                                             // É incluída no DTO pois faz parte da regra de negócio (controlo de prazos e atrasos)
                                                             // 👉 Ela NÃO é nullable porque: sempre existe (é calculada); nunca vai ser null.
     
        // ❌ não expor FK na saída:
        // *** Está alinhado com “DTO inteligente” ***
        //  public int UsuarioID { get; set; }    // FK para NomeUsuario
        //  public int ExemplarID { get; set; }   // FK para TituloObra
    }
}




/*
 
🧠 Regra mental pra você nunca errar nisso:

👉 Pergunta sempre:
“Esse dado é importante mostrar?”
✔️ sim → vai no DTO

*** dados calculados → podem e devem ir para o DTO se forem úteis***

*/