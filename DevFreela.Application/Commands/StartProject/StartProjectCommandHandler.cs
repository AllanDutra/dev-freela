using Dapper;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Commands.StartProject
{
    public class StartProjectCommandHandle : IRequestHandler<StartProjectCommand, Unit>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;
        public StartProjectCommandHandle(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCsWork");
        }
        public async Task<Unit> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);

            project.Start();

            #region USANDO ENTITY FRAMEWORK

            // _dbContext.SaveChanges();

            #endregion

            #region USANDO DAPPER

            // * USADO QUANDO QUEREMOS MELHORAR A PERFORMANCE QUE PROVAVELMENTE NÃO ESTÁ BOA USANDO ENTITY

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                var script = "UPDATE Projects SET Status = @status, StartedAt = @startedAt WHERE Id = @id";

                await sqlConnection.ExecuteAsync(script, new { status = project.Status, startedAt = project.StartedAt, id = request.Id });
            }

            #endregion

            return Unit.Value;
        }
    }
}