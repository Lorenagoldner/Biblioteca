namespace BibliotecaAPI.Models
{
    public class Nucleo
    {
        public int ID { get; set; }
        public string Nome { get; set; } = string.Empty; //utilizando string.Empty para evitar nulls
        public string Morada { get; set; } = string.Empty;
    }
}
