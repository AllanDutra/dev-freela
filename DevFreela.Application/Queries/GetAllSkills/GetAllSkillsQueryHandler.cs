using Dapper;
using DevFreela.Application.ViewModels;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Queries.GetAllSkills
{
    public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, List<SkillViewModel>>
    {
        private readonly string _connectionString;
        public GetAllSkillsQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DevFreelaCsWork");
        }

        public async Task<List<SkillViewModel>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            #region USANDO DAPPER

            // ! O DAPPER UTILIZA O BANCO DE DADOS FÍSICO, ENTÃO NÃO É POSSÍVEL USÁ-LO COM O ENTITY NA MEMÓRIA

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "SELECT Id, Description FROM Skills";

                var skills = await sqlConnection.QueryAsync<SkillViewModel>(script);

                return skills.ToList();
            }

            #endregion 

            #region  USANDO ENTITY FRAMEWORK

            // var skills = _dbContext.Skills;

            // var skillsViewModel = skills.Select(s => new SkillViewModel(s.Id, s.Description))
            // .ToList();

            // return skillsViewModel;

            #endregion
        }
    }
}