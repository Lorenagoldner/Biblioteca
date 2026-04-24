using BibliotecaAPI.DTOs;

namespace BibliotecaAPI.Repositories
{
    public interface IEstatisticasP2Repository
    {
        List<TopObraDTO> TopObras(DateTime inicio, DateTime fim);
        List<NucleoStatsDTO> NucleosMaisRequisicoes(DateTime inicio, DateTime fim);
        List<GeneroStatsDTO> ObrasPorGenero();
    }
}
