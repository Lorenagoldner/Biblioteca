namespace BibliotecaAPI.DTOs
{
    public class ExemplarDTO  // DTO de saída - OUTPUT (sistema devolve)
    {
        public int ExemplaresID { get; set; }
        public string? TituloObra { get; set; }
        public string? Nucleo { get; set; }
        public bool Disponivel { get; set; }

        // ❌ não expor FK na saída:
        // *** Está alinhado com “DTO inteligente” ***
        // public int ObraID { get; set; }      // FK para TipoUsuario
        // public int NucleoID { get; set; }    // FK para TipoUsuario

    }
}
