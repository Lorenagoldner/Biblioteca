namespace BibliotecaAPI.DTOs
{
    public class UsuarioDTO  // DTO de saída - OUTPUT (sistema devolve)
    {
        public int UsuarioID { get; set; }
        public required string Nome { get; set; }  // required: Isso obriga, quem criar o objeto a preencher
        public required string Email { get; set; }
        public string? TipoUsuario { get; set; }
        public string? Status { get; set; }

        // ❌ não expor FK na saída:
        // *** Está alinhado com “DTO inteligente” ***
        // public int TipoUsuarioID { get; set; }  // FK para TipoUsuario
        // public int StatusID { get; set; }       // FK para Status

    }
}   

  
/*
ERRO: propriedade não anulável (Email)
👉 Isso vem do nullable reference types do C# (ativado por padrão)

CORREÇÃO:
Usar required para campos obrigatórios, e string.Empty → evita valores nulos

Dica importante ao usar required:
👉 o Swagger depois já vai obrigar o preenchimento automaticamente
👉 isso ajuda MUITO no front.


👉 required NÃO valida vazio


-------------------------------
👉 Para DTOs:
    ✔️ usa required
    EX:
    public required string Email { get; set; }


👉 Para Models - valor padrão:
    ✔️ usa string.Empty
    EX:
    public string Email { get; set; } = string.Empty;


----------------------------------------------
string?
👉 só usar se realmente puder ser null / permite null (opcional)


----------------------------------------------
 --------------------------------
| Situação          | O que usar |
| ----------------- | ---------- |
| Campo obrigatório | `required` |
| Campo opcional    | `?`        |
 --------------------------------

----------------------------------------------
👉 int NUNCA é nullável, então não tem sentido usar int? a menos que seja para um campo opcional



----------------------------------------------
🔹 Tipos de valor vs referência

    ✔️ Tipos de valor (value types):
    int, bool, DateTime

        👉 Esses NUNCA são null por padrão:
        int → começa como 0.
        bool → começa como false.

        💡 Por isso:
        public int GeneroID { get; set; }

        ✔️ NÃO dá erro
        ✔️ NÃO precisa required


    ⚠️ Tipos de referência:
    string, byte[], objetos

        👉 Esses PODEM ser null.


----------------------------------------------
🧠 RESUMO FINAL (guarda isso!)

👉 Use:
✔️ required → strings obrigatórias
✔️ ? → campos opcionais
✔️ nada → int, bool, DateTime

*/