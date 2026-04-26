using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.DTOs
{
    public class AtualizarObraDTO 
    {
        public required string Titulo { get; set; }  
        public required string Autor { get; set; }
        public int GeneroID { get; set; }
        public string Descricao { get; set; } = string.Empty;
    }
}

