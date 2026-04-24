using BibliotecaAPI.DTOs;
using BibliotecaAPI.Repositories;

namespace BibliotecaAPI.Services
{
    public class EstatisticasP2Service : IEstatisticasP2Service
    {
        private readonly IEstatisticasP2Repository _repo;

        public EstatisticasP2Service(IEstatisticasP2Repository repo)
        {
            _repo = repo;
        }

        public List<TopObraDTO> TopObras(DateTime inicio, DateTime fim)
        {
            var result = _repo.TopObras(inicio, fim);

            // return _repo.TopObras(inicio, fim);
            if (result.Count == 0)
                return new List<TopObraDTO>();

            return result;
        }

        public List<NucleoStatsDTO> Nucleos(DateTime inicio, DateTime fim)
        {
            var result = _repo.NucleosMaisRequisicoes(inicio, fim);

            if (result.Count == 0)
                return new List<NucleoStatsDTO>();

            return result;
        }

        public List<GeneroStatsDTO> Generos()
        {
            var result = _repo.ObrasPorGenero();

            if (result.Count == 0)
                return new List<GeneroStatsDTO>();

            return result;
        }
    }
}