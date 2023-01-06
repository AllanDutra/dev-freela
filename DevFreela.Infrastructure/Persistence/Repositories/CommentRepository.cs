using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        public CommentRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(ProjectComment comment)
        {
            await _dbContext.ProjectComments.AddAsync(comment);

            await _dbContext.SaveChangesAsync();
        }
    }
}