using BibliotecaAPI.DTOs;

namespace BibliotecaAPI.Services
{
    public interface IObraService
    {
        object GetAll();
        public ObraDTO GetById(int id);
        int Add(CriarObraDTO dto);
        void Update(int id, AtualizarObraDTO dto);
        void Delete(int id);
        List<ObraDetalhadaDTO> GetObrasDetalhadas();
        List<PesquisaObraDTO> PesquisarObra(string texto);
    }
}
