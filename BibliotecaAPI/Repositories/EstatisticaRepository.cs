using Biblioteca.ADONet;
using System.Data;

namespace BibliotecaAPI.Repositories
{
    public class EstatisticasRepository
    {
        public DataTable GetTop10MesAnterior()
        {
            // 1. Calculamos o primeiro e o último dia do mês anterior
            DateTime hoje = DateTime.Now;
            DateTime primeiroDiaMesAnterior = new DateTime(hoje.Year, hoje.Month, 1).AddMonths(-1);
            DateTime ultimoDiaMesAnterior = new DateTime(hoje.Year, hoje.Month, 1).AddDays(-1);

            // 2. Montamos os parâmetros para a SP
            var parametros = new Dictionary<string, object>
    {
        { "@DataInicio", primeiroDiaMesAnterior },
        { "@DataFim", ultimoDiaMesAnterior }
    };

            // 3. Chamamos a SP passando as datas
            return DALPro.ExecuteSP("sp_Top10ObrasMaisRequisitadas", parametros);
        }

        public int GetTotalEmprestimosAtivos()
        {
            string sql = "SELECT COUNT(*) FROM Emprestimos WHERE DataDevolucao IS NULL";

            object resultado = DALPro.ExecuteScalar(sql); // ExecuteScalar retorna um objeto, então precisamos converter para int
            return Convert.ToInt32(resultado);
        }

        public int GetTotalAtrasados()
        {
            string sql = "SELECT COUNT(*) FROM Emprestimos WHERE DataDevolucao IS NULL AND GETDATE() > DataDevolucaoPrevista";

            object resultado = DALPro.ExecuteScalar(sql);
            return Convert.ToInt32(resultado);
        }

        public int GetPertoDeVencer()
        {
            string sql = @"SELECT COUNT(*) FROM Emprestimos 
                       WHERE DataDevolucao IS NULL 
                       AND DATEDIFF(day, GETDATE(), DataDevolucaoPrevista) BETWEEN 0 AND 3";

            object resultado = DALPro.ExecuteScalar(sql);
            return Convert.ToInt32(resultado);
        }
    }
}
