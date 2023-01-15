using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllProjectsCommandHandlerTests
    {
        [Fact]
        // ? PADRÃO GIVEN_WHEN_THEN
        public async Task ThreeProjectsExists_Executed_ReturnThreeProjectViewModels()
        {
            // ? PADRÃO AAA
            // ? Arrange
            var projects = new List<Project>
            {
                new Project("Nome do Teste 1", "Descrição De Teste 1", 1, 2, 10000),
                new Project("Nome do Teste 2", "Descrição De Teste 2", 1, 2, 20000),
                new Project("Nome do Teste 3", "Descrição De Teste 3", 1, 2, 30000),
            };

            // * Criando o moq
            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.GetAllAsync().Result).Returns(projects);

            var getAllProjectsQuery = new GetAllProjectsQuery("");
            var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectRepositoryMock.Object);

            // ? Act
            var projectViewModelList = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, new CancellationToken());

            // ? Assert
            Assert.NotNull(projectViewModelList);
            Assert.NotEmpty(projectViewModelList);
            Assert.Equal(projects.Count, projectViewModelList.Count);

            projectRepositoryMock.Verify(pr => pr.GetAllAsync().Result, Times.Once);
        }
    }
}