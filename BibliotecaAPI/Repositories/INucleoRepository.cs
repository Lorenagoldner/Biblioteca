using BibliotecaAPI.Models;

namespace BibliotecaAPI.Repositories
{
    public interface INucleoRepository
    {
        List<Nucleo> GetAllNucleos();
        Nucleo GetNucleoById(int id);
        void AddNucleo(Nucleo nucleo);
        void UpdateNucleo(Nucleo nucleo);
        void DeleteNucleo(int id);

    }
}
