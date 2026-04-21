using Biblioteca.ADONet;
using BibliotecaAPI.Repositories;
using System.Data;

namespace BibliotecaAPI.Services
{
    public class EstatisticasService
    {
        private readonly EstatisticasRepository _repo;

        public EstatisticasService(EstatisticasRepository repo)
        {
            _repo = repo;
        }

        public dynamic ObterResumoGeral()
        {
            return new
            {
                TotalAtivos = _repo.GetTotalEmprestimosAtivos(),
                TotalAtrasados = _repo.GetTotalAtrasados(),
            };
        }

        public List<dynamic> ObterTop10MesAnterior()
        {
            DataTable dt = _repo.GetTop10MesAnterior();

            return dt.AsEnumerable().Select(row => new {
                Obra = row["titulo"],
                Total = row["TotalRequisicoes"]
            }).Cast<dynamic>().ToList();
        }
    }

}
