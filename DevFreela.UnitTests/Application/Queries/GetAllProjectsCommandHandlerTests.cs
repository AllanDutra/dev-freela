using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Models;
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
            var projects = new PaginationResult<Project>
            {
                Data = new List<Project>
                {
                    new Project("Nome do Teste 1", "Descrição De Teste 1", 1, 2, 10000),
                    new Project("Nome do Teste 2", "Descrição De Teste 2", 1, 2, 20000),
                    new Project("Nome do Teste 3", "Descrição De Teste 3", 1, 2, 30000)
                }
            };

            // * Criando o moq
            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.GetAllAsync(It.IsAny<string>(), It.IsAny<int>()).Result).Returns(projects);

            var getAllProjectsQuery = new GetAllProjectsQuery { Query = "", Page = 1 };
            var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectRepositoryMock.Object);

            // ? Act
            var paginationProjectViewModelList = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, new CancellationToken());

            // ? Assert
            Assert.NotNull(paginationProjectViewModelList);
            Assert.NotEmpty(paginationProjectViewModelList.Data);
            Assert.Equal(projects.Data.Count, paginationProjectViewModelList.Data.Count);

            projectRepositoryMock.Verify(pr => pr.GetAllAsync(It.IsAny<string>(), It.IsAny<int>()).Result, Times.Once);
        }
    }
}