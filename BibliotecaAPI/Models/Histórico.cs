using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Models
{
    public class Historico
    {
        [Key] //utilizado para indicar que esta propriedade é a chave primária da tabela
        public int HistoricoID { get; set; }
        public string? Requisicao { get; set; }
        public string? Nucleo { get; set; }
        public DateTime? DataRequisicao { get; set; }
        public DateTime? DataDevolucao { get; set; }
    }
}
