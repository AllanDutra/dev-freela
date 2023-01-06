using Dapper;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Services.Implementations
{
    public class SkillService : ISkillService
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;
        public SkillService(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCsWork");
        }

        public List<SkillViewModel> GetAll()
        {
            #region USANDO DAPPER

            // ! O DAPPER UTILIZA O BANCO DE DADOS FÍSICO, ENTÃO NÃO É POSSÍVEL USÁ-LO COM O ENTITY NA MEMÓRIA

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "SELECT Id, Description FROM Skills";

                return sqlConnection.Query<SkillViewModel>(script).ToList();
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