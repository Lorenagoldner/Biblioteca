using BibliotecaAPI.Models;

namespace BibliotecaAPI.Repositories
{
    public interface IExemplaresRepository
    {
        List<Exemplares> GetAll(); 
        Exemplares GetById(int id); 
        void NewExemplar(Exemplares exemplar);
        void UpdateExemplar(Exemplares exemplar); 
        void DeleteExemplar(int id);
        void AlterNucleo(int id, int idNucleo); //Método para alterar o núcleo de um exemplar específico, identificando-o pelo ID do exemplar e fornecendo o novo ID do núcleo.

        //Não tenho certeza se esses métodos são necessários, mas eles podem ser úteis para marcar um exemplar como disponível ou indisponível, dependendo do status do empréstimo.
        void DisponibilizarExemplar(int id);//Marcar o exemplar como disponível
        void IndisponibilizarExemplar(int id);//Marcar o exemplar como indisponível
    }
}
