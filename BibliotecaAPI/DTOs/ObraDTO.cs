namespace BibliotecaAPI.DTOs
{
    public class ObraDTO
    {
     public int ObraID { get; set; }
     public required string Titulo { get; set; }
     public required string Autor { get; set; }
     public int GeneroID { get; set; }
     public string? Descricao { get; set; }
     public string? ISBN { get; set; }  // ISBN = identificador único de um livro
    }
}


/*
3. Diferença entre DTO vs CriarDTO
👉 Cada DTO representa um momento diferente do sistema

📦 Exemplo: Emprestimo

    CriarEmprestimoDTO:
        public int UsuarioID { get; set; }
        public int ExemplarID { get; set; }

    👉 Você só precisa disso pra criar


    EmprestimoDTO:
        public int EmprestimoID { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }

    👉 Isso é o que o sistema devolve


🧠 Regra de ouro:
 ---------------------------------------
| Tipo     | Função                     |
| -------- | -------------------------- |
| CriarDTO | dados que o usuário envia  |
| DTO      | dados completos do sistema |
 ---------------------------------------

👉 Você precisa pensar sempre:
Esse dado vem do sistema ou do usuário?
- usuário → CriarDTO
- sistema → DTO

--------------------------
🔹 DTO ≠ Tabela
👉 DTO é um formato de resposta, não precisa ser igual à tabela


----------------------------------------------------------
5. “Um DTO tem opcional e o outro obrigatório — dá erro?”

    👉 NÃO dá erro
    👉 Porque são classes diferentes com propósitos diferentes

    💡 Exemplo perfeito:
        CriarObraDTO → required ISBN
        ObraDTO → string? ISBN

    👉 Por quê?
    Entrada → você exige qualidade
    Saída → pode refletir dados antigos/incompletos



*/
