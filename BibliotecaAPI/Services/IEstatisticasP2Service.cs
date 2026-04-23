using BibliotecaAPI.DTOs;

namespace BibliotecaAPI.Services
{
    public interface IEstatisticasP2Service
    {
        List<TopObraDTO> TopObras(DateTime inicio, DateTime fim);
        List<NucleoStatsDTO> Nucleos(DateTime inicio, DateTime fim);
        List<GeneroStatsDTO> Generos();
    }
}
