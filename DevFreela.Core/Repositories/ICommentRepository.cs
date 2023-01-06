using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface ICommentRepository
    {
        Task AddAsync(ProjectComment comment);
    }
}