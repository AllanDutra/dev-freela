using DevFreela.Core.Repositories;

namespace DevFreela.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DevFreelaDbContext _context;
        public UnitOfWork(DevFreelaDbContext context, IProjectRepository projects, IUserRepository users)
        {
            _context = context;
            Projects = projects;
            Users = users;
        }

        public IProjectRepository Projects { get; }

        public IUserRepository Users { get; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}