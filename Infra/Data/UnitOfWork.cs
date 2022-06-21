using Contract;

namespace Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly IdentityServerDbContext _context;
        public UnitOfWork(IdentityServerDbContext context)
        {
            _context = context;
        }

        public void SaveChange()
        {
            _context.SaveChanges();
        }

        public void SaveChangeAsync()
        {
            _context.SaveChangesAsync();
        }
    }
}
