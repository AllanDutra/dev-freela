using Dapper;
using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;
        public SkillRepository(IConfiguration configuration, DevFreelaDbContext dbContext)
        {
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
            _dbContext = dbContext;
        }

        public async Task AddSkillFromProject(Project project)
        {
            var words = project.Description.Split(' ');
            var length = words.Length;

            var skill = $"{project.Id} - {words[length - 1]}";

            await _dbContext.Skills.AddAsync(new Skill(skill));
        }

        public async Task<List<SkillDTO>> GetAllAsync()
        {
            #region USANDO DAPPER

            // ! O DAPPER UTILIZA O BANCO DE DADOS FÍSICO, ENTÃO NÃO É POSSÍVEL USÁ-LO COM O ENTITY NA MEMÓRIA

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "SELECT Id, Description FROM Skills";

                var skills = await sqlConnection.QueryAsync<SkillDTO>(script);

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