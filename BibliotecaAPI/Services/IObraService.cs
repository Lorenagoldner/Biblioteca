using BibliotecaAPI.DTOs;

namespace BibliotecaAPI.Services
{
    public interface IObraService
    {
        List<ObraDTO> GetAll();
        ObraDTO GetById(int id);
        int Add(CriarObraDTO dto);
        void Update(int id, AtualizarObraDTO dto);
        void Delete(int id);
        List<ObraDetalhadaDTO> GetObrasDetalhadas();
        List<PesquisaObraDTO> PesquisarObra(string texto);

    }
}
