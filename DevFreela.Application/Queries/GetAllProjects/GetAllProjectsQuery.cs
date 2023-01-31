using DevFreela.Application.ViewModels;
using DevFreela.Core.Models;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectsQuery : IRequest<PaginationResult<ProjectViewModel>>
    {

        public string Query { get; set; }
        public int Page { get; set; }
    }
}