using Biblioteca.ADONet;
using BibliotecaAPI.DTOs;

namespace BibliotecaAPI.Repositories
{
    public class EstatisticasP2Repository : IEstatisticasP2Repository
    {
        public List<TopObraDTO> TopObras(DateTime inicio, DateTime fim)
        {
            string sql = "EXEC sp_Top10ObrasMaisRequisitadas @DataInicio, @DataFim";

            return DALPro.Query<TopObraDTO>(sql, new Dictionary<string, object>
            {
                { "@DataInicio", inicio },
                { "@DataFim", fim }
            });
        }


        public List<NucleoStatsDTO> NucleosMaisRequisicoes(DateTime inicio, DateTime fim)
        {
            string sql = "EXEC sp_NucleosMaisRequisicoes @DataInicio, @DataFim";

            return DALPro.Query<NucleoStatsDTO>(sql, new Dictionary<string, object>
        {
            { "@DataInicio", inicio },
            { "@DataFim", fim }
        });
        }

        public List<GeneroStatsDTO> ObrasPorGenero()
        {
            string sql = "EXEC sp_totalObrasPorGenero";

            return DALPro.Query<GeneroStatsDTO>(sql);
        }
    }
}
