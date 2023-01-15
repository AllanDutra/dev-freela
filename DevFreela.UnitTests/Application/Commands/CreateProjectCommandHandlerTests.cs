using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateProjectCommandHandlerTests
    {
        [Fact]
        // ? PADRÃO GIVEN_WHEN_THEN
        public async Task InputDataIsOk_Executed_ReturnProjectId()
        {
            // ? PADRÃO AAA
            // ? Arrange
            var projectRepositoryMock = new Mock<IProjectRepository>();

            var createProjectCommand = new CreateProjectCommand
            {
                Title = "Titulo de teste",
                Description = "Uma descrição daora",
                TotalCost = 50000,
                IdClient = 1,
                IdFreelancer = 2
            };

            var createProjectCommandHandler = new CreateProjectCommandHandler(projectRepositoryMock.Object);

            // ? Act
            var id = await createProjectCommandHandler.Handle(createProjectCommand, new CancellationToken());

            // ? Assert
            Assert.True(id >= 0);

            projectRepositoryMock.Verify(pr => pr.AddAsync(It.IsAny<Project>()), Times.Once);
        }
    }
}