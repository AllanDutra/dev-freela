using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.StartProject
{
    public class StartProjectCommandHandle : IRequestHandler<StartProjectCommand, Unit>
    {
        private readonly IProjectRepository _projectRepository;
        public StartProjectCommandHandle(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Unit> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);

            project.Start();

            await _projectRepository.StartAsync(project);

            return Unit.Value;
        }
    }
}