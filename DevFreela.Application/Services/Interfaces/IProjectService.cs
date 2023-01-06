using DevFreela.Application.ViewModels;
using DevFreela.Application.InputModels;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IProjectService
    {
        List<ProjectViewModel> GetAll(string query);
        ProjectDetailsViewModel GetById(int id);
        void Update(UpdateProjectInputModel updateProject); // ! EVITAR REUTILIZAR INPUT MODELS EM DIFERENTES MÉTODOS
        void Delete(int id);
        void CreateComment(CreateCommentInputModel inputModel);
        void Start(int id);
        void Finish(int id);
    }
}